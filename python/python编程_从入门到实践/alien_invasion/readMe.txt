.LOG
外星人入侵
15:56 2025/9/12

编译命令
pyinstaller -D -w --add-data "img;img" allen_invasion.py
编译后生成dist文件夹
img文件夹要与exe文件同级，需要手动拷贝调整路径
15:59 2025/9/12

alien_invasion.spec是编译文件，编译命令pyinstaller alien_invasion.spec
16:12 2025/9/12

运行命令pyinstaller allen_invasion.py，生成默认的spec编译文件，然后可以再进行修改