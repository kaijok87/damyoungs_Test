using EnumList;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// 저장화면에 보이는 파일 정보 
/// 다듬는 중
/// </summary>
public class SaveData : ObjectIsPool
{
    ///// <summary>
    ///// 파일명  
    ///// </summary>
    //private string fileName;
    //public string FileName { 
    //    get => fileName; 
    //    set => fileName = value;
    //}
    /// <summary>
    /// 내부적인 인덱스값
    /// </summary>
    private int fileIndex = 0;
    public int FileIndex { 
        get => fileIndex;
        set {
            fileIndex = value;
            saveFileNameObj.text += fileIndex.ToString("D3");
        }
    }
    /// <summary>
    /// 저장화면에 보일 저장파일 생성날짜
    /// </summary>
    private string createTime = "이천이십삼년유월십육일아홉시십분";
    public string CreateTime { 
        get => createTime;
        set { 
            createTime = value;
            createTimeObj.text = createTime.ToString();
        } 
    }
    /// <summary>
    /// 저장화면에 보일 캐릭터이름
    /// </summary>
    private string charcterName = "테스터";
    public string CharcterName { 
        get => charcterName;
        set { 
            charcterName = value;
            charcterInfoObj.text += sceanName;
        }
    }

    /// <summary>
    /// 저장화면에보일 캐릭터 소지금액
    /// </summary>
    private double money = 3333333333333333333;
    public double Money { 
        get => money;
        set { 
            money = value;
            charcterInfoObj.text += $"   {money}";
        }
    }
    /// <summary>
    /// 저장화면에 보일 씬정보
    /// </summary>
    private string sceanName = "따이톨?";
    public string SceanName {
        get => sceanName;
        set { 
            sceanName = value;
            saveFileNameObj.text += $"   {sceanName}";
        }
    }
    /// <summary>
    /// 오브젝트밑에 텍스트 오브젝트들 
    /// </summary>
    [SerializeField]
    TextMeshProUGUI saveFileNameObj;
    [SerializeField]
    TextMeshProUGUI charcterInfoObj;
    [SerializeField]
    TextMeshProUGUI createTimeObj;

    /// <summary>
    /// 생성시 포지션리셋 여부 셋팅
    /// </summary>
    private void Awake()
    {
        //로컬포지션 리셋하지않게 변수셋팅
        isPositionReset = false;
    }
}
