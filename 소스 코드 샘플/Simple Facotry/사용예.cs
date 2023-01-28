
public class Example
{
    void Initailize()
    {
        Factory.Instance.Execute(new GenerateSomeThing_1());
        Factory.Instance.Execute(new GenerateSomeThing_2());
		
        Factory.Instance.Execute(new updateSomething_1());
        Factory.Instance.Execute(new updateSomething_2());
		
		Factory.Instance.Execute(new DeleteSomeThing());
    }
}

public class GenerateSomeThing_1 : IGenerator
{
	public void Execute()
    {
		// Do something.
	}
}

public class GenerateSomeThing_2 : IGenerator
{
	public void Execute()
    {
		// Do something.
	}
}

public class updateSomething_1 : IUpdater
{
	public void Execute()
    {
		// Do something.
	}
}

public class updateSomething_2 : IUpdater
{
	public void Execute()
    {
		// Do something.
	}
}

public class DeleteSomeThing : IDeleter
{
	public void Execute()
    {
		// Do something.
	}
}