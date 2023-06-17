
using UnityEngine;
/*
 enum을 선언할때 보통 클래스 밖에서 선언하기때문에 그냥 네임스페이스로 묶어서 사용
 enum 이 필요한경우 여기에 추가.
 */
namespace EnumList 
{
    /// <summary>
    /// 씬리스트 
    /// 맴버변수명을 씬이름으로맞춰야한다.
    /// </summary>
    public enum SceanName 
	{
		Loading =0,
		Opening, 
		Title,
		Ending,
		CreateCharcter, //아직안만듬
        Item_Test,
        World,	//맵은 종류가많음 가장밑에 추가
	}
    /// <summary>
    /// 로딩화면에 보여줄 이미지 종류리스트
    /// </summary>
    public enum ProgressType
    {
        Bar = 0,

    }
    /// <summary>
    /// 타이틀에서 사용할 메뉴종류 리스트
    /// </summary>
    public enum TitleMenu { 
		NewGame =0,
		Continue,
		Options,
		Exit
	}
	/// <summary>
	/// BGM 리스트
	/// </summary>
	public enum BGM_List { 
	
	}
	/// <summary>
	/// 효과음 리스트
	/// </summary>
	public enum EffectSound {
        Explosion1,
        Explosion2, 
		Explosion3
	}

	
	/// <summary>
	/// 게임 객체
	/// </summary>
	public enum MultipleFactoryObjectList
	{
		SaveDataPool = 0, //저장화면에 보여줄 오브젝트생산용 풀
	}


	public enum UniqueFactoryObjectList 
	{
		OptionWindow = 0, //옵션창 esc나 o 키눌렀을때 나오게 하려고 생각중
		PlayerWindow,     //플레이어 정보창 특정단축키에 연결해서 사용하려고 생각중
		NonPlayerWindow,  //Npc 에게 말을걸고 상점이나 휴식 창같은 거에 사용될예정
		ProgressList,     //프로그래스바 종류가 늘어날시 담을 려고 넣어둠
		DefaultBGM,		  //배경음악 처리할 싱글톤담으려고 생각중
		SystemEffectSound //이팩트사운드 담을 싱글톤 아직 제작안함.
	}
}
