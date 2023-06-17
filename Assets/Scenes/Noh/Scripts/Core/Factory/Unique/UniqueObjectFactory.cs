using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// 한개만 생성되는 객체들이 공통적으로 사용할 클래스 
/// 공통적으로 처리해야될 로직작성
/// 이름만 팩토리....Prefab 이라고붙이는게낫았으려나..
/// </summary>
public class UniqueObjectFactory : Singleton<UniqueObjectFactory>
{

    /// <summary>
    /// 옵션창 담을 변수
    /// 기타옵션 및  저장 불러오기 종료 등
    /// </summary>
    [SerializeField]
    private GameObject optionWindow;

    /// <summary>
    /// 플레이어 정보창 담을변수
    /// 인벤, 스테이터스 , 스킬 등
    /// </summary>
    [SerializeField]
    private GameObject playerWindow;

    /// <summary>
    /// 논플레이어 정보창 담을변수 
    /// 상점 , 크래프팅 , 대화창? 등 
    /// </summary>
    [SerializeField]
    private GameObject nonPlayerWindow;

    /// <summary>
    /// 로딩화면에 사용될 진행바
    /// </summary>
    [SerializeField]
    private GameObject progressList;

    /// <summary>
    /// 기본 배경음악 
    /// </summary>
    [SerializeField]
    private GameObject defaultBGM;

    /// <summary>
    /// 창에서 클릭이나 선택시 소리 
    /// </summary>
    [SerializeField]
    private GameObject SystemEffectSound;

    /// <summary>
    /// 필요한오브젝트 가져오는 기능. 
    /// prefab 오브젝트를 넘겨주기때문에 
    /// 받은오브젝트 포지션은 수정이안된다.
    /// </summary>
    /// <param name="type">오브젝트 종류</param>
    /// <returns>prefab 오브젝트 바로연결해주기</returns>
    public GameObject GetObject(EnumList.UniqueFactoryObjectList type) {

        switch (type) {
            case EnumList.UniqueFactoryObjectList.OptionWindow:
                return optionWindow;
            case EnumList.UniqueFactoryObjectList.PlayerWindow:
                return playerWindow;
            case EnumList.UniqueFactoryObjectList.NonPlayerWindow:
                return nonPlayerWindow;
            case EnumList.UniqueFactoryObjectList.ProgressList:
                return progressList;
            case EnumList .UniqueFactoryObjectList.DefaultBGM:
                return defaultBGM;
            default:
                return null;
        }
    }

    /// <summary>
    /// 로딩끝낫을때 처리할내용 
    /// 아직 생각안남
    /// </summary>
    /// <param name="scene">현재씬정보</param>
    /// <param name="mode">??</param>
    protected override void Init(Scene scene, LoadSceneMode mode)
    {
        base.Init(scene, mode);
        if (scene.isLoaded) {
            Debug.LogWarning($"{scene.name}씬의 로딩이 완료 되었다");

            //씬 로딩이 완료된뒤 처리할 내용
            if (GameObject.FindObjectsOfType<Canvas>().Length > 1 ) {
                //캔버스가 두개이상일경우 중복해서 창을 띄우는것이아니라 
                //하나의 캔버스만 화면에 나올수있도록 다른 캔버스를 감추는 작업 생각중
            }
            else 
            {
                //캔버스가 하나일경우는 자신의 캔버스만 표시하도록 하는로직
            }
        }
        
    }
}
