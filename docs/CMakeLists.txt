option(BUILD_DOC "Build documentation" ON)

set(DOXYGEN_GENERATE_HTML YES)
set(DOXYGEN_GENERATE_XML NO)
set(DOXYGEN_GENERATE_TREEVIEW YES)
set(DOXYGEN_HIDE_SCOPE_NAMES YES)
set(DOXYGEN_USE_MDFILE_AS_MAINPAGE index.md)

set(DOXYGEN_HTML_EXTRA_STYLESHEET 
    theme/doxygen-awesome-css/doxygen-awesome.css 
    theme/doxygen-awesome-css/doxygen-awesome-sidebar-only.css
)

if (BUILD_DOC)
    find_package(Doxygen REQUIRED)
    #find_package(Sphinx REQUIRED)
    
    doxygen_add_docs(
        doxygen
        COMMENT "Generate DolbyIOSDK C# Documentation"
        ${CMAKE_SOURCE_DIR}/src/cs
        index.md
    )

    # add_custom_command(TARGET doxygen POST_BUILD
    #     COMMAND ${SPHINX_EXECUTABLE} -b html ${CMAKE_SOURCE_DIR}/docs ${CMAKE_BINARY_DIR}/docs/html
    # )
endif()

# sphinx-build -b html -D "release=$VERSION" $SOURCE_DIRECTORY $BUILD_DIRECTORY/html
