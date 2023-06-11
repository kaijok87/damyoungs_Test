using UnityEngine;
using UnityEngine.SceneManagement;

public class SingletonBase<T> : MonoBehaviour where T : Component
{
    private static bool isShutDown = false;
    private static T instance;
    public static T Inst
    {
        get
        {
            if (isShutDown) 
            {
                Debug.LogWarning($"{typeof(T).Name} 싱글톤은 이미 삭제 중이다.");   
                return null;
            }
            if (instance == null) 
            { 
                T singleton = FindObjectOfType<T>();
                if (singleton == null) 
                {
                    GameObject gameObj = new GameObject();
                    gameObj.name = $"{typeof(T).Name} singleton";
                    singleton = gameObj.AddComponent<T>();
                }
                instance = singleton;
                DontDestroyOnLoad(instance.gameObject);
            }
            return instance;
        }
    }
    protected virtual void Awake()
    {
        if (instance == null)
        {
            instance = this as T;
            DontDestroyOnLoad(instance.gameObject);
        }
        else
        {
            if (instance != this) {
                Destroy(this.gameObject);
            }
        }


    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        OnInitialize(scene,mode);
    }

    
    private void OnApplicationQuit()
    {
        isShutDown = true;        
    }



    protected virtual void OnInitialize(Scene scene, LoadSceneMode mode)
    {
    }
}
