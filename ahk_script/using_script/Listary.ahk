;;; Listary快捷键 ;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;
#HotIf WinActive("ahk_exe Listary.exe")
;;; 网络认证
::rz::
{
    run "http://172.18.20.4/ac_portal/default/pc.html?tabs=pwd&pop=0"
    ; WinActivate("Google Chrome")
    WinWaitActive("Google Chrome")
    Sleep 500
    SendInput userNo   ;;; 用户名
    Sleep 100
    SendInput "{tab}"
    Sleep 100
    SendInput oamm    ;;; 密码
    SendInput "{Enter}"
    sleep 2000
    run "http://172.18.20.20/access/ui/index.html?index_redirec=1&ascid=08:35:71:9F:73:DE#/access/auth"
}


;;; 堡垒机
::blj::
{
    run "https://fort-sx.luxshare-ict.com/client/login/index"
    sleep 2000
    SendInput "{tab}"
    SendInput userNo
    SendInput "{tab}"
    SendInput oamm
    SendInput "{Enter}"
}


;;; OA
::oa::
{
    run "https://oa.luxshare-ict.com/wui/main.jsp?templateId=1"
}

::lux::
{
    run "https://luxlink.luxshare-ict.com/portal/r/w?cmd=CLIENT_USER_HOME&sid=373467c5-8f30-45b1-aad7-b7e55af2cf24"
}

; Bobcat HA页面
::hab::
{
    MyArray := ["http://172.18.32.206:7000/","http://172.18.32.212:7000/"]
    for i in MyArray
    {
        run i
        Sleep 800
        SendInput "root{tab}"
        SendInput hamm
        SendInput "{enter}"
    }
}


; MES HA页面
::ham::
{
    MyArray := ["http://172.18.32.207:7000/","http://172.18.32.209:7000/","http://172.18.32.213:7000/"]
    for i in MyArray
    {
        run i
        Sleep 800
        SendInput "root{tab}"
        SendInput hamm
        SendInput "{enter}"
    }
}
#HotIf

;;; Listary快捷键 ;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;