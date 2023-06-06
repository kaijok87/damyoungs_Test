using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MapTestScript : SingletonBase<MapTestScript>
{
    [SerializeField]
    private GameObject[] cellList;
    //에디터창에서 드래그앤 드랍으로 리스트를 0번째부터 순서대로 넣을순있는데 
    //순석가 알수가없어서 순서를 확인하기위한 로직이 필요함
    //랜덤 셀 만들때 리스트에 담아서 사용하면 편할것 같음.
    public void TestC() {
        //Debug.Log("start");
        foreach (var a in cellList) { 
            Debug.Log(a.name);
        }
        //Debug.Log("end");
    }

}
