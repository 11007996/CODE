namespace LuxVideoDet.Core.Utils;

public class CooldownTimer
{
    private readonly float _cooldownSeconds;
    private DateTime _lastTriggerTime;

    public CooldownTimer(float cooldownSeconds)
    {
        _cooldownSeconds = cooldownSeconds;
        _lastTriggerTime = DateTime.MinValue;
    }

    public bool CanTrigger()
    {
        var now = DateTime.Now;
        var elapsed = (now - _lastTriggerTime).TotalSeconds;
        
        if (elapsed < _cooldownSeconds)
        {
            return false;
        }
        
        _lastTriggerTime = now;
        return true;
    }

    public bool CanTriggerWithoutUpdate()
    {
        var now = DateTime.Now;
        var elapsed = (now - _lastTriggerTime).TotalSeconds;
        return elapsed >= _cooldownSeconds;
    }

    public void Trigger()
    {
        _lastTriggerTime = DateTime.Now;
    }

    public void Reset()
    {
        _lastTriggerTime = DateTime.MinValue;
    }

    public double GetElapsedSeconds()
    {
        return (DateTime.Now - _lastTriggerTime).TotalSeconds;
    }
}
