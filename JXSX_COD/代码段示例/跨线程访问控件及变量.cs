private void Show_Message(string sText, int iType)
{
	switch (iType)
	{
        case 0: //Error
            this.Invoke((EventHandler)(delegate { this.txtMsg.Text = SajetCommon.SetLanguage(sText); }));
            this.Invoke((EventHandler)(delegate { this.txtMsg.BackColor = Color.Yellow; }));
            this.Invoke((EventHandler)(delegate { this.txtMsg.ForeColor = Color.Red; }));
            Play("NG");
            break;
        case 1: //Warning                    
	        this.Invoke((EventHandler)(delegate { this.txtMsg.Text = SajetCommon.SetLanguage(sText); }));
	        this.Invoke((EventHandler)(delegate { this.txtMsg.BackColor = Color.DarkGoldenrod; }));
	        this.Invoke((EventHandler)(delegate { this.txtMsg.ForeColor = Color.Gainsboro; }));
	        break;
        case 2: //Confirm
	        this.Invoke((EventHandler)(delegate { this.txtMsg.Text = SajetCommon.SetLanguage(sText); }));
	        this.Invoke((EventHandler)(delegate { MessageBox.Show(sText, "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question); }));
	        break;
        default:
	        this.Invoke((EventHandler)(delegate { this.txtMsg.Text = SajetCommon.SetLanguage(sText); }));
	        this.Invoke((EventHandler)(delegate { this.txtMsg.BackColor = Color.White; }));
	        this.Invoke((EventHandler)(delegate { this.txtMsg.ForeColor = Color.Green; }));
	        Play("OK");
	        break;
    }
}





public delegate void ShowMsgHandle(string sText, int iType);
public void ShowMsg(string sText, int iType)
{
	if (this.txtMsg.InvokeRequired)
	{
		txtMsg.Invoke(new ShowMsgHandle(ShowMsg), new object[] { sText, iType });
	}
	else
	{
		switch (iType)
		{
            case 0: //Error   
	            txtMsg.Text = sText;
	            txtMsg.ForeColor = Color.Red;
	            txtMsg.BackColor = Color.Yellow;
	            Play("NG");
	            break;
            case 1: //Warning   
	            txtMsg.Text = sText;
	            txtMsg.ForeColor = Color.DarkGoldenrod;
	            txtMsg.BackColor = Color.Gainsboro;
	            break;
            case 2: //Confirm
	            txtMsg.Text = sText;
	            MessageBox.Show(sText, "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
	            break;
            default:
	            txtMsg.Text = sText;
	            txtMsg.ForeColor = Color.Green;
	            txtMsg.BackColor = Color.White;
	            Play("OK");
	            break;
        }
    }

}




private delegate void addstringDEL(string message,int stype);
private void addKpsnText(string message, int stype)
{
	if (txtKPSN.InvokeRequired)
	{
		addstringDEL d = new addstringDEL(addKpsnText);
		txtKPSN.Invoke(d, new object[] { message,stype });
	}
	else
	{
		switch (stype)
		{
			case 0:
				txtKPSN.Text = message;
				break;
			case 1:
				txtKPSN.Focus();
				txtKPSN.SelectAll();
				break;
			case 2:
				txtKPSN.Enabled = false;
				break;
			case 3:
				txtKPSN.Clear();
				break;
			case 4:
				txtKPSN.Enabled = false;
				txtKPSN.Clear();
				break;
		}

	}
}






KeyEventArgs vKPSNKeyEventArgs = new KeyEventArgs(Keys.Enter);
txtKPSN_KeyDown(sender, vKPSNKeyEventArgs);




void UpdateMessage(string message)
{
      //修改TextBox的值
      Action action = () => this.TextMessage.Text = message;  //跨线程访问控件
      //Dispatcher.BeginInvoke(action)相当于排队把委托发送到UI线程的消息队列里
      Dispatcher.BeginInvoke(action);
}