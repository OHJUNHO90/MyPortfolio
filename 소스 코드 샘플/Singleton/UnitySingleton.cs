

public class UnitySingleton<T> : MonoBehaviour where T: Component
{
    private static T instance;
    public static T Instance
    {
        get
        {
            /*단 하나의 객체만 생성, 유지되도록 보장*/
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

   
    /*virtual로 재정의를 선택할수 있도록 선언*/
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


