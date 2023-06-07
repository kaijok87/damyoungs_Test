using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Slot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject item_Description;

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("enter");
        item_Description.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("exit");
        item_Description.SetActive(false);
    }
}
