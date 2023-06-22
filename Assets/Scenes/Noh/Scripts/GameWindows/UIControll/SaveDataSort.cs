using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// 화면에 보이는것만 신경쓰자
/// </summary>
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
            }
            else if (value > lastPageIndex) // 페이지가 최대페이지보다많게들어오면
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
    /// 저장윈도우 오브젝트랑 세이브데이터 미리접근해서 가져오고
    /// 저장기능, 삭제기능, 복사기능 발동시 화면 리로드 기능을 사용하기위해 액션을 연결해준다.
    /// 화면전환시 다시발동안됨
    /// </summary>
    private void Start()
    {
        saveWindowObject = SaveLoadManager.Instance.SaveLoadWindow; //저장파일오브젝트들이 담길 오브젝트 위치 //Awake 에서 못찾는다.
        
        //saveDatas = SaveLoadManager.Instance.SaveDataList; //저장데이터가져오기 비동기하면 스타트타이밍에 못가져옴
        // 저장시 저장한위치의 오브젝트가 저장한내용으로 변환을시켜줘야함 리로드

        SaveLoadManager.Instance.saveObjectReflash += SetGameObject;
        SaveLoadManager.Instance.isDoneDataLoaing += SetGameObjectList; //데이터 비동기로 처리시 끝나면 화면리셋 자동으로 해주기위해 추가

        InitSaveObjects(); //저장화면 초기값셋팅
        SetLastPageIndex(); //페이징에 사용될 초기값셋팅
        SaveLoadManager.Instance.isDoneDataLoaing += SetGameObjectList;
    }
    
    /// <summary>
    /// 활성화시 풀에서 생성된 여분의 오브젝트를 숨기는기능추가
    /// </summary>
    private void OnEnable()
    {
        if (saveWindowObject != null) { //맨처음 초기화할때는 자동으로 실행되니 이후 활성화시에만 체크하자.
            SetPoolBug(pageMaxSize,saveWindowObject.transform.childCount);
            if (saveWindowObject.activeSelf) {//오브젝트가 활성화되야 접근이가능하기때문에 체크하자
                
                SetGameObjectList(SaveLoadManager.Instance.SaveDataList); //초기화 작업때 비동기로 파일데이터를 읽어오기때문에 셋팅이안됬을수도있다 
            }
        }
        
    }
    
    /// <summary>
    /// 풀에서 2배씩늘리기때문에 안쓰는파일이 생긴다 그것들을 비활성화 하는 작업.
    /// </summary>
    /// <param name="startIndex">시작 인덱스</param>
    /// <param name="lastIndex">끝 인덱스</param>
    private void SetPoolBug(int startIndex , int lastIndex)
    {
        for (int i = startIndex; i < lastIndex; i++)//안쓰는 파일만큼만 돌린다.
        {
            saveWindowObject.transform.GetChild(i).gameObject.SetActive(false); //안쓰는파일 숨기기 
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
    /// 저장화면에 보일만큼 오브젝트풀이용하여 데이터오브젝트 생성하여 나열하는 함수
    /// </summary>
    private void InitSaveObjects() {

        int childCount = saveWindowObject.transform.childCount;
        //pageMaxSize 만큼 화면에 보일 오브젝트를 생성해서 가져온다.
        if (childCount> 0) 
        { //정상적으로 초기화됬을때 풀에서 생성하여 0개이상이된다.
            if (childCount < pageMaxSize)
            {// 초기화때 생성된 오브젝트수보다 페이지 최대출력 갯수가 크면 
                for (int i = childCount; i < pageMaxSize; i++) //부족한만큼 가져온다.
                {
                    MultipleObjectsFactory.Instance.GetObject(EnumList.MultipleFactoryObjectList.SAVEDATAPOOL); // 풀을 늘리기위해 겟사용  자동풀증가
                }

            }
        }

        SetPoolBug(pageMaxSize , saveWindowObject.transform.childCount);//2배씩늘어나는것중안쓰는거 비활성화
        
        for (int i = 0; i < pageMaxSize; i++)//한페이지만큼만 돌린다
        {
            saveWindowObject.transform.GetChild(i).localPosition = new Vector3(0, -(saveDataHeight * i), 0);// 창위치 잡아주기 
            SetGameObject(null, i);//초기데이터 셋팅 (*초기값 셋팅이라 무조건 0페이지부터 시작하기때문에 문제없다.*)
        }
        
        //트랜스폼을 변경해봤지만 사이즈델타값이 최종적으로바껴서 사이즈델타를 수정하였다.
        saveWindowObject.GetComponent<RectTransform>().sizeDelta = // 사이즈델타에 Vector2로 값을 넣으면 사이즈가 조절된다. 
                new Vector2(saveWindowObject.GetComponent<RectTransform>().rect.width, //기본사이즈 
                saveDataHeight * pageMaxSize); //한페이지 에 보일 페이지크기 정하기
    }

    /// <summary>
    /// 저장하거나 삭제하거나 복사하거나 할때 특정오브젝트 리로드 기능 
    /// 수정되는 인덱스값을 넣어주면된다
    /// </summary>
    /// <param name="saveData">수정된 저장데이터</param>
    /// <param name="index">수정된 인덱스</param>
    public void SetGameObject(JsonGameData saveData, int fileIndex) {

        int viewObjectNumber = fileIndex - (pageIndex * pageMaxSize); //페이지별 오브젝트 위치찾기
        //저장오브젝트 넘버(0 ~ pageMaxSize) =  저장파일번호(0 ~ 설정한최대값) - (페이지넘버(설정값) 0번부터시작 * 한페이지에 보이는 갯수 ) 

        SaveDataObject sd = saveWindowObject.transform.GetChild(viewObjectNumber).GetComponent<SaveDataObject>(); //수정된 오브젝트 가져온다.

        sd.ObjectIndex = viewObjectNumber; //오브젝트 넘버링을 해준다 

        if (saveData != null) { //저장데이터가 있는지 체크
            
            //밑으로는 데이터 셋팅 프로퍼티 Set 함수에 화면에 보여주는 기능넣어놨다.
            //보이는기능수정시 SaveDataObject 클래스수정.
            sd.FileIndex = saveData.DataIndex; 
            sd.name = saveData.CharcterInfo[0].CharcterName; //오브젝트가 바뀐건지 확인용
            sd.CreateTime = saveData.SaveTime;
            sd.Money = saveData.CharcterInfo[0].Money;
            sd.SceanName = saveData.SceanName;
        }   
        else
        {
            //기본값 셋팅
            sd.FileIndex = fileIndex; //기본적인 파일 넘버링
            sd.name = "";
            sd.CreateTime = "";
            sd.Money = 0;
            sd.SceanName = EnumList.SceanName.NONE;
        }
    }


    /// <summary>
    /// 한페이지에 보이는 오브젝트들의 데이터를 다시셋팅한다.
    /// <param name="saveDataList">화면에 뿌릴 데이터리스트</param>
    /// </summary>
    public void SetGameObjectList(JsonGameData[] saveDataList)
    {
        if (saveDataList == null)
        { // 읽어온 파일정보가없는경우 리턴
            Debug.Log("초기화?");
            return;
        }
        int startIndex = pageIndex * pageMaxSize; //페이지시작오브젝트위치값 가져오기

        int lastIndex = (pageIndex + 1) * pageMaxSize > SaveLoadManager.Instance.MaxSaveDataLength ? //파일리스트 최대값 < 현재페이징값 * 화면에보여지는최대오브젝트수
                            SaveLoadManager.Instance.MaxSaveDataLength : //마지막페이지면 남은갯수만 셋팅
                            (pageIndex + 1) * pageMaxSize; //아니면 일반적인 페이징 

        for (int i = startIndex; i < lastIndex; i++)
        { //데이터를 한페이지만큼만 확인한다.
            SetGameObject(saveDataList[i], i); // 데이터를 셋팅하자 
        }
        int visibleEndIndex = lastIndex - startIndex; //페이지의 마지막 인덱스값을 준다.
        SetPoolBug(visibleEndIndex, saveWindowObject.transform.childCount);//풀은 오브젝트를 2배씩늘리는데 사용안하는것들은 비활성화작업이필요해서 추가했다.

    }

}
