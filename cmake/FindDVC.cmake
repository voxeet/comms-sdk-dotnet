message("Finding Dolby Voice Client Libraries")


if (DEFINED ENV{DVC_LIBRARY_PATH})
  set(DVC_LIBRARY_PATH $ENV{DVC_LIBRARY_PATH})
  message("DVC_LIBRARY_PATH = '${DVC_LIBRARY_PATH}' from environment variable")
endif()

if (DEFINED ENV{DVC_DNR_LIBRARY_PATH})
  set(DVC_DNR_LIBRARY_PATH $ENV{DVC_DNR_LIBRARY_PATH})
  message("DVC_DNR_LIBRARY_PATH = '${DVC_DNR_LIBRARY_PATH}' from environment variable")
endif()

if ( "${DVC_LIBRARY_PATH}" STREQUAL "")
    message(FATAL_ERROR "A DVC_LIBRARY_PATH is requred.")
endif()

find_path(DVC_INCLUDE_DIR
  NAMES
    client/include/dvclient.h
  PATHS
    ${DVC_LIBRARY_PATH}
)

set(MACOS False)

if(IOS)
  set(DVC_LIBRARY_DIR ${DVC_LIBRARY_PATH}/client/lib/ios/dvclient.xcframework/ios-arm64)
elseif(ANDROID)
  set(DVC_LIBRARY_DIR "${DVC_LIBRARY_PATH}/client/lib/android/${ANDROID_ABI}")
elseif(LINUX)
  set(DVC_LIBRARY_DIR "${DVC_LIBRARY_PATH}/client/lib/linux/amd64")
elseif(APPLE)
  set(DVC_LIBRARY_DIR "${DVC_LIBRARY_PATH}/client/lib/macos")
  set(MACOS True)
elseif(WIN32)
  set(DVC_LIBRARY_DIR "${DVC_LIBRARY_PATH}\\client\\lib\\windows\\win64\\")
else()
  message(FATAL_ERROR "Unsupported platform")
endif()

# setting the DVC_DNR_LIBRARY_DIR when DVC_DNR_LIBRARY_PATH is set
if(DEFINED DVC_DNR_LIBRARY_PATH)
  set(DVC_DNR_LIBRARY_DIR "${DVC_DNR_LIBRARY_PATH}/client/lib/android/${ANDROID_ABI}")
endif()

if(ANDROID OR LINUX OR MACOS OR WIN32)
  set(DVC_INCLUDE_DIR "${DVC_LIBRARY_PATH}/client/include")
  set(DVC_LIB_NAME dvclient)

  # CMake + NDK requires a specific library descriptor
  add_library(dvc SHARED IMPORTED)

  set_target_properties(dvc PROPERTIES
      IMPORTED_LOCATION ${DVC_LIBRARY_DIR}/${CMAKE_SHARED_LIBRARY_PREFIX}${DVC_LIB_NAME}${CMAKE_SHARED_LIBRARY_SUFFIX}
      INTERFACE_INCLUDE_DIRECTORIES ${DVC_INCLUDE_DIR}
  )

  if(WIN32)
    set_target_properties(dvc PROPERTIES
      IMPORTED_IMPLIB ${DVC_LIBRARY_DIR}\\${CMAKE_SHARED_LIBRARY_PREFIX}${DVC_LIB_NAME}.lib
      IMPORTED_LOCATION ${DVC_LIBRARY_DIR}\\${CMAKE_SHARED_LIBRARY_PREFIX}${DVC_LIB_NAME}.dll
    )
  endif(WIN32)

  # Needs to be defined as parts of the Media Engine rely on the old style
  # dependency (find_library etc):
  set(DVC_LIBRARY dvc)

  if(DEFINED DVC_DNR_LIBRARY_DIR)
    add_library(dvdnr SHARED IMPORTED)

    set_target_properties(dvdnr PROPERTIES
        IMPORTED_LOCATION ${DVC_DNR_LIBRARY_DIR}/${CMAKE_SHARED_LIBRARY_PREFIX}dvdnr${CMAKE_SHARED_LIBRARY_SUFFIX}
    )
    set(DVC_DNR_LIBRARY dvdnr)
  endif()
endif()

find_library(DVC_LIBRARY dvclient
  ${DVC_LIBRARY_DIR}
)

if(IOS)
  set(CMAKE_XCODE_ATTRIBUTE_DVC_LIBRARY_iphoneos "ios-arm64" CACHE INTERNAL "")
  set(CMAKE_XCODE_ATTRIBUTE_DVC_LIBRARY_iphonesimulator "ios-arm64_x86_64-simulator" CACHE INTERNAL "")
  string(REPLACE "ios-arm64" "$(DVC_LIBRARY_$(PLATFORM_NAME))" DVC_LIBRARY ${DVC_LIBRARY})
endif()

mark_as_advanced(
  DVC_INCLUDE_DIR
  DVC_LIBRARY_DIR
  DVC_LIBRARY
)

if(DEFINED DVC_DNR_LIBRARY_DIR)
mark_as_advanced(
  DVC_DNR_LIBRARY
)

endif()

if (DVC_INCLUDE_DIR AND DVC_LIBRARY)
  message("Found DVC library successfully.")
  set(DVC_FOUND 1)
else()
  set(DVC_FOUND 0)
  message(FATAL_ERROR "DVC library was not found.\n"
      "Please check 'DVC_LIBRARY_PATH'.\n")
endif()
