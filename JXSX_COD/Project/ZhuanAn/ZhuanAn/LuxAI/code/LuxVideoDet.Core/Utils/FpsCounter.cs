namespace LuxVideoDet.Core.Utils;

public class FpsCounter
{
    private int _frameCount;
    private DateTime _startTime;
    private double _currentFps;
    private readonly int _updateInterval;

    public FpsCounter(int updateInterval = 10)
    {
        _updateInterval = updateInterval;
        _startTime = DateTime.Now;
    }

    public void Update()
    {
        _frameCount++;
        
        if (_frameCount % _updateInterval == 0)
        {
            var elapsed = (DateTime.Now - _startTime).TotalSeconds;
            if (elapsed > 0)
            {
                _currentFps = _frameCount / elapsed;
                _startTime = DateTime.Now;
                _frameCount = 0;
            }
        }
    }

    public double GetFps() => _currentFps;

    public void Reset()
    {
        _frameCount = 0;
        _startTime = DateTime.Now;
        _currentFps = 0;
    }
}
