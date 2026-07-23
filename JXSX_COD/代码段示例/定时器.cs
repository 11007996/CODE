private System.Timers.Timer timer;
timer= new System.Timers.Timer();
timer.Interval = 1000;
timer.Elapsed += new ElapsedEventHandler(timer_Elapsed);
timer.Start();
private void timer_Elapsed(object sender,Eventargs e)
{
	Console.WriteLine("定时器方法");
}
timer.Stop();