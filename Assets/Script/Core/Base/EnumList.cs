
public interface EnumList 
{
	/// <summary>
	/// 게임 객체
	/// </summary>
	enum ObjectGenelateList
	{
		Monster
	}
	/// <summary>
	/// 씬리스트 
	/// </summary>
	enum SceanName 
	{
		Loding,
		Opening, //아직안만듬
		Title,
		Ending,
		CreateCharcter, //아직안만듬
		World,	//맵은 종류가많음 가장밑에 추가
	}
	/// <summary>
	/// 타이틀에서 사용할 메뉴종류 리스트
	/// </summary>
	enum TitleMenu { 
		NewGame,
		Continue,
		Options,
		Exit
	}
	/// <summary>
	/// BGM 리스트
	/// </summary>
	enum BGM_List { 
	
	}
	/// <summary>
	/// 효과음 리스트
	/// </summary>
	enum EffectSound {
        Explosion1,
        Explosion2, 
		Explosion3
	}

	/// <summary>
	/// 로딩화면에 보여줄 이미지 종류리스트
	/// </summary>
	enum ProgressType { 
		Bar = 0,

	}
}
