

public class UnitySingleton<T> : MonoBehaviour where T: Component
{
    private static T instance;
    public static T Instance
    {
        get
        {
            /*�� �ϳ��� ��ü�� ����, �����ǵ��� ����*/
            if (instance == null) 
            {
                instance = FindObjectOfType<T>();

                if (instance == null)
                {
                    GameObject obj = new GameObject();
                    obj.name = typeof(T).Name;
                    instance = obj.AddComponent<T>();
                }        
            }
            return instance;
        }
    }

   
    /*virtual�� �����Ǹ� �����Ҽ� �ֵ��� ����*/
    public virtual void Awake() {

        if (instance == null)
        {
            instance = this as T;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destory(gameObject);
        }
    }

}


