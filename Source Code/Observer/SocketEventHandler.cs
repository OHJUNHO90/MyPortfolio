using System;

public class SocketEventHandler : Singleton<SocketEventHandler>, ISubject
{
	public event EventHandler eventArray;
	
    public SocketEventHandler()
    {
        this.AddNode(MainSubject.Instance);
    }

    public void NotifyObservers(EventArgs args)
    {
        this?.eventArray(this, args);
    }
}