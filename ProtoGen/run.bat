@echo off

::协议文件路径, 最后不要跟“\”符号
set SOURCE_FOLDER=protos

::C#编译器路径
set CS_COMPILER_PATH=protogen.exe
::C#文件生成路径, 最后不要跟“\”符号
set CS_TARGET_PATH=cs

::遍历所有文件
for /f "delims=" %%i in ('dir /b "%SOURCE_FOLDER%\*.proto"') do (
    
    ::生成 C# 代码
    echo %CS_COMPILER_PATH% -i:%SOURCE_FOLDER%\%%i -o:%CS_TARGET_PATH%\%%~ni.cs
    %CS_COMPILER_PATH% -i:%SOURCE_FOLDER%\%%i -o:%CS_TARGET_PATH%\%%~ni.cs

)

echo 协议生成完毕。

pause