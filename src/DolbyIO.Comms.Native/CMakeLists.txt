project(DolbyIO.Comms.Native LANGUAGES CXX)
set(PROJECT_VERSION ${CSSDK_MAJOR}.${CSSDK_MINOR}.${CSSDK_PATH})

if(APPLE)
    find_library(CoreVideo CoreVideo)
endif()

set(SOURCES
    utils.h
    sdk.h
    translators.h
    session.cc
    conference.cc
    media_device.cc
    audio.cc
    sdk.cc
    video.cc
    video_sink.cc
    video_frame_handler.cc
)

add_library(DolbyIO.Comms.Native  SHARED
    ${SOURCES}
)

set_target_properties(DolbyIO.Comms.Native  PROPERTIES C_VISIBILITY_PRESET default)
set_target_properties(DolbyIO.Comms.Native  PROPERTIES CXX_VISIBILITY_PRESET default)
set_target_properties(DolbyIO.Comms.Native  PROPERTIES CXX_STANDARD 17)
set_target_properties(DolbyIO.Comms.Native  PROPERTIES C_STANDARD 11)

target_link_libraries(DolbyIO.Comms.Native  PRIVATE
    DolbyioComms::sdk
    DolbyioComms::media
    ${CoreVideo}
)

if (BUILD_TESTS)
    add_library(DolbyIO.Comms.Native.Tests  SHARED
        ${SOURCES}
        $<$<BOOL:BUILD_TESTS>:tests/translators_tests.cc>
    )

    target_link_libraries(DolbyIO.Comms.Native.Tests  PRIVATE
        DolbyioComms::sdk
        DolbyioComms::media
        ${CoreVideo}
    )

    target_compile_definitions(DolbyIO.Comms.Native.Tests PRIVATE MOCK)

    set_target_properties(DolbyIO.Comms.Native.Tests  PROPERTIES C_VISIBILITY_PRESET default)
    set_target_properties(DolbyIO.Comms.Native.Tests  PROPERTIES CXX_VISIBILITY_PRESET default)
    set_target_properties(DolbyIO.Comms.Native.Tests  PROPERTIES CXX_STANDARD 17)
    set_target_properties(DolbyIO.Comms.Native.Tests  PROPERTIES C_STANDARD 11)

endif()

if (APPLE)

    set_target_properties(DolbyIO.Comms.Native PROPERTIES BUILD_RPATH "@loader_path/../PlugIns;@loader_path/.") # Unity App compatibility

    add_dotnet_runtime(
        NAME
            DolbyIO.Comms.Sdk.Runtime.Osx
        FRAMEWORK
            netstandard2.1
        VERSION
            ${CSSDK_MAJOR}.${CSSDK_MINOR}.${CSSDK_PATH}
        FILES
            $<TARGET_FILE:DolbyIO.Comms.Native>
            $<TARGET_FILE:DolbyioComms::sdk>
            $<TARGET_FILE:DolbyioComms::media>
            $<TARGET_FILE:dvc>
            $<TARGET_FILE:dnr>
        OUTPUT
            runtimes/osx-x64/native
    )
