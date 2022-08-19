project(DolbyIO.Comms.Native 
    LANGUAGES 
        CXX
    VERSION
        ${CSSDK_MAJOR}.${CSSDK_MINOR}.${CSSDK_PATH}
)

add_library(DolbyIO.Comms.Native  SHARED
    utils.h
    sdk.h
    translators.h
    session.cc
    conference.cc
    media_device.cc
    sdk.cc
    $<$<BOOL:BUILD_TESTS>:tests/translators_tests.cc>
)

set_target_properties(DolbyIO.Comms.Native  PROPERTIES C_VISIBILITY_PRESET default)
set_target_properties(DolbyIO.Comms.Native  PROPERTIES CXX_VISIBILITY_PRESET default)
set_target_properties(DolbyIO.Comms.Native  PROPERTIES CXX_STANDARD 17)
set_target_properties(DolbyIO.Comms.Native  PROPERTIES C_STANDARD 11)

target_link_libraries(DolbyIO.Comms.Native  PRIVATE
    DolbyioComms::sdk
    DolbyioComms::media
)

if (WIN32)
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
    endif (MSVC)

    add_custom_command(TARGET DolbyIO.Comms.Native POST_BUILD
        COMMAND ${CMAKE_COMMAND} -E copy_if_different $<TARGET_FILE:DolbyioComms::sdk> "${CMAKE_LIBRARY_OUTPUT_DIRECTORY}/$<TARGET_FILE_NAME:DolbyioComms::sdk>"
        COMMAND ${CMAKE_COMMAND} -E copy_if_different $<TARGET_FILE:DolbyioComms::media> "${CMAKE_LIBRARY_OUTPUT_DIRECTORY}/$<TARGET_FILE_NAME:DolbyioComms::media>"
        COMMAND ${CMAKE_COMMAND} -E copy_if_different $<TARGET_FILE:dvc> "${CMAKE_LIBRARY_OUTPUT_DIRECTORY}/$<TARGET_FILE_NAME:dvc>"
    )
elseif(APPLE)
    set_target_properties(DolbyIO.Comms.Native PROPERTIES BUILD_RPATH "@loader_path/../PlugIns") # Unity App compatibility
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