private byte[] HexStringToByteArray(string s)
        {
            s = s.Replace(" ","");
            byte[] buffer = new byte[s.Length / 2];
            for(int i=0;i<s.Length;i+=2)
            {
                buffer[i / 2] = (byte)Convert.ToByte(s.Substring(i, 2), 16);
            }
            return buffer;
        }


byte[] buffer = new byte[5];
string srt ="ff 4e ab 3d 1e";
buffer = HexStringToByteArray(srt);