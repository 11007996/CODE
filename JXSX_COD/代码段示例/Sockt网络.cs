IPEncoding ip = new IPEncoding(IPAddress.Any,6389);           //服务器ip和端口
Sockt mySockt = new Sockt(ip.AddressFamily,SocketType.Stream,ProtocolType.TCP);      //实例一个Sockt,以TCP协议传输
mySockt.bind(IP);              //绑定ip
mySockt.Listen(10);            //监听客户端消息，最多同时监听10个
mySockt.Accept();              //等待接收消息