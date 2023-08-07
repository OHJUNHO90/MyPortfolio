
abstract public class Container<T> : IDisposable, IContainer
{
    [SerializeField] public readonly List<T> rows = new List<T>();
    
    private bool disposedValue = false; 

    protected virtual void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            if (disposing)
            {
                if (rows != null)
                {
                    rows.Clear();
                    //rows = null;
                }
            }
            disposedValue = true;
        }
    }
    
    public void Dispose()
    {
        Dispose(true);
    }
}