find_program(DOTNET_EXE 
  NAME
    dotnet
  HINTS
    /usr/local/share/dotnet/x64/
  PATHS
    /usr/local/share/dotnet/x64/
)

include(FindPackageHandleStandardArgs)

#Handle standard arguments to find_package like REQUIRED and QUIET
find_package_handle_standard_args(Dotnet
                                  "Failed to find dotnet executable"
                                  DOTNET_EXE)


if (NOT DOTNET_EXE)
  set(DOTNET_FOUND FALSE)
  if (Dotnet_FIND_REQUIRED)
    fatal("Unable to find dotnet executable.")
  endif()
endif()

set(_DOTNET_PROJECT_OUTPUT_DIR ${CMAKE_BINARY_DIR}/dotnet)

# Generates XML for CS Sources
macro(dotnet_gen_sources)
  set(_DOTNET_SOURCES "")
  string(APPEND _DOTNET_SOURCES "<ItemGroup>\n") 
  
  foreach(S ${DOTNET_PROJECT_SOURCES})
    string(APPEND _DOTNET_SOURCES "    <Compile Include=\"${CMAKE_CURRENT_SOURCE_DIR}/${S}\"></Compile>\n")
  endforeach()
  
  string(APPEND _DOTNET_SOURCES "</ItemGroup>\n")
endmacro()

# Generates XML for project references
macro(dotnet_gen_references)
  set(_DOTNET_REFERENCES)

  string(APPEND _DOTNET_REFERENCES "<ItemGroup>\n") 
  
  foreach(R ${DOTNET_PROJECT_REFERENCES})
    get_target_property(TMP_PROJECT ${R} DOTNET_PROJECT_PATH)
    string(APPEND _DOTNET_REFERENCES "    <ProjectReference Include=\"${TMP_PROJECT}\"></ProjectReference>\n")
  endforeach()
  
  string(APPEND _DOTNET_REFERENCES "</ItemGroup>\n")
endmacro()

# Generates XML for packages
macro(dotnet_gen_packages)
  set(_DOTNET_PACKAGES)

  string(APPEND _DOTNET_PACKAGES "<ItemGroup>\n") 

  foreach(P ${DOTNET_PROJECT_PACKAGES})
    string(REPLACE "_" ";" PL ${P})
    list(GET PL 0 PName)
    list(GET PL 1 PVersion)

    string(APPEND _DOTNET_PACKAGES "    <PackageReference Include=\"${PName}\" Version=\"${PVersion}\"></PackageReference>\n")
  endforeach()

  string(APPEND _DOTNET_PACKAGES "</ItemGroup>\n")
endmacro()

# Generates XML for runtime files
macro(dotnet_gen_files)
  set(_DOTNET_FILES)

  string(APPEND _DOTNET_FILES "<ItemGroup>\n") 
  
  foreach(F ${DOTNET_PROJECT_FILES})
    string(APPEND _DOTNET_FILES "    <Content CopyToOutputDirectory=\"PreserveNewest\" Include=\"${F}\" Link=\"%(Filename)%(Extension)\" Pack=\"true\" PackagePath=\"${DOTNET_PROJECT_OUTPUT}\"/>\n")
  endforeach()
  
  string(APPEND _DOTNET_FILES "</ItemGroup>\n")
endmacro()

# Adds build commands
macro(dotnet_build_commands)
  add_custom_command(
    OUTPUT ${DOTNET_PROJECT_BUILDTIMESTAMP}
    DEPENDS ${DOTNET_PROJECT_SOURCES} ${DOTNET_PROJECT_PATH} ${DOTNET_PROJECT_REFERENCES}
    #COMMAND ${DOTNET_EXE} restore ${DOTNET_PROJECT_PATH} 
    #COMMAND ${DOTNET_EXE} clean ${DOTNET_PROJECT_PATH}
    COMMAND ${DOTNET_EXE} build ${DOTNET_PROJECT_PATH}
    COMMAND ${CMAKE_COMMAND} -E touch ${DOTNET_PROJECT_BUILDTIMESTAMP}
  )
endmacro()

# Adds tests commands
macro(dotnet_test_commands)
  add_custom_command(
    OUTPUT ${DOTNET_PROJECT_BUILDTIMESTAMP}
    DEPENDS ${DOTNET_PROJECT_SOURCES} ${DOTNET_PROJECT_PATH}
    COMMAND ${DOTNET_EXE} build ${DOTNET_PROJECT_PATH}
    COMMAND ${CMAKE_COMMAND} -E touch ${DOTNET_PROJECT_BUILDTIMESTAMP}
  )

  add_test(
    NAME ${DOTNET_PROJECT_NAME}
    COMMAND ${DOTNET_EXE} test --collect "XPlat Code Coverage" --logger trx ${DOTNET_PROJECT_PATH}
  )
endmacro()

