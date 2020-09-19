cmake_minimum_required(VERSION 3.17)

project(MATH_MODEL_TESTS C)


add_executable (MATH_MODEL_TESTS main.c)
add_library (library_source STATIC main.c)

include_directories (../src/headers)

find_library (LIBRARY_SOURCE library_source LinkedBinary)
target_link_libraries (MATH_MODEL_TESTS  ${CMAKE_SOURCE_DIR}/LinkedBinary/Library.a)
