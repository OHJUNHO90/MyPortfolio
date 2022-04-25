using System;

public class DecoratorObject
{
    private const bool NOT_ERROR = false;
    public IDecorator component;

    public virtual void CheckLogic(Action action = null, params object[] datas)
    {
        if (component.CheckCondition(datas) == NOT_ERROR)
        {
            action?.Invoke();
        }
    }
}
