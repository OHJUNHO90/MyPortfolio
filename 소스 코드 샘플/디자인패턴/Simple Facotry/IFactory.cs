
public interface IFactory
{
    void Execute();
}

public interface IGenerator : IFactory { }
public interface IUpdater : IFactory { }
public interface IDeleter : IFactory { }


public class Factory : Singleton<Factory>
{
    public void Execute(IFactory factory)
    {
        factory.Execute();
    }
}

