public class GenerateClone
{
    public T Clone<T>() where T : new()
    {
        T clone = new T();
        MemberInfo[] members = clone.GetType().GetFields();

        foreach (MemberInfo member in members)
        {
            clone.GetType().GetField(member.Name).SetValue(clone, this.GetType().GetField(member.Name).GetValue(this));
        }

        return clone;
    }
}