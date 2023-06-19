using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Unity.VisualScripting;
using UnityEngine;

public class SaveDataSort : MonoBehaviour
{
    /// <summary>
    /// 현재 페이지넘버
    /// 해당값은 기본적으로 1씩 페이지입력처리시 마지막 페이지값을 가져와야한다.
    /// 페이지넘버는 0부터 시작해야한다.
    /// </summary>
    [SerializeField]
    int pageIndex = 0;          // 현재 페이지넘버 첫번째 0번
    public int PageIndex 
    {
        get => pageIndex;
        set {
            pageIndex = value;

            if( pageIndex < 0) //페이지수가 0이 첫번째이니 그보다작을순없다
            {
                pageIndex = 0;
            }else if (value > lastPageIndex) // 페이지가 최대페이지보다많게들어오면
            { 
                pageIndex = lastPageIndex; //마지막페이지를 보여준다.
            }
               
        }
    }
    /// <summary>
    /// 페이지 계산을하여 마지막 페이지를 담아둔다.
    /// </summary>
    int lastPageIndex = -1;

    /// <summary>
    /// 페이지의 마지막에 보여줄 오브젝트 갯수
    /// </summary>
    int lastPageFileViewSize = -1;

    /// <summary>
    /// 한페이지에 보일 오브젝트 갯수
    /// </summary>
    [SerializeField]
    int pageMaxSize = 8;        // 한페이지에 보일 오브젝트갯수

    /// <summary>
    /// 저장윈도우의 오브젝트 세로크기
    /// </summary>
    [SerializeField]
    float saveDataHeight = 150.0f;

    /// <summary>
    /// 저장윈도우 위치값 받아오기
    /// </summary>
    private GameObject saveWindowObject;

    /// <summary>
    /// 읽기전용으로 데이터받아오기
    /// </summary>
    private JsonGameData[] saveDatas;
   
    /// <summary>
    /// 저장윈도우 오브젝트랑 세이브데이터 미리접근해서 가져오고
    /// 저장기능, 삭제기능, 복사기능 발동시 화면 리로드 기능을 사용하기위해 액션을 연결해준다.
    /// 화면전환시 다시발동안됨
    /// </summary>
    private void Start()
    {
        saveWindowObject = SaveLoadManager.Instance.SaveLoadWindow; //저장파일오브젝트들이 담길 오브젝트 위치 //Awake 에서 못찾는다.
        saveDatas = SaveLoadManager.Instance.SaveDataList; //저장데이터가져오기
        // 저장시 저장한위치의 오브젝트가 저장한내용으로 변환을시켜줘야함 리로드
        SaveLoadManager.Instance.readAgain += reLoad;

        Debug.Log("맵이동시 실행?되냐9?");
        InitSaveObjects(); //저장화면 초기값셋팅
        SetLastPageIndex();//저장페이지 갯수와 마지막 페이지의 오브젝트 갯수 셋팅
    }

    
    /// <summary>
    /// 활성화시 풀에서 생성된 여분의 오브젝트를 숨기는기능추가
    /// </summary>
    private void OnEnable()
    {
        if (saveWindowObject != null) { //맨처음 초기화할때는 자동으로 실행되니 이후 활성화시에만 체크하자.
            SetPoolBug(); 
        }
    }

    /// <summary>
    /// 파일최대갯수 와 현재 페이지에 보이는 오브젝트갯수 를 가지고 최종페이지수와 마지막페이지에 보여질 오브젝트갯수를 가져온다.
    /// </summary>
    private void SetLastPageIndex() {
        lastPageIndex = (SaveLoadManager.Instance.MaxSaveDataLength / pageMaxSize) + 1;  //페이지 갯수 가져오기
        lastPageFileViewSize = SaveLoadManager.Instance.MaxSaveDataLength % pageMaxSize; //마지막 페이지에 보여줄 오브젝트갯수 가져오기
    }


    /// <summary>
    /// 화면에 저장관련 오브젝트들의 위치와 기본데이터 초기화 함수
    /// </summary>
    private void InitSaveObjects() {
        
        if (saveWindowObject.transform.childCount > 0) 
        { //정상적으로 초기화됬을때 풀에서 생성하여 0개이상이된다.
            if (saveWindowObject.transform.childCount < pageMaxSize)
            {// 초기화때 생성된 오브젝트수보다 페이지 최대출력 갯수가 크면 
                for (int i = saveWindowObject.transform.childCount; i < pageMaxSize; i++) //부족한만큼 가져온다.
                {
                    AddSaveDataObject();
                }

            }
        }
        for (int i = 0; i < pageMaxSize; i++)//한페이지만큼만 돌린다
        {
            saveWindowObject.transform.GetChild(i).localPosition = new Vector3(0, -(saveDataHeight * i), 0);// 창위치 잡아주기 
            SetGameObject(null, i);//초기데이터 셋팅 (*초기값 셋팅이라 무조건 0페이지부터 시작하기때문에 문제없다.*)
        }
        //트랜스폼을 변경해봤지만 사이즈델타값이 최종적으로바껴서 사이즈델타를 수정하였다.
        saveWindowObject.GetComponent<RectTransform>().sizeDelta = // 사이즈델타에 Vector2로 값을 넣으면 사이즈가 조절된다. 
                new Vector2(saveWindowObject.GetComponent<RectTransform>().rect.width, //기본사이즈 
                saveDataHeight * pageMaxSize); //한페이지 에 보일 페이지크기 정하기
        SetGameObjectList(); //데이터 셋팅
    }

