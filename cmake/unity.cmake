if (BUILD_UNITY)
    if (NOT DEFINED ENV{UNITY_PLUGIN_PATH})
        message(FATAL_ERROR "Please set UNITY_PLUGIN_PATH")
    endif()
    set(UNITY_PLUGIN_PATH $ENV{UNITY_PLUGIN_PATH})

    if (WIN32)
        set(UNITY_RUNTIME_DIRECTORY "${UNITY_PLUGIN_PATH}/Plugins/win-x64/native")
    elseif(APPLE)
        set(UNITY_RUNTIME_DIRECTORY "${UNITY_PLUGIN_PATH}/Plugins/osx-universal/native")
    endif()

    set(UNITY_ASSEMBLY_DIRECTORY "${UNITY_PLUGIN_PATH}/Runtime/Assembly")
    
    add_custom_target(UnityPackage ALL
        COMMAND ${CMAKE_COMMAND} -E copy_if_different $<TARGET_FILE:DolbyioComms::sdk> "${UNITY_RUNTIME_DIRECTORY}/$<TARGET_FILE_NAME:DolbyioComms::sdk>"
        COMMAND ${CMAKE_COMMAND} -E copy_if_different $<TARGET_FILE:DolbyioComms::media> "${UNITY_RUNTIME_DIRECTORY}/$<TARGET_FILE_NAME:DolbyioComms::media>"
        COMMAND ${CMAKE_COMMAND} -E copy_if_different $<TARGET_FILE:dvc> "${UNITY_RUNTIME_DIRECTORY}/$<TARGET_FILE_NAME:dvc>"
        COMMAND ${CMAKE_COMMAND} -E copy_if_different $<TARGET_FILE:dnr> "${UNITY_RUNTIME_DIRECTORY}/$<TARGET_FILE_NAME:dnr>"
        COMMAND ${CMAKE_COMMAND} -E copy_if_different $<TARGET_FILE:DolbyIO.Comms.Native> "${UNITY_RUNTIME_DIRECTORY}/$<TARGET_FILE_NAME:DolbyIO.Comms.Native>"
        COMMAND ${CMAKE_COMMAND} -E copy_if_different ${CMAKE_RUNTIME_OUTPUT_DIRECTORY}/DolbyIO.Comms.Sdk.dll "${UNITY_ASSEMBLY_DIRECTORY}/DolbyIO.Comms.Sdk.dll"

        DEPENDS DolbyIO.Comms.Native DolbyIO.Comms.Sdk
    )
endif()