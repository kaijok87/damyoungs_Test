using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseDataSingleton : Singleton<BaseDataSingleton>
    //이런식으로 오브젝트형 아이템을 여기서 관리해서 Instantiate함수로 생성해서 사용하는 방법 
    //prefab 으로 만든 것들을 싱글톤 하나로 관리하면 편할것 같습니다. 
    [SerializeField]
    GameObject item1;
    [SerializeField]
    GameObject item2;
    [SerializeField]
    GameObject item3;
    [SerializeField]
    GameObject item4;
    [SerializeField]
    GameObject item5;
    [SerializeField]
    GameObject item6;
    [SerializeField]
    GameObject item7;
    [SerializeField]
    GameObject item8;
    [SerializeField]
    GameObject item9;

    GameObject[] itemList;

    public GameObject[] ItemList
    {
        get {
            if (ItemList == null) {
                itemList = new GameObject[9];
                itemList[0] = item1;
                itemList[1] = item2;
                itemList[2] = item3;
                itemList[3] = item4;
                itemList[4] = item5;
                itemList[5] = item6;
                itemList[6] = item7;
                itemList[7] = item8;
                itemList[8] = item9;

            }
            return itemList;
        }
        private set
            {
            itemList = value; 
            }

    }
// 캐릭터 변수도 싱글톤 상속받아서 넣고 그안에 데이터를 넣어서 관리하는 방법으로 
// 각자 필요한 오브젝트 여기에서 추가하여 관리하는 코드로사용 하는방법


//유니티 배우면서 게임오브젝트도 저렇게 하드코딩말고 리스트로받아올수있는방법이있으면 게임오브젝트 생성하고 프리펩에서 넣고 하는 작업만으로 코드수정하기 편할것 같습니다. 


}
