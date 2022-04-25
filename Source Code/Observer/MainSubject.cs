using System;

public class MainSubject : Singleton<MainSubject>, ISubject
{
    public event EventHandler eventArray;

    public void NotifyObservers(EventArgs args)
    {
        this?.eventArray(this, args);
    }
}