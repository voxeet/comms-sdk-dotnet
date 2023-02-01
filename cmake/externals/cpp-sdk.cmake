include(FetchContent)

set(CPP_SDK_VERSION 2.3.0)

if (WIN32)
  set(CPP_SDK_FILENAME cppsdk-${CPP_SDK_VERSION}-windows64.zip)
elseif (APPLE)
  set(CPP_SDK_FILENAME cppsdk-${CPP_SDK_VERSION}-macos64.zip)
endif()

if (NOT DOLBYIO_LIBRARY_PATH)
  FetchContent_Declare(CPPSDK
      URL https://github.com/DolbyIO/comms-sdk-cpp/releases/download/${CPP_SDK_VERSION}/${CPP_SDK_FILENAME}
      DOWNLOAD_EXTRACT_TIMESTAMP true
  )

  FetchContent_MakeAvailable(CPPSDK)

  set(DOLBYIO_LIBRARY_PATH ${cppsdk_SOURCE_DIR})
endif()
