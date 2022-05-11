
public class WrongEntryOrder : DecoratorCheckCondition<SOME THING>
{
    public WrongEntryOrder(SOME THING target, IDecorator component = null) : base(target, component)
    {
    }

    protected override bool CheckLocalLogic(params object[] datas)
    {
        //비지니스 로직 수행
        return false;
    }
}
