using System;
using System.Threading.Tasks;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;
using System.Collections.Generic;

//직렬화 안되는이유
//1. MonoBehaviour 를 상속받으면 직렬화 를 할수없다 
//2. [Serializable] 속성(Attribute) 를 클래스에 붙혀줘야함 
//
// -  클래스 변수를 private 로 지정하였을땐 [SerializeField] 를 붙혀줘야 JsonUtil에서 접근이 가능하다.
//    싱글톤에 들어가는 클래스타입은 제네릭으로 설정할수없다.  gameObject.AddComponent<T>(); addComponent 에서 제네릭클래스를 처리할수가없다
/// <summary>
/// 
/// 저장 로드 관련 베이스 로직 작성할 클래스 
/// try{}cahch(Exeption e){} 를 사용한이유는 게임저장은 게임상에서 단독으로 이루어지는 기능이고 
/// 여기서 오류가난다고 게임이 멈추면 안되기때문에 에러발생하더라도 멈추지않고 플레이 되도록 추가하였다.
/// </summary>
public class SaveLoadManager : Singleton<SaveLoadManager> {

    /// <summary>
    /// 저장폴더 위치 
    /// project 폴더 바로밑에있음 Assets폴더랑 동급위치
    /// </summary>
    string saveFileDirectoryPath;
    public String SaveFileDirectoryPath => saveFileDirectoryPath;


    /// <summary>
    /// 기본 저장할 파일이름
    /// </summary>
    const string saveFileDefaultName = "SpacePirateSave";


    /// <summary>
    /// 저장할 파일 확장명
    /// </summary>
    const string fileExpansionName = ".json ";


    /// <summary>
    /// 세이브 파일만 읽어오기위한 패턴 정보 
    /// ? 은 문자열 하나를 비교하는것인데 이것은된다
    /// </summary>
    //const string searchPattern = "SpacePirateSave[0-9][0-9][0-9].json"; //사용안됨     
    const string searchPattern = "SpacePirateSave???.json";


    /// <summary>
    /// 풀에서 저장윈도우로 SaveData오브젝트를 넘기기위해 저장화면윈도우 
    /// </summary>
    GameObject saveLoadWindow;
    public GameObject SaveLoadWindow => saveLoadWindow;


    /// <summary>
    /// 게임저장시 게임화면도 전환이 되야함으로 저장로직에연결해두었다.
    /// </summary>
    public Action<JsonGameData,int> readAgain;

    /// <summary>
    /// 오브젝트풀 안쓰고 그냥 데이터로만 가지고 있자.
    /// </summary>
    JsonGameData[] saveDataList;
    public JsonGameData[] SaveDataList => saveDataList;


    /// <summary>
    /// 저장파일 최대갯수 
    /// </summary>
    int maxSaveDataLength = 100;
    public int MaxSaveDataLength => maxSaveDataLength;


    /// <summary>
    /// 디렉토리 있는지 확인하는 변수
    /// </summary>
    public bool isDirectory = false; 

    /// <summary>
    /// 게임시작시 저장파일 로딩이 제대로 이루어졌는지 확인하는 변수
    /// </summary>
    public bool isFilesLoading = false;
    
    /// <summary>
    /// 게임저장화면 오브젝트 위치를 찾아오고 
    /// 게임저장될 폴더위치를 셋팅한다.
    /// </summary>
    protected override void Awake()
    {
        base.Awake();
        //게임저장화면위치
        saveLoadWindow = GameObject.FindGameObjectWithTag("WindowList").transform. 
                                GetChild(0).            //OptionsWindow
                                GetChild(0).            //OptionSettingWindow
                                GetChild(1).            //SaveLoadWindow
                                GetChild(1).            //SaveFileList
                                GetChild(0).            //Scroll View
                                GetChild(0).            //Viewport
                                GetChild(0).gameObject; //Content

    }
    //화면이동시 awake가 새로운 싱글톤객채에 의해 실행되기때문에 무거운 작업은 Start 로뺏다.
    void Start (){
        setDirectoryPath(); // 게임 저장할폴더주소값셋팅
        isDirectory = ExistsSaveDirectory();//폴더체크후 없으면 생성 
        Debug.Log($"비동기 로딩테스트 시작 {saveDataList}");
        if (!isFilesLoading)
        {
            FileListLoadind();//비동기로 파일로딩
            //isFilesLoading = SetSaveFileList();
        }
        Debug.Log($"비동기 로딩테스트 확인2번 {saveDataList}"); // 동기방식이면 이메세지가 잴아래에 떠야한다 하지만 비동기면 다르다.
    
    }
  
