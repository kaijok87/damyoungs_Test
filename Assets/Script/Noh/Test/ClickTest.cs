
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class ClickTest : MonoBehaviour , IPointerClickHandler
{
    InputKeyMouse inputSystem;
    
    private void Awake()
    {
        inputSystem = new InputKeyMouse();
       
    }
    private void OnEnable()
    {
        inputSystem.Enable();
        inputSystem.Mouse.VirtualMouse.performed += OnClickTest;
    }

    private void OnDisable()
    {
        inputSystem.Mouse.VirtualMouse.performed -= OnClickTest;
        inputSystem.Disable();
    }

    private void OnClickTest(InputAction.CallbackContext context)
    {
        Debug.Log(context);

    }

    /// <summary>
    /// 클릭한 위치의 데이터를 가져오기위해 사용
    /// </summary>
    /// <param name="eventData">클릭지점에 대한 데이터정보</param>
    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {

        if (eventData.pointerEnter.CompareTag("Button"))
        {
            if (eventData.pointerEnter.gameObject.name.Equals("NewGame"))
            {
                LoadingScean.LoadScean(EnumList.SceanName.Title);
            }
            else if (eventData.pointerEnter.gameObject.name.Equals("Continue"))
            {

                LoadingScean.LoadScean(EnumList.SceanName.World);

            }
            else if (eventData.pointerEnter.gameObject.name.Equals("Options"))
            {
                //비활성화된것 못찾음 
                //optionsWindow = GameObject.FindGameObjectWithTag("Window");
                //Debug.Log(optionsWindow);
                //optionsWindow = GameObject.FindWithTag("Window");
                //Debug.Log(optionsWindow);

                //비활성화된 오브젝트를 찾기위해서는 활성화된 부모가 필요하고 그안에 넣어야함
                GameObject optionsWindow = GameObject.FindGameObjectWithTag("Window").transform.GetChild(0).gameObject;
                if (optionsWindow != null)
                {
                    optionsWindow.SetActive(true);
                }
            }
            else if (eventData.pointerEnter.gameObject.name.Equals("Exit")) 
            { 
                LoadingScean.LoadScean(EnumList.SceanName.Ending);
            }
        }

    }
}
