namespace HelpDesk.Api.Status;

public class HitCounterService
{
    private int _hitCounter = 0;


    public int GetHits()
    {
        return _hitCounter;
    }

    public void LogHit()
    {

        _hitCounter++;

    }
}
