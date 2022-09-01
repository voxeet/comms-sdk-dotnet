option(BUILD_UNITY "Build unity package" ON)

if (BUILD_UNITY)
    set(UNITY_RUNTIME_DIRECTORY "${CMAKE_BINARY_DIR}/unity")

    configure_file(cmake/package.json.in ${UNITY_RUNTIME_DIRECTORY}/package.json)

    add_custom_target(UnityPackage ALL
        COMMAND ${CMAKE_COMMAND} -E copy_if_different $<TARGET_FILE:DolbyioComms::sdk> "${UNITY_RUNTIME_DIRECTORY}/Runtime/$<TARGET_FILE_NAME:DolbyioComms::sdk>"
        COMMAND ${CMAKE_COMMAND} -E copy_if_different $<TARGET_FILE:DolbyioComms::media> "${UNITY_RUNTIME_DIRECTORY}/Runtime/$<TARGET_FILE_NAME:DolbyioComms::media>"
        COMMAND ${CMAKE_COMMAND} -E copy_if_different $<TARGET_FILE:dvc> "${UNITY_RUNTIME_DIRECTORY}/Runtime/$<TARGET_FILE_NAME:dvc>"
        COMMAND ${CMAKE_COMMAND} -E copy_if_different $<TARGET_FILE:DolbyIO.Comms.Native> "${UNITY_RUNTIME_DIRECTORY}/Runtime/$<TARGET_FILE_NAME:DolbyIO.Comms.Native>"
        COMMAND ${CMAKE_COMMAND} -E copy_if_different ${CMAKE_RUNTIME_OUTPUT_DIRECTORY}/DolbyIO.Comms.Sdk.dll "${UNITY_RUNTIME_DIRECTORY}/Runtime/DolbyIO.Comms.Sdk.dll"
        
        COMMAND ${CMAKE_COMMAND} -E copy_if_different ${CMAKE_CURRENT_SOURCE_DIR}/scripts/dolbyio.comms.asmdef "${UNITY_RUNTIME_DIRECTORY}/Runtime/dolbyio.comms.asmdef"
        COMMAND ${CMAKE_COMMAND} -E copy_if_different ${CMAKE_CURRENT_SOURCE_DIR}/scripts/VectorExtension.cs "${UNITY_RUNTIME_DIRECTORY}/Runtime/Extensions/VectorExtension.cs"
        COMMAND ${CMAKE_COMMAND} -E copy_if_different ${CMAKE_CURRENT_SOURCE_DIR}/scripts/DolbyIOManager.cs "${UNITY_RUNTIME_DIRECTORY}/Runtime/Components/DolbyIOManager.cs"
        
        DEPENDS DolbyIO.Comms.Native DolbyIO.Comms.Sdk
    )
endif()