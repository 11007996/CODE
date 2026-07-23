@echo off
if "%1"=="h" goto begin
start mshta vbscript:createobject("wscript.shell").run("""%~nx0"" h",0)(window.close)&&exit
:begin
tasklist|findstr -i "notepad.exe"
if ERRORLEVEL 1 (
	start "" "C:\Windows\notepad.exe"
)

rem @echo off
rem tasklist|findstr -i "ConsoleApplication2.exe"
rem if ERRORLEVEL 1 (
rem 	start "" "D:/温湿度计/WSDJ/ConsoleApplication2.exe"
rem )