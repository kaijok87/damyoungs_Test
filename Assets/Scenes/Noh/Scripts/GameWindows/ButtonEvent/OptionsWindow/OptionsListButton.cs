using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class OptionsListButton : MonoBehaviour
{
    public void SaveAction() 
    {
        SaveLoadPopupWindow.Instance.OpenPopupAction(EnumList.SaveLoadButtonList.SAVE);
    }

    public void LoadAction()
    {
        SaveLoadPopupWindow.Instance.OpenPopupAction(EnumList.SaveLoadButtonList.LOAD);

    }

    public void CopyAction() 
    {
        if (SaveLoadPopupWindow.Instance.NewIndex > -1)
        {
            SaveLoadPopupWindow.Instance.CopyCheck = true;

            Debug.Log("복사될 위치를 클릭하세요");
        }
        else {

            Debug.Log("복사할 파일을 클릭하세요");

        }
    }

    public void DeleteAction()
    {
        SaveLoadPopupWindow.Instance.OpenPopupAction(EnumList.SaveLoadButtonList.DELETE);
       
    }

    public void OptionsAction()
    {
        TestSaveData<string> testData = new();
        testData.TestFunc();
        testData.SetSaveData();
        SaveLoadManager.Instance.GameSaveData = testData; 
    }

    public void TitleAction()
    {
        LoadingScean.SceanLoading(EnumList.SceanName.TITLE);
    }

    

}
