add_custom_command(TARGET DolbyIOSDK POST_BUILD
    COMMAND ${CMAKE_COMMAND} -E copy_if_different $<TARGET_FILE:dvc> "$<TARGET_FILE_DIR:DolbyIOSDK>/../Frameworks/$<TARGET_FILE_NAME:dvc>"
    COMMAND ${CMAKE_COMMAND} -E copy_if_different $<TARGET_FILE:sdk> "$<TARGET_FILE_DIR:DolbyIOSDK>/../Frameworks/$<TARGET_FILE_NAME:sdk>"
    COMMAND ${CMAKE_COMMAND} -E copy_if_different $<TARGET_FILE:media> "$<TARGET_FILE_DIR:DolbyIOSDK>/../Frameworks/$<TARGET_FILE_NAME:media>"
    COMMAND ${CMAKE_COMMAND} -E copy_if_different $<TARGET_FILE:iapi_test> "$<TARGET_FILE_DIR:DolbyIOSDK>/../Frameworks/$<TARGET_FILE_NAME:iapi_test>"
)