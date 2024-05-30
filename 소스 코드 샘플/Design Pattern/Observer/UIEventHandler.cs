using System;

public class UIEventHandler : Singleton<UIEventHandler>, ISubject
{
	public event EventHandler eventArray;
	
    public UIEventHandler()
    {
        this.AddNode(MainSubject.Instance);
    }
	
    public void NotifyObservers(EventArgs args)
    {
        this?.eventArray(this, args);
    }
}