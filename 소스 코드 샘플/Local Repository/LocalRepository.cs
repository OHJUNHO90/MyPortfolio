public class LocalRepository : Singleton<LocalRepository>
{
	List<IContainer> ContainerList = new List<IContainer>();
	public int GetContainerLength () => ContainerList.Count;
    
    public T GetContainer<T>(string typeName) where T : class {

        IContainer target = ContainerList.Find(t => t.GetType().Name.Equals(typeName));
        return target as T;
    }

    public void JsonToObject(string Key, string Json) {

        switch (Key) {
			//내용 생략
        }
    }

    private void AddContainerList<T>(string Json) where T : IContainer
    {
        IContainer target = ContainerList.Find(t => t.GetType().Equals(typeof(T)));
        var index = ContainerList.IndexOf(target);
		
        if (ContainerList.Contains(target))
        {
            ContainerList.RemoveAt(index);
        }

        ContainerList.Add(UnityJson.JsonToObject<T>(Json));
    }

}