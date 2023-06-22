using EnumList;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SaveLoadPopupWindow : Singleton<SaveLoadPopupWindow>
{
    GameObject saveButton;
    GameObject loadButton;
    GameObject copyButton;
    GameObject deleteButton;
    TextMeshProUGUI windowText;
    [SerializeField]
    bool copyCheck = false;
    public bool CopyCheck {
        get => copyCheck;
        set { 
            copyCheck = value;
            //1. 카피버튼 눌럿다가 땟따가 .
            //2. 카피팝업창에서 창닫을때 v
            //3, 카피가성공적으로 이뤄졌을때
        } 
    }
    [SerializeField]
    int oldIndex = -1;
    public int OldIndex {
        get => oldIndex; 
        set 
        {
            if (copyCheck) //카피로직일때만 값을 셋팅한다 
            {
                oldIndex = value;

            }
        }
    }
    [SerializeField]
    int newIndex = -1;
    public int NewIndex { 
        get => newIndex;
        set
        {
            newIndex = value;
        }
    }

    EnumList.PopupList processType;
    public EnumList.PopupList ProcessType { 
        get => processType;
        set => processType = value;
    }
    EnumList.SaveLoadButtonList buttonType;
    public EnumList.SaveLoadButtonList ButtonType {
        get => buttonType;
        set => buttonType = value;
    }

    protected override void Awake()
    {
        base.Awake();
        saveButton = transform.GetChild(1).GetChild(1).gameObject;
        loadButton = transform.GetChild(1).GetChild(2).gameObject;
        copyButton = transform.GetChild(1).GetChild(3).gameObject;
        deleteButton = transform.GetChild(1).GetChild(4).gameObject;
        windowText = transform.GetChild(1).GetChild(6).GetComponent<TextMeshProUGUI>();
    }


    public void OpenPopupAction(EnumList.SaveLoadButtonList type)
    {
        if (newIndex > -1) { 
            buttonType = type;
            switch (type) { 
                case EnumList.SaveLoadButtonList.SAVE:
                    saveButton.SetActive(true);
                    break;
                case EnumList.SaveLoadButtonList.LOAD:
                    loadButton.SetActive(true);
                    break;
                case EnumList.SaveLoadButtonList.COPY:
                    copyButton.SetActive(true);
                    break;
                case EnumList.SaveLoadButtonList.DELETE:
                    deleteButton.SetActive(true);
                    break;
            }
            windowText.text = $"{type} 하시겠습니까 ?";
            transform.GetChild(1).gameObject.SetActive(true);
        }
    }


}
