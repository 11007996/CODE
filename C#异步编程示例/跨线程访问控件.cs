private void Button_Click(object sender, RoutedEventArgs e)
{
      new Thread(this.Work).Start();   //新开一个线程，避免主线程界面锁死
}

private void Work()
{
      //Thread.Sleep(5000)，相当于执行一个耗时操作
      Thread.Sleep(5000);
      this.UpdateMessage("The answer");
}
void UpdateMessage(string message)
{
      //修改TextBox的值
      Action action = () => this.TextMessage.Text = message;  //跨线程访问控件
      //Dispatcher.BeginInvoke(action)相当于排队把委托发送到UI线程的消息队列里
      Dispatcher.BeginInvoke(action);
}