elseif (WIN32)
    # Set target link options (allows to step into and set breakpoints for mixed-mode C++ assemblies).
    # This will not be enabled for Release or MinSizeRel builds.
    target_link_options(DolbyIO.Comms.Native 
        PUBLIC $<$<OR:$<CONFIG:>,$<CONFIG:Debug>,$<CONFIG:RelWithDebInfo>>:/DEBUG /ASSEMBLYDEBUG>
    )

    target_compile_options(DolbyIO.Comms.Native PUBLIC /GR /EHa /MT /Zi)

    if (MSVC)
        set_target_properties(DolbyIO.Comms.Native 
            PROPERTIES 
                LIBRARY_OUTPUT_DIRECTORY ${CMAKE_LIBRARY_OUTPUT_DIRECTORY}/$<0:>
                RUNTIME_OUTPUT_DIRECTORY ${CMAKE_LIBRARY_OUTPUT_DIRECTORY}/$<0:>
                ARCHIVE_OUTPUT_DIRECTORY ${CMAKE_LIBRARY_OUTPUT_DIRECTORY}/$<0:>)

        if (BUILD_TESTS)
            target_compile_options(DolbyIO.Comms.Native.Tests PUBLIC /GR /EHa /MT /Zi)
            set_target_properties(DolbyIO.Comms.Native.Tests 
                PROPERTIES 
                    LIBRARY_OUTPUT_DIRECTORY ${CMAKE_LIBRARY_OUTPUT_DIRECTORY}/$<0:>
                    RUNTIME_OUTPUT_DIRECTORY ${CMAKE_LIBRARY_OUTPUT_DIRECTORY}/$<0:>
                    ARCHIVE_OUTPUT_DIRECTORY ${CMAKE_LIBRARY_OUTPUT_DIRECTORY}/$<0:>)
        endif()
    endif (MSVC)

    add_custom_command(TARGET DolbyIO.Comms.Native POST_BUILD
        COMMAND ${CMAKE_COMMAND} -E copy_if_different $<TARGET_FILE:DolbyioComms::sdk> "${CMAKE_LIBRARY_OUTPUT_DIRECTORY}/$<TARGET_FILE_NAME:DolbyioComms::sdk>"
        COMMAND ${CMAKE_COMMAND} -E copy_if_different $<TARGET_FILE:DolbyioComms::media> "${CMAKE_LIBRARY_OUTPUT_DIRECTORY}/$<TARGET_FILE_NAME:DolbyioComms::media>"
        COMMAND ${CMAKE_COMMAND} -E copy_if_different $<TARGET_FILE:dvc> "${CMAKE_LIBRARY_OUTPUT_DIRECTORY}/$<TARGET_FILE_NAME:dvc>"
        COMMAND ${CMAKE_COMMAND} -E copy_if_different $<TARGET_FILE:dnr> "${CMAKE_LIBRARY_OUTPUT_DIRECTORY}/$<TARGET_FILE_NAME:dnr>"
        COMMAND ${CMAKE_COMMAND} -E copy_if_different $<TARGET_FILE:avcodec> "${CMAKE_LIBRARY_OUTPUT_DIRECTORY}/$<TARGET_FILE_NAME:avcodec>"
        COMMAND ${CMAKE_COMMAND} -E copy_if_different $<TARGET_FILE:avformat> "${CMAKE_LIBRARY_OUTPUT_DIRECTORY}/$<TARGET_FILE_NAME:avformat>"
        COMMAND ${CMAKE_COMMAND} -E copy_if_different $<TARGET_FILE:avutil> "${CMAKE_LIBRARY_OUTPUT_DIRECTORY}/$<TARGET_FILE_NAME:avutil>"
    )

    add_dotnet_runtime(
        NAME
            DolbyIO.Comms.Sdk.Runtime.Win
        FRAMEWORK
            netstandard2.1
        VERSION
            ${CSSDK_MAJOR}.${CSSDK_MINOR}.${CSSDK_PATH}
        FILES
            $<TARGET_FILE:DolbyIO.Comms.Native>
            $<TARGET_FILE:DolbyioComms::sdk>
            $<TARGET_FILE:DolbyioComms::media>
            $<TARGET_FILE:dvc>
            $<TARGET_FILE:dnr>
            $<TARGET_FILE:avcodec>
            $<TARGET_FILE:avformat>
            $<TARGET_FILE:avutil>
        OUTPUT
            runtimes/win-x64/native
    )
endif()

set(CMAKE_POSITION_INDEPENDENT_CODE ON)


if(MACOS_BUNDLE)
    set_target_properties(DolbyIO.Comms.Native PROPERTIES
        #FRAMEWORK TRUE
        BUNDLE TRUE
        BUNDLE_EXTENSION "bundle"
        MACOSX_FRAMEWORK_IDENTIFIER io.dolby.comms
        MACOSX_FRAMEWORK_BUNDLE_VERSION 1
        VERSION 0.1.0
        # "compatibility version" in semantic format in Mach-O binary file
        SOVERSION 0.1.0
        XCODE_ATTRIBUTE_CODE_SIGN_IDENTITY "iPhone Developer"
    )
endif()