    /// <summary>
    /// 비동기 함수 테스트겸 적용해보앗다
    /// 비동기로선언한 async 함수는 사용가능한 Thread 하나를 얻어서 함수를실행한다.
    /// 프로그램 안에서 선언됬기때문에 프로그램과 생명주기를 같이한다 함수내용처리가안됬을때 프로그램이종료되면 같이 사라진다.
    /// </summary>
    async void FileListLoadind() //비동기 테스트
    {

        Debug.Log($"비동기 로딩테스트 확인1번 {saveDataList}");

        await TestAsyncFunction(); //이함수가 끝날때까지 기다린다.
        //await Task.Run(() => { isFilesLoading =  SetSaveFileList(); }); //이함수가 끝날때까지 대기 

    }

    /// <summary>
    /// 비동기 함수 테스트 
    /// 파일저장을 비동기로 하기위해 테스트 코드작성
    /// 코루틴과 비슷하지만 기본적으로 개념은 다르다.
    /// </summary>
    async Task TestAsyncFunction() {
        Debug.Log("비동기 테스트함수 시작");
        await Task.Run(() => { isFilesLoading = SetSaveFileList(); }); //이함수가 끝날때까지 대기 
        Debug.Log("비동기 테스트함수 진행");
        await Task.Delay( 3000 ); //3초 기다리기

    }

    /// <summary>
    /// 저장파일 위치와 파일이름및 확장자 정보를 가져온다.
    /// </summary>
    /// <param name="index">저장파일 번호</param>
    /// <returns>파일이 생성 위치와 파일명,확장자를반환한다</returns>
    private string GetFileInfo(int index) {
        //파일이름 뒤에 숫자의 형태("D3")를 001 이런형식으로 바꿔서 저장한다. 3은 숫자표시자릿수이다
        return $"{saveFileDirectoryPath}{saveFileDefaultName}{index.ToString("D3")}{fileExpansionName}"; 
    }
   

    /// <summary>
    /// 저장될 폴더위치값 셋팅
    /// </summary>
    private void setDirectoryPath()
    {
        //유니티 에디터가아닌 실행환경은 Applicaion에서 자동으로 폴더를만들어준다. 
    #if UNITY_EDITOR //유니티 에디터에서만의 설정
        saveFileDirectoryPath = $"{Application.dataPath}/SaveFile/";
        //Application.dataPath 런타임때 결정된다.
    #else //유니티에디터가 아닐때의 설정 
        saveFileDirectoryPath = Application.persistentDataPath + "/SaveFile/"; //유니티 에디터가아닌 실행환경은 Applicaion에서 자동으로 폴더를만들어준다. 
    #endif

    }


    /// <summary>
    /// 파일저장시 기본정보를 입력한다.
    /// </summary>
    /// <param name="saveData">저장 파일</param>
    /// <param name="index">저장 파일번호</param>
    private void setDefaultInfo(JsonGameData saveData, int index) {
        saveData.DataIndex = index;  ///파일인덱스 저장
        saveData.SaveTime = DateTime.Now.ToString();// 저장되는 시간을 저장한다
        saveData.SceanName = SceneManager.GetActiveScene().name; //씬이름을 저장한다.
    }


    /// <summary>
    /// 저장데이터 파일로 빼는 작업
    /// </summary>
    /// <param name="saveData">저장 데이터 클래스</param>
    /// <param name="index">저장파일 번호</param>
    /// <returns>파일저장 성공여부</returns>
    public bool Json_Save(JsonGameData saveData, int index)
    {

        try
        {
            setDefaultInfo(saveData, index);// 파일저장시 기본정보를 저장한다.
            string toJsonData = JsonUtility.ToJson(saveData, true); //저장데이터를 Json형식으로 직렬화 하는 작업 유니티 기본제공  
            File.WriteAllText(GetFileInfo(index), toJsonData); //파일이없으면 생성해준다.
            readAgain?.Invoke(saveData,index); //저장후 화면에 저장된 데이터로 표시하기위해 실행
            return true;
        }
        catch (Exception e)
        {
            Debug.LogException(e); 
                                   
            return false;
        }
    }

