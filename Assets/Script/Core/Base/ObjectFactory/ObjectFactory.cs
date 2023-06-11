using UnityEngine;
using UnityEngine.SceneManagement;



public class ObjectFactory : SingletonBase<ObjectFactory>
{
    /// <summary>
    /// 
    /// </summary>
    [SerializeField]
    private GameObject optionWindow;

    [SerializeField]
    private GameObject playerWindow;

    [SerializeField]
    private GameObject nonPlayerWindow;


    public GameObject InstanceObject(EnumList.ObjectGenelateList genelateObject) {
        GameObject obj = null;
        switch (genelateObject)
        {
            case EnumList.ObjectGenelateList.Monster:

                break;

            default:

                break;
        }
        return obj;
    }

    protected override void OnInitialize(Scene scene, LoadSceneMode mode)
    {
        base.OnInitialize(scene, mode);
        if (scene.isLoaded) {
            Debug.LogWarning($"{scene.name}씬의 로딩이 완료 되었다");
            //씬 로딩이 완료된뒤 처리할 내용
            if (GameObject.FindObjectsOfType<Canvas>().Length > 1 ) {
                //캔버스가 두개이상일경우 중복해서 창을 띄우는것이아니라 
                //하나의 캔버스만 화면에 나올수있도록 다른 캔버스를 감추는 작업이필요
            }
            else 
            {
                //캔버스가 하나일경우는 자신의 캔버스만 표시하도록 하는로직
            }
        }
        
    }
}
