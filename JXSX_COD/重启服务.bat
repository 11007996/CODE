@echo off  
  
echo "killing application ..."
  
taskkill /f /im FoxThreadSend.exe
  
echo "application was killed successfully."
  
@ping 127.0.0.1 -n 6 >nul
 
echo "starting application"
  
cd /d C:\upfox\

start FoxThreadSend.exe
  
echo "application successfully"
 
taskkill /f /im cmd.exe
 
exit