   /// <summary>
   /// 저장파일 읽어오는 함수 
   /// </summary>
   /// <param name="saveData">저장데이터 클래스</param>
   /// <param name="index">저장파일 번호</param>
   /// <returns>읽어오기 성공여부</returns>
    public bool Json_Load(JsonGameData saveData, int index ) 
    {
        try 
        {
            if (File.Exists(GetFileInfo(index)))//저장폴더에서 파일이 있는지 체크 
            {
                string saveJsonData = File.ReadAllText(GetFileInfo(index)); //저장파일을 스트링으로 읽어오기 
                //제약이 많음. 다중배열 지원안되고, private 맴버변수들 접근하려면  [SerializeField] 선언해야함. 클래스가 [Serializable] 선언되야함.
                saveData = JsonUtility.FromJson<JsonGameData>(saveJsonData); //변환된 스트링 데이터를 JsonUtility 내부적으로 직렬화처리후 클래스에 자동파싱 
                return true;
            }
            else
            {
                Debug.LogWarning($"{GetFileInfo(index)} 해당위치에 파일이 존재하지 않습니다.");
            }
            return false;
        }
        catch(Exception e)
        {
            Debug.LogException(e); // 예상치 못한 오류 발생시 아직발견못함. 클래스 구조만 잘맞추면 대체로 잘된다.
            return false;
        }
    }

    /// <summary> 테스트아직안됨
    /// 저장된 파일 삭제 
    /// </summary>
    /// <param name="index">저장파일 번호</param>
    /// <returns>파일삭제 성공여부</returns>
    public bool Json_FileDelete(int index) 
    {
        try {
        
            if (File.Exists(GetFileInfo(index)))//파일있는지 확인 
            {
                File.Delete(GetFileInfo(index));//있으면 삭제 
                readAgain?.Invoke(null, index); //삭제후 화면에 삭제된 데이터로 표시하기위해 실행
                return true;
            }
            else
            {
                Debug.LogWarning($"{GetFileInfo(index)} 해당위치에 파일이 존재하지 않습니다.");
                return false;           
            }
        }catch(Exception e)
        {
            //파일 IO관련은 알수없는 오류가 발생할수 있으니 일단 걸어둔다.
            Debug.LogException (e);
            return false;
        }
    }

    /// <summary> 테스트아직안됨
    /// 첫번째 인덱스값에 해당하는 파일을 두번째 인덱스값에 해당하는 파일을내용으로 붙혀넣는기능
    /// 저장 파일 복사 
    /// </summary>
    /// <param name="oldIndex">복사할 파일번호</param>
    /// <param name="newIndex">복사될 파일번호</param>
    /// <returns>복사하기 성공여부</returns>
    public bool Json_FileCopy(int oldIndex , int newIndex) 
    {
        try {
            string oldFullFilePathAndName = GetFileInfo(oldIndex);// 복사할 파일위치
            string newFullFilePathAndName = GetFileInfo(newIndex);// 복사될 파일위치
            if (File.Exists(oldFullFilePathAndName))//복사할 파일위치에 파일이있는지 확인
            {
                if (!File.Exists(newFullFilePathAndName))//복사될 파일 위치에 파일이있는지 확인
                {
                    File.Create(newFullFilePathAndName);//없으면 생성  생성안할시 파일오류발생
                }
                string tempa = File.ReadAllText(oldFullFilePathAndName);//복사할 파일을 읽어오기

                File.WriteAllText(newFullFilePathAndName,tempa);//복사될 파일에 내용추가

                readAgain?.Invoke(JsonUtility.FromJson<JsonGameData>(tempa), newIndex); //파일내용이바뀌면 다시화면에 바뀐내용보여줘야함으로 추가
                return true;
            }
            else
            {
               
                Debug.LogWarning($"{GetFileInfo(oldIndex)} 해당위치에 파일이 존재하지 않습니다.");
                return false;
            }
        }
        catch (Exception e) {
            Debug.LogException (e);
            return false;
        }


    }
   
    /// <summary>
    /// 저장폴더 생성여부 확인하고 없으면 생성하는로직
    /// </summary>
    /// <returns>폴더 존재 여부 반환</returns>
    public bool ExistsSaveDirectory() {
        try {
            if (!Directory.Exists(saveFileDirectoryPath))//폴더 위치에 폴더가 없으면 
            {
                Directory.CreateDirectory(saveFileDirectoryPath);//폴더를 만든다.
            }
            
            return true; //기본적으로 여기로만 온다.
        }
        catch (Exception e) { 
            //여기올일은 없겠지만 내가모르는 경우가 있을수있으니 일단 걸어둔다.
            Debug.LogException(e);
            return false;
        }
    }
    

