
add_dotnet_executable(
    NAME
        DolbyCMD
    VERSION
        ${CSSDK_MAJOR}.${CSSDK_MINOR}.${CSSDK_PATH}
    FRAMEWORK
        net6.0
    SOURCES
        Main.cs
    REFERENCES
        DolbyIO.Comms.Sdk
    PACKAGES
        "System.CommandLine_2.0.0-beta4.22272.1"
        "Serilog_2.11.0"
        "Serilog.Sinks.Console_4.0.1"
        "System.Net.Http.Json_6.0.0"
        "Terminal.Gui_1.7.2"
)

if(WIN32)
# # Optional: enable native code debugging to allow stepping into unmanaged sources.
# # This is only required by the top level executable project.
    if(NOT CMAKE_BUILD_TYPE OR CMAKE_BUILD_TYPE MATCHES "Debug|RelWithDebInfo")
        set_property(TARGET DolbyCMD PROPERTY VS_GLOBAL_EnableUnmanagedDebugging "true")
    endif()
endif()