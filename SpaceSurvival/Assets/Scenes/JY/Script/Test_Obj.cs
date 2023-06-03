using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Test_Obj : MonoBehaviour
{
    OpenInput openInput;
    public GameObject inven;
    bool IsOpen;
    private void Awake()
    {
        openInput = new OpenInput();
    }
    private void OnEnable()
    {
        openInput.Enable();
        openInput.Inventory.Open.started += OpenInventory;
    }

    private void OpenInventory(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        if (!IsOpen)
        {
            inven.SetActive(true);
            IsOpen = true;
        }
        else
        {
            inven.SetActive(false);
            IsOpen = false;
        }

    }
}
