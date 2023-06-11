
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameOptionsBase : MonoBehaviour
{
    InputKeyMouse inputActions;

    protected virtual void Awake()
    {
        inputActions = new InputKeyMouse();
    }
    protected virtual void OnEnable()
    {
        inputActions.Enable();
        inputActions.Player.WindowOpen.performed += OnOpenWindows;
    }
    protected virtual void OnDisable()
    {
        inputActions.Player.WindowOpen.performed -= OnOpenWindows;
        inputActions.Disable();
    }

    protected virtual void OnOpenWindows(InputAction.CallbackContext context)
    {
        if (context.action.activeControl.name.Equals("o"))
        {
            gameObject.SetActive(false);
        }
        else if (context.action.activeControl.name.Equals("i"))
        {
            LoadingScean.LoadScean(EnumList.SceanName.Title);
        }

    }

    
}
