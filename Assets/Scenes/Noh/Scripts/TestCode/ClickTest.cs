
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
/// <summary>
/// 참고용 테스트코드
/// </summary>
public class ClickTest : MonoBehaviour , IPointerClickHandler
{
    KeyMouseInputSystem inputSystem;
    
    private void Awake()
    {
        inputSystem = new KeyMouseInputSystem();
       
    }
    private void OnEnable()
    {
        inputSystem.Enable();
        //inputSystem.Mouse.MouseClick.performed += OnClickTest;
        //inputSystem.Mouse.TestClick.performed += OnClickTest;
    }

    private void OnDisable()
    {
        //inputSystem.Mouse.TestClick.performed -= OnClickTest;
        //inputSystem.Mouse.MouseClick.performed -= OnClickTest;
        inputSystem.Disable();
    }


    string filePath = "Assets/ImportPack/SoundFiles";
    string fileName = "Piano Instrumental 1.wav";
    private void OnClickTest(InputAction.CallbackContext context)
    {
        string sb = filePath + fileName; 
        //UIBehaviour testVal = context.ReadValue<UIBehaviour>();
        if (File.Exists(sb)) {
            Debug.Log($"{sb} 파일이 존재함");
        }
        //Debug.Log(testVal);

    }

    /// <summary>
    /// 클릭한 위치의 데이터를 가져오기위해 사용
    /// </summary>
    /// <param name="eventData">클릭지점에 대한 데이터정보</param>
    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {



        if (eventData.pointerEnter.CompareTag("TitleButton"))
        {
            if (eventData.pointerEnter.gameObject.name.Equals(EnumList.TitleMenu.NewGame.ToString()))
            {
                LoadingScean.SceanLoading(EnumList.SceanName.Title);
            }
            else if (eventData.pointerEnter.gameObject.name.Equals(EnumList.TitleMenu.Continue.ToString()))
            {

                LoadingScean.SceanLoading(EnumList.SceanName.Item_Test);

            }
            else if (eventData.pointerEnter.gameObject.name.Equals(EnumList.TitleMenu.Options.ToString()))
            {
                GameObject optionsWindow = GameObject.FindGameObjectWithTag("WindowList"). //활성화 오브젝트
                    transform.GetChild(0).gameObject; // 비활성화 오브젝트 접근

                if (optionsWindow != null)
                {
                    optionsWindow.SetActive(true);
                }
            }
            else if (eventData.pointerEnter.gameObject.name.Equals(EnumList.TitleMenu.Exit.ToString())) 
            { 
                LoadingScean.SceanLoading(EnumList.SceanName.Ending);
            }
        }

    }
}

                //비활성화된것 못찾음 
                //optionsWindow = GameObject.FindGameObjectWithTag("Window");
                //Debug.Log(optionsWindow);
                //optionsWindow = GameObject.FindWithTag("Window");
                //Debug.Log(optionsWindow);

                //비활성화된 오브젝트를 찾기위해서는 활성화된 부모가 필요하고 그안에 넣어야함
                //GameObject optionsWindow = GameObject.FindGameObjectWithTag("Window").transform.GetChild(0).gameObject;