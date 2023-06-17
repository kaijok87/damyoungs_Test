using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using TMPro;
using TreeEditor;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

//직렬화 안되는이유
//1. MonoBehaviour 를 상속받으면 직렬화 를 할수없다 
//2. [Serializable] 속성(Attribute) 를 클래스에 붙혀줘야함 
//
// -  클래스 변수를 private 로 지정하였을땐 [SerializeField] 를 붙혀줘야 JsonUtil에서 접근이 가능하다.
/// <summary>
/// 저장 로드 관련 베이스 로직 작성할 클래스 
/// try{}cahch(Exeption e){} 를 사용한이유는 게임저장은 게임상에서 단독으로 이루어지는 기능이고 
/// 여기서 오류가난다고 게임이 멈추면 안되기때문에 에러발생하더라도 멈추지않고 플레이 되도록 추가하였다.
/// </summary>
public class SaveLoadManager : Singleton<SaveLoadManager> {

    /// <summary>
    /// 저장폴더 위치 
    /// project 폴더 바로밑에있음 Assets폴더랑 동급위치
    /// </summary>
    const string saveFileDirectoryPath = "SaveFile/";

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
    /// 저장파일 위치와 파일이름및 확장자 정보를 가져온다.
    /// </summary>
    /// <param name="index">저장파일 번호</param>
    /// <returns>파일이 생성 위치와 파일명,확장자를반환한다</returns>
    private string GetFileInfo(int index) {
        //파일이름 뒤에 숫자의 형태("D3")를 001 이런형식으로 바꿔서 저장한다. 3은 숫자표시자릿수이다
        return saveFileDirectoryPath + saveFileDefaultName + index.ToString("D3") + fileExpansionName; 
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
            saveData.SaveTime = DateTime.Now.ToString(); //저장시간 저장해두기 
            //ReflashSaveFileOne();
            string toJsonData = JsonUtility.ToJson(saveData, true); //저장데이터를 Json형식으로 직렬화 하는 작업 유니티 기본제공  
            
            File.WriteAllText(GetFileInfo(index), toJsonData); //파일이없으면 생성해준다.

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

    /// <summary>
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

    /// <summary>
    /// 첫번째 인자값에 해당하는 파일에 두번째 인자값에 해당하는 파일을내용을 붙혀넣는기능
    /// 저장파일 복사 할때 사용할 예정
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
    /// 저장폴더의 저장파일리스트의 기본정보를 불러온다
    /// 저장화면 보여줄때 사용할예정
    /// </summary>
    /// <returns>저장파일 리스트FileInfo[]로 반환</returns>
    public FileInfo[] GetSaveFileList()
    {
        //pool이용하여 데이터 연결및 해제
        try
        {
            DirectoryInfo di = new DirectoryInfo(saveFileDirectoryPath); //폴더의 상세정보를 읽어온다.

            FileInfo[] fi = di.GetFiles(searchPattern, SearchOption.TopDirectoryOnly); //폴더안에 패턴을 이용하여 특정파일만 골라서 배열로 읽어온다.
                                                                                       //SearchOption.TopDirectoryOnly 옵션과 SearchOption.AllDirectories
                                                                                       //두개가있는데 Top는 폴더 바로아래내용만 읽어오는것이고 
                                                                                       //All은 폴더안의 폴더까지 뒤져서 읽어오는것이다.
            return fi;
        }
        catch (Exception e)
        {
            Debug.LogException(e);
            return null;
        }
    }

    /// <summary>
    /// 파일 저장시 화면에 데이터를 수정하기위한 함수
    /// </summary>
    /// <param name="index">파일 넘버</param>
    public bool ReflashSaveFileOne(int index) {
        //제작중
        return false;
    }



    /// <summary>
    /// index 수만큼 파일만들기. 테스트용
    /// </summary>
    /// <param name="index">최대 999개만하자</param>
    public void TestcreateFiles(int index) {

        for (int i = 0; i < index; i++) {
            if (!File.Exists(GetFileInfo(i))) { 
                File.Create(GetFileInfo(i));
            }
        }
    }

    public string TestFileDataLoad(int i) {
        
        if (File.Exists(GetFileInfo(i))) {
            return File.ReadAllText(GetFileInfo(i));
            
        }
        return null;
    }
   
   
}
