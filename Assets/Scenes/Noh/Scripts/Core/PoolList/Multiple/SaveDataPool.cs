using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 저장데이터화면에 보여줄 오브젝트 생산클래스
/// </summary>
public class SaveDataPool : MultipleObjectPool<ObjectIsPool>
{
    ////오브젝트는 풀아래에 있어야 안전하다..옮겼다가 개피봄
    ///// <summary>
    ///// 저장화면 오브젝트보여줄 윈도우
    ///// </summary>
    //GameObject saveDataWindow;
    //private void Awake()
    //{
    //    //saveDataWindow = GameObject.FindGameObjectWithTag("SaveList"); //세이브 파일리스트가 보여져야할 윈도우 위치
    //    saveDataWindow = GameObject.FindGameObjectWithTag("WindowList").transform.GetChild(0).GetChild(0).GetChild(1).GetChild(1).GetChild(0).GetChild(0).GetChild(0).gameObject;
    //}
    ///// <summary>
    ///// 풀이 생성될 부모위치를 바꾸기위해 추가함
    ///// 함수는 풀이 초기화 하가전에 사전작업이필요할경우 사용하면된다.
    ///// </summary>
    //protected override void SettingFuntion()
    //{
    //    setPosition = saveDataWindow.transform; //기본적으로 풀아래에 생성되지만 원하는오브젝트아래에 생성되게 변경하였음.
    //}

}
