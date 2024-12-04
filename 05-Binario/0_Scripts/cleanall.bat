@echo off

for /F "tokens=*" %%a in (allprojects) do (
	call clean ../../04-Source/%%a
)

