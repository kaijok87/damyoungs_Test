using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameOptionsWindow : GameOptionsBase, IPointerClickHandler
{
   
    /// <summary>
    /// 현재 스크립트 오브젝트 포함및 하위 오브젝트의 클릭 한위치를 얻어오는 핸들러
    /// </summary>
    /// <param name="eventData">마우스클릭 발생위치에대한 정보값이포함되있다.</param>

    void IPointerClickHandler.OnPointerClick(UnityEngine.EventSystems.PointerEventData eventData)
    {
        //스크립트가 들어간 오브젝트 자식의 클릭한 위치를 찾는 핸들러 
        Debug.Log(eventData.pointerEnter);
        //닫기 버튼클릭시 윈도우 비활성화
        if (eventData.pointerEnter.name.Equals("CloseButton")) // 더좋은 접근방법이 있으면 좋겟다.
        {
            gameObject.SetActive(false);
            //게임이 턴제라 상관없지만 
            //실시간일경우 여기에 플레이중인 내용을 멈춰두는 기능도필요함.
        }
    }

}