    /// <summary>
    /// 저장화면에 보일 오브젝트 추가 
    /// 풀을 사용하여 가져올게 없으면 자동으로 증가한다.
    /// </summary>
    private void AddSaveDataObject() {
        MultipleObjectsFactory.Instance.GetObject(EnumList.MultipleFactoryObjectList.SaveDataPool); // 풀을 늘리기위해 겟사용  자동풀증가
    }


    /// <summary>
    /// 저장하거나 삭제하거나 복사하거나 할때 특정오브젝트 리로드 기능 
    /// </summary>
    /// <param name="saveData">저장데이터</param>
    /// <param name="index">화면인덱스</param>
    private void reLoad(JsonGameData saveData,  int index)
    {
        SetGameObject(saveData, index);//화면갱신해주기
    }


    /// <summary>
    /// 한페이지에 보이는 오브젝트들의 데이터를 다시셋팅한다.
    /// </summary>
    public void SetGameObjectList() {
        if (saveDatas == null) { // 읽어온 파일정보가없는경우 리턴
            return;
        }
        int startIndex = pageIndex * pageMaxSize; //페이지시작오브젝트위치값 가져오기
        
        int lastIndex = (pageIndex + 1) * pageMaxSize > SaveLoadManager.Instance.MaxSaveDataLength ? //페이지 마지막 값이 저장파일 마지막 인덱스 보다 크면
                            SaveLoadManager.Instance.MaxSaveDataLength : (pageIndex + 1) * pageMaxSize ; //값을 저장파일 마지막 인덱스로 셋팅

        for (int i = startIndex; i < lastIndex; i++) { //데이터를 한페이지만큼만 확인한다.
            SetGameObject(saveDatas[i],i); // 데이터를 셋팅하자 
        }
        
        SetPoolBug();//풀은 오브젝트를 2배씩늘리는데 사용안하는것들은 비활성화작업이필요해서 추가했다.

    }
    


    /// <summary>
    /// 오브젝트 위치찾아서 데이터 출력한다.
    /// </summary>
    /// <param name="index">저장파일 인덱스</param>
    public void SetGameObject(JsonGameData saveData, int index) {

        int viewObjectNumber = index - (pageIndex * pageMaxSize); //페이지별 오브젝트 위치찾기

        SaveDataObject sd = saveWindowObject.transform.GetChild(viewObjectNumber).GetComponent<SaveDataObject>(); //오브젝트 가져와서

        InitObjectIndex(sd,viewObjectNumber);

        if (saveData != null) { //저장데이터가 있는지 체크
            
            //밑으로는 데이터 셋팅 프로퍼티 Set 함수에 화면에 보여주는 기능넣어놨다.
            //보이는기능수정시 SaveDataObject 클래스수정.
            sd.FileIndex = saveData.DataIndex; 
            sd.name = saveData.CharcterInfo.CharcterName; //오브젝트가 바뀐건지 확인용
            sd.CreateTime = saveData.SaveTime;
            sd.Money = saveData.CharcterInfo.Money;
            sd.SceanName = saveData.SceanName;
        }
        else
        {
            sd.FileIndex = index;
            sd.name = $"SaveList  :: {index} :: 객체없음";
            sd.CreateTime = "";
            sd.Money = 0;
            sd.SceanName = "";
            //초기화로직
        }
    }
    /// <summary>
    /// 객체에 오브젝트번호를 부여한다 . 처음한번만
    /// </summary>
    /// <param name="sdo">객체위치</param>
    /// <param name="index">번호</param>
    private void InitObjectIndex(SaveDataObject sdo, int index) {
        if (sdo.ObjectIndex < 0) { //초기값이 -1이기때문에 초기값일때만 값을 셋팅한다.
            sdo.ObjectIndex = index;  // 오브젝트의 순서넣어놓기
        }
    }
    /// <summary>
    /// 풀에서 2배씩늘리기때문에 안쓰는파일이 생긴다 그것들을 비활성화 하는 작업.
    /// </summary>
    private void SetPoolBug() {
        for (int i = pageMaxSize; i < saveWindowObject.transform.childCount; i++)//안쓰는 파일만큼만 돌린다.
        {
            saveWindowObject.transform.GetChild(i).gameObject.SetActive(false); //안쓰는파일 숨기기 
            Debug.Log("숨겨라+");
        }
    }
}
