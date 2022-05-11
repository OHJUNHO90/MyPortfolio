

public class Example
{
	public DecoratorObject decorator;

	public void Initialize()
	{
		decorator = new DecoratorObject();
		decorator.component = new WrongEntryOrder(this, new Unstable(this));
	}
	
	public void Execute()
	{
		decorator.CheckLogic(DoSomeThing);
	}
	
	void DoSomeThing()
	{
		// 비지니스 로직 수행
	}
}