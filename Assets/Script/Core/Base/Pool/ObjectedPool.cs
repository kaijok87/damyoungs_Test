using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectedPool : MonoBehaviour
{
    /// <summary>
    /// 
    /// </summary>
    public Action onDisable;

    protected virtual void OnEnable()
    {
        //위치값 초기화 기본적으로 오브젝트설정할때 트랜스폼을 0,0,0 으로 설정해야한다.
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
    }
    protected virtual void OnDisable() 
    {
        
        //비활성화 처리로직을 한번에하기위해 onDisable 델리게이터에 넣고 실행한다.
        onDisable?.Invoke();
    }
    /// <summary>
    /// 일정 시간 후에 이 게임오브젝트를 비활성화 시키는 코루틴
    /// </summary>
    /// <param name="delay">비활성화가 될때까지 걸리는 시간(기본 = 0.0f)</param>
    /// <returns></returns>
    protected IEnumerator LifeOver(float delay = 0.0f)
    {
        yield return new WaitForSeconds(delay); // delay만큼 대기하고
        gameObject.SetActive(false);            // 게임 오브젝트 비활성화
    }

}
