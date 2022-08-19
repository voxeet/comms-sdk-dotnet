cmake_minimum_required(VERSION 3.15)

option(BUILD_TESTS "Build Tests" ON)
option(BUILD_UNITY "Package Unity Plugin" ON)

set(CMAKE_MODULE_PATH ${CMAKE_MODULE_PATH} "${CMAKE_CURRENT_SOURCE_DIR}/cmake/")

include(version)

project(DolbyIO VERSION ${CSSDK_MAJOR}.${CSSDK_MINOR}.${CSSDK_PATH} LANGUAGES CXX)
set(CMAKE_VS_NUGET_PACKAGE_RESTORE OFF)

find_package(DolbyIO REQUIRED)
find_package(Dotnet REQUIRED)

SET(CMAKE_RUNTIME_OUTPUT_DIRECTORY "${CMAKE_CURRENT_BINARY_DIR}/bin/")
SET(CMAKE_LIBRARY_OUTPUT_DIRECTORY "${CMAKE_CURRENT_BINARY_DIR}/bin/")

add_subdirectory(src/DolbyIO.Comms.Native) 
add_subdirectory(src/DolbyIO.Comms.SDK)

add_subdirectory(samples/CmdLine)
add_subdirectory(samples/SimpleApp)    

if (BUILD_UNITY)
    include(unity)
endif()

if (BUILD_TESTS)
    include(CTest)
    add_subdirectory(src/DolbyIO.Comms.Tests)
endif()

unset(BUILD_TESTS CACHE)
unset(BUILD_UNITY CACHE)