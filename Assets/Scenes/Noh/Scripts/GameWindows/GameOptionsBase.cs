
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

/// <summary>
/// WindowsList 
/// </summary>
public class GameOptionsBase : SingletonBase<GameOptionsBase>
{
    /// <summary>
    /// 인풋시스템연결할클래스
    /// </summary>
    KeyMouseInputSystem inputActions;

    /// <summary>
    /// 관리할 윈도우
    /// </summary>
    GameObject optionsWindow;



    protected virtual void Awake()
    {
        inputActions = new KeyMouseInputSystem();
        optionsWindow = transform.GetChild(0).gameObject;
    }
    protected virtual void OnEnable()
    {
        inputActions.Enable();
        inputActions.KeyBorad.System.performed += OnOffWindowOption;
        inputActions.KeyBorad.InvenKey.performed += OnOffInventory;
        inputActions.KeyBorad.OptionKey.performed += OnOffWindowOption;
        inputActions.Mouse.MouseClick.performed += OnLeftClick;
    }

    protected virtual void OnDisable()
    {
        inputActions.Mouse.MouseClick.performed -= OnLeftClick;
        inputActions.KeyBorad.OptionKey.performed -= OnOffWindowOption;
        inputActions.KeyBorad.InvenKey.performed -= OnOffInventory;
        inputActions.KeyBorad.System.performed -= OnOffWindowOption;
        inputActions.Disable();
    }


    /// <summary>
    /// 메뉴 창 온오프 
    /// </summary>
    /// <param name="context">입력정보</param>
    protected virtual void OnOffWindowOption(InputAction.CallbackContext context)
    {
        if (context.performed) {
            optionsWindow.SetActive(!optionsWindow.activeSelf);
        }
    }

    /// <summary>
    /// 인벤 창 온오프
    /// </summary>
    /// <param name="context"></param>
    private void OnOffInventory(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Debug.Log("인벤창로직");

        }
    }

    protected virtual void OnLeftClick(InputAction.CallbackContext context)
    {
        if (context.performed) {
        }
    }

    
}
