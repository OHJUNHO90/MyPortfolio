
public interface IFactory
{
    void Execute();
}

public interface ICreate : IFactory { }
public interface IUpdate : IFactory { }
public interface IDelete : IFactory { }


public class Factory : Singleton<Factory>
{
    public void Execute(IFactory factory)
    {
        factory.Execute();
    }
}

