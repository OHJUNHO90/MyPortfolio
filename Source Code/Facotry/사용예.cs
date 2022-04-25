
public class Example
{
    void Initailize()
    {
        PacketReceiver.Instance.Initailize();
        PathContainer.Instance.Initailize();
        LineManager.Instance.Initialize();
        MappingManager.Instance.init();

        Factory.Instance.Execute(new CreateSomeThing_1());
        Factory.Instance.Execute(new CreateSomeThing_2());
		
        Factory.Instance.Execute(new updateSomething_1());
        Factory.Instance.Execute(new updateSomething_2());
		
		Factory.Instance.Execute(new ExecuteRemovalProcess());
    }
}


public class CreateSomeThing_1 : ICreate
{
	public void Execute()
    {
		// Do something.
	}
}

public class CreateSomeThing_2 : ICreate
{
	public void Execute()
    {
		// Do something.
	}
}

public class updateSomething_1 : IUpdate
{
	public void Execute()
    {
		// Do something.
	}
}

public class updateSomething_2 : IUpdate
{
	public void Execute()
    {
		// Do something.
	}
}

public class ExecuteRemovalProcess : IDelete
{
	public void Execute()
    {
		// Do something.
	}
}