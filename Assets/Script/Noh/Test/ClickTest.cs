using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
public class ClickTest : MonoBehaviour
{
    KeyMouseInputSystem inputSystem;
    public GameObject options;
    private void Awake()
    {
        inputSystem = new KeyMouseInputSystem();
        
    }
    private void OnEnable()
    {
        inputSystem.Enable();
        inputSystem.Mouse.MouseLeft.performed += OnMouseDown;
    }


    private void OnDisable()
    {

        inputSystem.Mouse.MouseLeft.performed -= OnMouseDown;
        inputSystem.Disable();
    }
    public void TestClickFunc() {
        //스트링비교 안좋다!!!!!
        switch (this.name) {
            case "NewGame":
                SceneManager.LoadScene("SceanTest1");
                Debug.Log("NewGame");
                break;
            case "Continue":
                SceneManager.LoadScene("Ending_Test");
                Debug.Log("Continue");
                break;
            case "Options":
                options.gameObject.SetActive(true);
                Debug.Log("Options");
                break;
            case "Exit":
                Debug.Log("Exit");
                Application.Quit();
                break;
        }

    }
    private void OnMouseDown(InputAction.CallbackContext context)
    {
        if (context.performed) {
            if (this.name.Equals("OptionsView"))
            {
                this.gameObject.SetActive(false);

            }

        }
    }
}