macro(dotnet_target)
  add_custom_target(${DOTNET_PROJECT_NAME} ALL
    DEPENDS ${DOTNET_PROJECT_BUILDTIMESTAMP}
    SOURCES ${DOTNET_PROJECT_SOURCES}
  )

  set_target_properties(${DOTNET_PROJECT_NAME}
    PROPERTIES
      DOTNET_PROJECT_PATH ${DOTNET_PROJECT_PATH}
  )
endmacro()

macro(dotnet_gen_project TEMPLATE)
  dotnet_gen_sources()
  dotnet_gen_references()
  dotnet_gen_packages()
  dotnet_gen_files()

  configure_file(
    ${CMAKE_MODULE_PATH}/dotnet/${TEMPLATE}
    ${DOTNET_PROJECT_PATH}
  )
endmacro()

# Adds a dotnet library project
function(add_dotnet_library)
  set(options)
  set(oneValueArgs NAME VERSION FRAMEWORK)
  set(multiValueArgs SOURCES REFERENCES)

  cmake_parse_arguments(DOTNET_PROJECT "${options}" "${oneValueArgs}" "${multiValueArgs}" ${ARGN})
  
  set(DOTNET_PROJECT_PATH ${_DOTNET_PROJECT_OUTPUT_DIR}/${DOTNET_PROJECT_NAME}/${DOTNET_PROJECT_NAME}.csproj)
  set(DOTNET_PROJECT_BUILDTIMESTAMP ${_DOTNET_PROJECT_OUTPUT_DIR}/${DOTNET_PROJECT_NAME}/${DOTNET_PROJECT_NAME}.buildtimestamp)

  dotnet_gen_project("library.csproj.in")
  dotnet_build_commands()
  dotnet_target()

endfunction()

# Adds a dotnet library project
function(add_dotnet_runtime)
  set(options "")
  set(oneValueArgs NAME VERSION FRAMEWORK OUTPUT)
  set(multiValueArgs FILES)

  cmake_parse_arguments(DOTNET_PROJECT "${options}" "${oneValueArgs}" "${multiValueArgs}" ${ARGN})
  
  set(DOTNET_PROJECT_PATH ${_DOTNET_PROJECT_OUTPUT_DIR}/${DOTNET_PROJECT_NAME}/${DOTNET_PROJECT_NAME}.csproj)
  set(DOTNET_PROJECT_BUILDTIMESTAMP ${_DOTNET_PROJECT_OUTPUT_DIR}/${DOTNET_PROJECT_NAME}/${DOTNET_PROJECT_NAME}.buildtimestamp)

  dotnet_gen_files()

  configure_file(
    ${CMAKE_MODULE_PATH}/dotnet/runtime.csproj.in
    ${_DOTNET_PROJECT_OUTPUT_DIR}/${DOTNET_PROJECT_NAME}/${DOTNET_PROJECT_NAME}.csproj.in
  )

  file(GENERATE
    OUTPUT ${DOTNET_PROJECT_PATH}
    INPUT  ${_DOTNET_PROJECT_OUTPUT_DIR}/${DOTNET_PROJECT_NAME}/${DOTNET_PROJECT_NAME}.csproj.in
  )
endfunction()

# Adds a dotnet executable project
function(add_dotnet_executable)
  set(options "")
  set(oneValueArgs NAME VERSION FRAMEWORK)
  set(multiValueArgs SOURCES PACKAGES REFERENCES)

  cmake_parse_arguments(DOTNET_PROJECT "${options}" "${oneValueArgs}" "${multiValueArgs}" ${ARGN})

  set(DOTNET_PROJECT_PATH ${_DOTNET_PROJECT_OUTPUT_DIR}/${DOTNET_PROJECT_NAME}/${DOTNET_PROJECT_NAME}.csproj)
  set(DOTNET_PROJECT_BUILDTIMESTAMP ${_DOTNET_PROJECT_OUTPUT_DIR}/${DOTNET_PROJECT_NAME}/${DOTNET_PROJECT_NAME}.buildtimestamp)

  dotnet_gen_project("executable.csproj.in")
  dotnet_build_commands()
  dotnet_target()
endfunction()

# Adds a dotnet Test project
function(add_dotnet_test)
  set(options "")
  set(oneValueArgs NAME VERSION FRAMEWORK)
  set(multiValueArgs SOURCES PACKAGES REFERENCES)

  cmake_parse_arguments(DOTNET_PROJECT "${options}" "${oneValueArgs}" "${multiValueArgs}" ${ARGN})

  set(DOTNET_PROJECT_PATH ${_DOTNET_PROJECT_OUTPUT_DIR}/${DOTNET_PROJECT_NAME}/${DOTNET_PROJECT_NAME}.csproj)
  set(DOTNET_PROJECT_BUILDTIMESTAMP ${_DOTNET_PROJECT_OUTPUT_DIR}/${DOTNET_PROJECT_NAME}/${DOTNET_PROJECT_NAME}.buildtimestamp)

  dotnet_gen_project("test.csproj.in")
  dotnet_test_commands()
  dotnet_target()
endfunction()
