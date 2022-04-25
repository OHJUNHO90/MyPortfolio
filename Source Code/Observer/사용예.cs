

public class Main
{
	public void Excute()
	{
		SocketEventHandler.Instance.Excute(new NodeEventArgs("OnSocketEvent"), SomeThing);
		UIEventHandler.Instance.Excute(new NodeEventArgs("OnUIEvent"), SomeThing);
		BusinessEventHandler.Instance.Excute(new NodeEventArgs("OnBusinessEvent"), SomeThing);
	}
}


public class Example : IObserver
{
	public 생성자()
	{
		this.AddNode(SocketEventHandler.Instance);
		this.AddNode(UIEventHandler.Instance);
		this.AddNode(BusinessEventHandler.Instance);
	}
	
	public 소멸자()
	{
		this.RemoveNode(SocketEventHandler.Instance);
		this.RemoveNode(UIEventHandler.Instance);
		this.RemoveNode(BusinessEventHandler.Instance);
	}
	
	
	public void OnSocketEvent(object args)
	{
		// 로직 수행.
	}
	
	public void OnUIEvent(object args)
	{
		// 로직 수행.
	}
	
	public void OnBusinessEvent(object args)
	{
		// 로직 수행.	
	}
	
	
}