    /// <summary>
    /// 게임시작시 저장화면 보여줄때 사용할 데이터들 찾아오기 
    /// List 형식이아닌 배열형식으로 작성하였다 
    /// 이유는 배열형식으로 순서를 맞춰서 검색이 빠르게 이뤄지도록 하기위해서다.
    /// </summary>
    public bool SetSaveFileList()
    {
        try
        {
            string[] jsonDataList = Directory.GetFiles(saveFileDirectoryPath,searchPattern); // 폴더안에 파일들 정보를 읽어서 
            if (jsonDataList.Length == 0) //읽어올파일이없으면 리턴~
            {
                Debug.LogWarning($"폴더에 저장 파일이 없습니다 ");
                return false;
            }
            // 리스트 사용시 게임화면에서 세이브창 바뀔때마다 시간이 걸릴가능성이있다 이중포문이 무조건들어가게된다.  
            JsonGameData[] tempSaveDataList = new JsonGameData[maxSaveDataLength]; //배열로 처리시 포문한번으로해결된다.

            int checkDumyCount = 0;
            
            for (int i = 0; i < tempSaveDataList.Length; i++) { //폴더에서 찾아온 파일갯수만큼 반복문돌리고
                if (i > jsonDataList.Length) { // 파일이 더이상 존재하지않는경우 빠져나간다.
                    break ;
                }
                int tempIndex = GetIndexString(jsonDataList[i - checkDumyCount]);//중간에 빈공간을 체크하기위해 checkDumyCount를 적용한다.
                if (tempIndex == i) // 파일 인덱스와 포문순서가 같으면 셋팅한다 
                {
                    string jsonData = File.ReadAllText(jsonDataList[i - checkDumyCount]); // 파일에 접근해서 데이터뽑아보고 
                    if (jsonData.Length == 0 || jsonData == null)
                    {
                        //기본적으로 실행되면안된다. 이것이 실행되면 파일은있으나 파일내용이없다는것이다   
                        Debug.LogWarning($"{i - checkDumyCount} 번째 파일 내용이 없습니다 파일내용 : {jsonDataList[i - checkDumyCount]} ");
                    }
                    else
                    {
                        //더미파일이 아닌경우 작업시작
                        JsonGameData temp = JsonUtility.FromJson<JsonGameData>(jsonData); // 유틸사용해서 파싱작업후 저장 

                        if (temp == null) //파싱안됬을때  
                        {
                            //기본적으로 실행되면안된다.  저장로직을 확인해보거나 저장데이터가 정상적으로 만들어지지않아서발생한다.
                            Debug.LogWarning($"{i - checkDumyCount} 번째 파일 내용을 Json으로 변환할수 없습니다  파일내용 : {jsonDataList[i - checkDumyCount]} ");
                        }
                        else
                        {
                            tempSaveDataList[i] = temp;//제대로처리되면 저장한다.
                            
                        }
                    }
                }
                else
                {
                    checkDumyCount++;// 파일인덱스번호맞추기위해 못찾으면 더미값추가
                }
            }

            //초기로딩(게임시작)시에 처리되게해놔서 조금시간이걸리는작업이라도 해당방법으로 진행하였다.
            saveDataList = tempSaveDataList;
            
            return true;
        }
        catch (Exception e)
        {
            Debug.LogException(e);
            return false;
        }
    }

    /// <summary>
    /// 파일명마지막에 카운팅한 숫자 3자리 가져오기
    /// </summary>
    /// <param name="findText">저장파일실제주소값과 이름 확장자명까지</param>
    /// <returns>파일명의 인덱스로줬던 숫자를 뽑아서반환 -1이면 파일없음</returns>
    public int GetIndexString(string findText)
    {
        if (findText != null)
        {
            int findDotPoint = findText.IndexOf('.'); //무조건 ***.json 이고 .은 하나밖에있을수없으니 일단 .위치찾는다
            int temp = int.Parse(findText.Substring(findDotPoint-3, 3));//점위치에서 3칸앞으로 이동후 3개의 값을 가져오면 숫자 3자리가져올수있다.
            return temp;
        }
        else {
            return -1;
        }
    }


    /// <summary>
    /// 테스터클래스로 테스트데이터생성
    /// </summary>
    public void TestCreateFile() {

        for (int i = 0; i < maxSaveDataLength; i++)
        {
            TestSaveData<string> jb = new TestSaveData<string>();
            jb.TestFunc();
            setDefaultInfo(jb, i);// 파일저장시 기본정보를 저장한다.
            string temp = JsonUtility.ToJson(jb);

            File.WriteAllText(GetFileInfo(i),temp);
            
        }
    }

    public void TestReLoad(int i) {
        readAgain?.Invoke(saveDataList[i],i);
    }
}
