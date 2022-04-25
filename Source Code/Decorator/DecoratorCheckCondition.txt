
public abstract class DecoratorCheckCondition<T> : IDecorator
{
    protected T target;
    public bool isError = false;
    protected string ErrLog = string.Empty;

    protected IDecorator Component = null;

    public DecoratorCheckCondition(T target, IDecorator component = null)
    {
        Component = component;
        this.target = target;
    }

    protected abstract bool CheckLocalLogic(params object[] datas);

    public bool CheckCondition(params object[] datas)
    {
        isError = CheckLocalLogic(datas);

        if (isError)
        {
            Debug.Log(ErrLog);
            ErrLog = string.Empty;
        }

        if (Component != null)
        {
            return Component.CheckCondition(datas) || isError;
        }
        
        return isError;
    }
}