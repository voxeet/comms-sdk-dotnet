if (DEFINED ENV{DOLBYIO_LIBRARY_PATH})
  set(DOLBYIO_LIBRARY_PATH $ENV{DOLBYIO_LIBRARY_PATH})
  message("DOLBYIO_LIBRARY_PATH = '${DOLBYIO_LIBRARY_PATH}' from environment variable")
endif()

set(DOLBYIO_SEARCH_PATHS
  /usr/local
)

if(WIN32)
  set(DOLBYIO_LIBRARY_SUFFIXES lib)
elseif(APPLE)
  set(DOLBYIO_LIBRARY_SUFFIXES "lib")
endif()

if (NOT DOLBYIO_LIBRARY_SUFFIXES)
  message(error "Platform not supported at the moment !")
endif()

find_path(DOLBYIO_INCLUDE_DIR
  HINTS
    ${DOLBYIO_LIBRARY_PATH}
    ${DOLBYIO_LIBRARY_PATH}/sdk-release-arm
  NAMES
    "dolbyio/comms/sdk.h"
  PATH_SUFFIXES
    "include/" include
  PATHS
    ${DOLBYIO_SEARCH_PATHS}
)

if(WIN32)
  set(DOLBYIO_BIN_DIR ${DOLBYIO_INCLUDE_DIR}/../bin)
  set(DOLBYIO_LIB_DIR ${DOLBYIO_INCLUDE_DIR}/../lib)

  set(DOLBYIO_LIBRARY_SDK "${DOLBYIO_LIB_DIR}/dolbyio_comms_sdk.lib")
  set(DOLBYIO_LIBRARY_MEDIA "${DOLBYIO_LIB_DIR}/dolbyio_comms_media.lib")
  set(DOLBYIO_LIBRARY_DVC "${DOLBYIO_LIB_DIR}/dvclient.lib")
  set(DOLBYIO_LIBRARY_DNR "${DOLBYIO_LIB_DIR}/dvdnr.lib")

  set(DOLBYIO_LIBRARY_SDK_IMPORTED "${DOLBYIO_BIN_DIR}/dolbyio_comms_sdk.dll")
  set(DOLBYIO_LIBRARY_MEDIA_IMPORTED "${DOLBYIO_BIN_DIR}/dolbyio_comms_media.dll")
  set(DOLBYIO_LIBRARY_DVC_IMPORTED "${DOLBYIO_BIN_DIR}/dvclient.dll")
  set(DOLBYIO_LIBRARY_DNR_IMPORTED "${DOLBYIO_BIN_DIR}/dvdnr.dll")

  add_library(avcodec SHARED IMPORTED)
  set_target_properties(avcodec PROPERTIES
    IMPORTED_IMPLIB ""
    IMPORTED_LOCATION ${DOLBYIO_BIN_DIR}/avcodec-58.dll
  )

  add_library(avformat SHARED IMPORTED)
  set_target_properties(avformat PROPERTIES
    IMPORTED_IMPLIB ""
    IMPORTED_LOCATION ${DOLBYIO_BIN_DIR}/avformat-58.dll
  )

  add_library(avutil SHARED IMPORTED)
  set_target_properties(avutil PROPERTIES
    IMPORTED_IMPLIB ""
    IMPORTED_LOCATION ${DOLBYIO_BIN_DIR}/avutil-56.dll
  )
