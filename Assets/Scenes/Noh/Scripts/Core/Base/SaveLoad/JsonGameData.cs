using System;
using System.IO;
using UnityEngine;

/// <summary>
/// 게임의 저장할 데이터를 정의 하는 클래스로 지정한다 .
/// 작성방법 
/// 1. 변수는 기본적으로 private 로 선언후 프로퍼티를 생성한다. [필수적으로 캡슐화]
/// 2. private 로 선언후 변수명에 속성을 [SerializeField] 로 설정한다  - 유니티 기본 JsonUtility 에서 데이터 입출력시 접근하기위해 사용  
/// 3. 다중배열(2차원배열이상)은 1차원배열에 구조체를 넣고 구조체안에 다시 1차원배열로 구조를 짜는식으로 구조체를 이용하여 다중배열형식으로 만들고 변수로 넣는다.
/// ex) struct A{B[] b; int i;}; struct B{C[] c; int a;};  struct C{int a;};  
/// [SerializeField]
/// A a ; 
/// public A A => a; 
/// 테스트 결과
/// 1. 해당클래스를 상속받아서 사용해보았지만 상속받은 클래스의 맴버변수는 저장은 되나 읽어올때 값이 제대로 들어가지지가 않는다. 
///  1-1. 해결: 함수호출시 상속받은 클래스를 넘기면 제대로 파싱이 된다. 로딩할때도 같은 객체를 사용하여야한다.
/// 2. JsonGameData 를 베이스로 상속받은 클래스를   유니티 싱글톤 객체에 적용하기위해 제네럴값으로 넣어봣지만 컴퍼넌트가없어서 싱글톤생성오류가난다. 
///  2-1. 해당 내용은 다른방법으로 수정을 생각하고있고 테스트중이다.
///  
/// 3. 아직 큐와 리스트 스택은 테스트를 안해보앗다.
/// 4. 
///  ************* 캐릭터 변수선언만하고 해당클래스를 상속하여 파싱함수를 제작한다.***************
///  
/// MonoBehaviour 는  [Serializable] 를 지원하지않는다.
/// </summary>

//직렬화 : 내부적으로 파싱작업에 필요하다고 한다.
[Serializable]
public class JsonGameData 
{
    
    /// <summary>
    /// 저장시간 넣어두기 
    /// </summary>
    [SerializeField]
    protected string saveTime;
    public string SaveTime { 
        get => saveTime;
        set { 
            saveTime = value;
        }
    }
    /// <summary>
    /// 캐릭터이름
    /// </summary>
    [SerializeField]
    protected string charcterName;
    public String CharcterName { 
        get => charcterName;
        protected set 
        { 
            charcterName = value;
        }
    }
    /// <summary>
    /// 캐릭터의 레벨
    /// </summary>
    [SerializeField]
    protected int level;
    public int Level { 
        get => level;
        protected set
        {
            level = value;
        }
    }
    /// <summary>
    /// 캐릭터 소지금액
    /// </summary>
    [SerializeField]
    protected long money;
    public long Money { 
        get => money;
        protected set { 
            money = value;
        }
    }
    /// <summary>
    /// 불러오기시 사용될 씬정보 
    /// </summary>
    [SerializeField]
    protected string sceanName;
    public string SceanName
    {
        get => sceanName;
        protected set
        {
            sceanName = value;
        }
    }
    /// <summary>
    /// 캐릭터 마지막위치
    /// </summary>
    [SerializeField]
    protected Vector3 characterPosition;
    public Vector3 CharcterPosition { 
        get => characterPosition;
        protected set
        {
            characterPosition = value;
        }
    }

    /// <summary>
    /// 캐릭터 소지아이템 리스트
    /// </summary>
    [SerializeField]
    protected int[] itamIndexList;
    public int[] ItemIndexList { 
        get => itamIndexList;
        protected set
        {
            ItemIndexList = value;
        }
    }
   
    /// <summary>
    /// 캐릭터습득 기술번호 인덱스리스트  
    /// </summary>
    [SerializeField]
    protected int[] skill_IndexList;
    public int[] Skill_IndexList { 
        get => skill_IndexList;
        protected set 
        { 
            skill_IndexList = value;
        }
    }
}
