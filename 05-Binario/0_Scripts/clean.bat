@echo off

if exist "%1\bin" (
	echo %1\bin
	rd /s /q "%1\bin"
)

if exist "%1\obj" (
	echo %1\obj
	rd /s /q "%1\obj"
)
