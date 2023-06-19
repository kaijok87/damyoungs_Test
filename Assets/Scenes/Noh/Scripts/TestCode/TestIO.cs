using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.IO;
using UnityEngine.InputSystem;
using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using EnumList;

public class TestIO : MonoBehaviour

{


    const string testPath = "Assets/ImportPack/SoundFiles";
    const string saveFileDirectoryPath = "SaveFile/";
    const string fileExpansionName = ".sav";
    DirectoryInfo di;

    public int index = 0;
    const string soundPath = "/BGM/Piano Instrumental 1.wav";
    protected void OnLeftClick(InputAction.CallbackContext context)
    {
        if (context.performed) {
            //폴더 없으면 만들고 시작
            if (!Directory.Exists(saveFileDirectoryPath))
            {
                Directory.CreateDirectory(saveFileDirectoryPath);
            }

            ////더미파일 생성테스트
            //for (int i= 0; i < 100; i++) {
            //    if (!File.Exists(saveFileDirectoryPath + i + fileExpansionName)) {
            //        File.Create(saveFileDirectoryPath + i + fileExpansionName);
            //        Debug.Log($"{saveFileDirectoryPath + i + fileExpansionName} 파일생성완료");
            //    }
            //}
            //SaveLoadManager.Instance.Json_SaveFile(index);


            //SaveLoadManager.Instance.Json_LoadFile(index);


        }
    }
    public static int fileIndex = -1;
    /// <summary>
    /// 테스트 데이터 생성
    /// </summary>
    TestSaveData<string> saveData;
    public void SaveTest()
    {
        if (fileIndex > -1) {
            index = fileIndex;
        }
        saveData = new TestSaveData<string>();
        saveData.TestFunc(); //삼차원배열? 구조체형식 테스트값 넣기
        SaveLoadManager.Instance.Json_Save(saveData, index);
    }
    public void LoadTest()
    {
        if (fileIndex > -1)
        {
            index = fileIndex;
        }
        SaveLoadManager.Instance.Json_Load(saveData, index);

    }
    float minY = 200.0f;
    int minlength = 8;
    
    public void FileInfos() ///파일에대한 내부정보는 풀로 따로만들어서 전부 읽어
    {


        SaveLoadManager.Instance.TestCreateFile();
        //if (fileIndex > -1)
        //{
        //    index = fileIndex;
        //}
        //long money;
        //Match match = Regex.Match(toJsonData, @"""money""\s*:\s*(\d+)"); //이정규식 작동한다.
        //if (match.Success)
        //{
        //    string moneyValue = match.Groups[1].Value;
        //    money = long.Parse(moneyValue);
        //}
        //saveData = new BaseSaveData<string>();
        //saveData.TestFunc();
        
        
        //for (int i = 0; i < minlength; i++)
        //{
        //    GameObject go = MultipleObjectsFactory.Instance.GetObject(EnumList.MultipleFactoryObjectList.SaveDataPool);
        //    go.GetComponent<RectTransform>().localPosition = new Vector3(678, -(i * minY) - 85.0f, 0);
        //    //go.transform.localPosition.Set(go.transform.localPosition.x, go.transform.localPosition.y - (i * minY), go.transform.localPosition.z);
        //    SaveData sd = go.GetComponent<SaveData>();
        //    sd.FileIndex = i;
        //    sd.SceanName = LoadingScean.SceanName;
        //}
        //x = -625   -1303
        //y = -90 -200

        
        //foreach (FileInfo saveFile in fi)
        //{

        //    Debug.Log($" 파일명 : {saveFile.Name}   ,  마지막 저장 시간 :{saveFile.LastWriteTime} "); 
        //}
        //string temp = SaveLoadManager.Instance.TestFileDataLoad(0);
    }
    //화면만들고 파일 있는지 검색하는기능 
    // 있는파일리스트를 화면에 보여주는 기능
    //파일 삭제기능
    public int fileIndexTest = -1;

    public void TitleLoad()
    {
        SaveLoadManager.Instance.TestReLoad(fileIndexTest);
        //LoadingScean.SceanLoading(EnumList.SceanName.Title);
        //WindowList.Instance.OptionsWindow.SetActive(false);
    }
    readonly int pageDataSize = 8; //한페이지에 보여줄 갯수
    
    bool isSaved = false;
    //저장된데이터 캐싱할 리스트

    public void SaveFileListInfo(int pageIndex = 0)
    {

        if (isSaved)
        {

            isSaved = false;
        }
        //}else if (sdc.saveDataList == null && sdc.fi == null)//전부읽어오는거라 한번만하고 필요한경우 파일하나씩불러오자
        //{ // 게임시작하고 처음 호출되거나 게임저장을하여 리스트갱신이 필요한경우 
        //    sdc = SaveLoadManager.Instance.GetSaveFileList();
        //}
        
        
        //SaveLoadManager.Instance.SaveLoadWindow.transform;
    }




    public void SaveAction() {
        SaveTest();
    }
    public void LoadAction() {
        LoadTest();
    }
    public void CopyAction() {
        //인덱스 두개 가져오는 것부터 
        //SaveLoadManager.Instance.Json_FileCopy();
    }
    public void DeleteAction() { 
        
    }
    public void OptionsAction() {
        FileInfos();
    }
    public void TitleAction() {
        LoadingScean.SceanLoading(EnumList.SceanName.Title ,EnumList.ProgressType.Bar);
        WindowList.Instance.OptionsWindow.SetActive(false);
    }





}
//long money;
//Match match = Regex.Match(saveJsonData, @"""money""\s*:\s*(\d+)");
//if (match.Success)
//{
//    string moneyValue = match.Groups[1].Value;
//    money = long.Parse(moneyValue);
//}
