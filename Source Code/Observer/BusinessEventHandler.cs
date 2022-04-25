using System;

public class BusinessEventHandler : Singleton<BusinessEventHandler>, ISubject
{
	public event EventHandler eventArray;
	
    public BusinessEventHandler()
    {
        this.AddNode(MainSubject.Instance);
    }
	
    public void NotifyObservers(EventArgs args)
    {
        this?.eventArray(this, args);
    }
}