elseif(APPLE)
  set(DOLBYIO_BIN_DIR ${DOLBYIO_LIBRARY_PATH}/sdk-release-x86/bin)
  set(DOLBYIO_LIB_DIR ${DOLBYIO_LIBRARY_PATH}/sdk-release-x86/lib)

  set(DOLBYIO_LIBRARY_SDK "${DOLBYIO_LIBRARY_PATH}/universal/libdolbyio_comms_sdk.dylib")
  set(DOLBYIO_LIBRARY_MEDIA "${DOLBYIO_LIBRARY_PATH}/universal/libdolbyio_comms_media.dylib")
  set(DOLBYIO_LIBRARY_DVC "${DOLBYIO_LIBRARY_PATH}/sdk-release-arm/lib/libdvclient.dylib")
  set(DOLBYIO_LIBRARY_DNR "${DOLBYIO_LIBRARY_PATH}/sdk-release-arm/lib/libdvdnr.dylib")

  set(DOLBYIO_LIBRARY_SDK_IMPORTED ${DOLBYIO_LIBRARY_SDK})
  set(DOLBYIO_LIBRARY_MEDIA_IMPORTED ${DOLBYIO_LIBRARY_MEDIA})
  set(DOLBYIO_LIBRARY_DVC_IMPORTED ${DOLBYIO_LIBRARY_DVC})
  set(DOLBYIO_LIBRARY_DNR_IMPORTED ${DOLBYIO_LIBRARY_DNR})

  add_custom_command(
    OUTPUT 
      ${DOLBYIO_LIBRARY_PATH}/universal/libdolbyio_comms_sdk.dylib
    COMMAND ${CMAKE_COMMAND} -E make_directory ${DOLBYIO_LIBRARY_PATH}/universal
    COMMAND 
      "lipo" "-create" ${DOLBYIO_LIBRARY_PATH}/sdk-release-arm/lib/libdolbyio_comms_sdk.dylib ${DOLBYIO_LIBRARY_PATH}/sdk-release-x86/lib/libdolbyio_comms_sdk.dylib "-output" ${DOLBYIO_LIBRARY_PATH}/universal/libdolbyio_comms_sdk.dylib
  )

  add_custom_command(
    OUTPUT 
      ${DOLBYIO_LIBRARY_PATH}/universal/libdolbyio_comms_media.dylib
    COMMAND 
      "lipo" "-create" ${DOLBYIO_LIBRARY_PATH}/sdk-release-arm/lib/libdolbyio_comms_media.dylib ${DOLBYIO_LIBRARY_PATH}/sdk-release-x86/lib/libdolbyio_comms_media.dylib "-output" ${DOLBYIO_LIBRARY_PATH}/universal/libdolbyio_comms_media.dylib
  )

  # add_custom_command(
  #   OUTPUT 
  #     ${DOLBYIO_LIBRARY_PATH}/universal/libdvclient.dylib
  #   COMMAND 
  #     "lipo" "-create" ${DOLBYIO_LIBRARY_PATH}/sdk-release-arm/lib/libdvclient.dylib ${DOLBYIO_LIBRARY_PATH}/sdk-release-x86/lib/libdvclient.dylib "-output" ${DOLBYIO_LIBRARY_PATH}/universal/libdolbyio_comms_media.dylib
  # )

  add_custom_target(macos_universal_library 
    DEPENDS 
      ${DOLBYIO_LIBRARY_PATH}/universal/libdolbyio_comms_sdk.dylib
      ${DOLBYIO_LIBRARY_PATH}/universal/libdolbyio_comms_media.dylib
  )
endif()

add_library(DolbyioComms::sdk SHARED IMPORTED)
set_target_properties(DolbyioComms::sdk PROPERTIES
  IMPORTED_IMPLIB ${DOLBYIO_LIBRARY_SDK}
  IMPORTED_LOCATION ${DOLBYIO_LIBRARY_SDK_IMPORTED}
  INTERFACE_INCLUDE_DIRECTORIES ${DOLBYIO_INCLUDE_DIR}
  LINKER_LANGUAGE CXX
)

add_dependencies(DolbyioComms::sdk macos_universal_library)

add_library(DolbyioComms::media SHARED IMPORTED)
set_target_properties(DolbyioComms::media PROPERTIES
  IMPORTED_IMPLIB ${DOLBYIO_LIBRARY_MEDIA}
  IMPORTED_LOCATION ${DOLBYIO_LIBRARY_MEDIA_IMPORTED}
  INTERFACE_INCLUDE_DIRECTORIES ${DOLBYIO_INCLUDE_DIR}
  LINKER_LANGUAGE CXX
)

add_library(dvc SHARED IMPORTED)
set_target_properties(dvc PROPERTIES
  IMPORTED_IMPLIB ${DOLBYIO_LIBRARY_DVC}
  IMPORTED_LOCATION ${DOLBYIO_LIBRARY_DVC_IMPORTED}
  INTERFACE_INCLUDE_DIRECTORIES ${DOLBYIO_INCLUDE_DIR}
  LINKER_LANGUAGE CXX
)

add_library(dnr SHARED IMPORTED)
set_target_properties(dnr PROPERTIES
  IMPORTED_IMPLIB ${DOLBYIO_LIBRARY_DNR}
  IMPORTED_LOCATION ${DOLBYIO_LIBRARY_DNR_IMPORTED}
  INTERFACE_INCLUDE_DIRECTORIES ${DOLBYIO_INCLUDE_DIR}
  LINKER_LANGUAGE CXX
)

if (NOT WIN32)
  target_link_libraries(DolbyioComms::sdk INTERFACE dvc)
  target_link_libraries(DolbyioComms::sdk INTERFACE dnr)
endif()



mark_as_advanced(
  DOLBYIO_INCLUDE_DIR
  DOLBYIO_LIBRARY_SDK
  DOLBYIO_LIBRARY_MEDIA
  DOLBYIO_LIBRARY_IAPI_TEST
)

if (DOLBYIO_INCLUDE_DIR AND DOLBYIO_LIBRARY_SDK AND DOLBYIO_LIBRARY_MEDIA)
  message("Found DolbyIO library successfully: " ${DOLBYIO_LIBRARY_PATH})
  set(DOLBYIO_FOUND 1)
else()
  set(DOLBYIO_FOUND 0)
  message(ERROR ${DOLBYIO_LIBRARY_SDK})
  message(FATAL_ERROR "DolbyIO library was not found.\n"
      "Please check 'DOLBYIO_LIBRARY_PATH'.\n")
endif()