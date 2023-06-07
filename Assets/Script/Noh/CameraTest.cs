
using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraTest : MonoBehaviour
{
    [SerializeField]
    private CharcterBaseTest charcterObj;
    [SerializeField]
    private Vector3 distancePosition;

    private Camera cameraTest;

    [SerializeField]
    private float lerp;
    private Vector3 targetPos;

    private void Awake()
    {
        if (charcterObj == null)
        {
            charcterObj = FindObjectOfType<CharcterBaseTest>();
        }
        if (cameraTest == null) {
            cameraTest = transform.GetChild(0).GetComponent<Camera>();
        }
         
    }
    private void Start()
    {
        distancePosition = charcterObj.transform.position - transform.position;
        distancePosition.Normalize();
    }


    private void LateUpdate()
    {
        InitCamera();


    }

    public Vector2 limitX;
    public Vector2 limitZ;
    private void InitCamera() {
        if (charcterObj != null)
        {
            float posX = Mathf.Clamp(charcterObj.transform.position.x + distancePosition.x, limitX.x, limitX.y);
            float posY = charcterObj.transform.position.y + distancePosition.y;
            float posZ = Mathf.Clamp(charcterObj.transform.position.z + distancePosition.z, limitZ.x, limitZ.y);

            targetPos = charcterObj.transform.position + distancePosition;
            targetPos = new Vector3(posX, posY, posZ);
            transform.position = Vector3.Lerp(transform.position, targetPos, lerp * Time.deltaTime);
            
            

        }
    }



   
    //private KeyMouseInputSystem keyMouseInputSystem;

    //private Vector3 direction = Vector3.zero;

    //private void Awake()
    //{
    //    keyMouseInputSystem = new KeyMouseInputSystem();
    //}
    //private void OnEnable()
    //{
    //    keyMouseInputSystem.Enable();
    //    keyMouseInputSystem.Camera.CameraMove.performed += OnCameraMove;
    //}


    //private void OnDisable()
    //{
    //    keyMouseInputSystem.Camera.CameraMove.performed -= OnCameraMove;
    //    keyMouseInputSystem.Disable();        
    //}

    //private void Update()
    //{
    //    //transform.Translate(Time.deltaTime * direction);
    //}


    //private void OnCameraMove(InputAction.CallbackContext context)
    //{
    //     //direction = context.ReadValue<Vector3>();

    //}



}
