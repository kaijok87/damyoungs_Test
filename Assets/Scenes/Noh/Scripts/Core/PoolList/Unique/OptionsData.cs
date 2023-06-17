using System.IO;
using UnityEngine;

/// <summary>
/// 옵션창 관리할 싱글톤 아직 제작중이라 정리안됨 일단 소리만나옴
/// </summary>
public class OptionsData : Singleton<OptionsData>
{
    /// <summary>
    /// 오디오 안끊기게 가지고다닐변수
    /// </summary>
    AudioSource audioSetting;
    protected override void Awake()
    {
        audioSetting = GetComponent<AudioSource>();
    }

    /// <summary>
    /// 볼륨조절
    /// </summary>
    /// <param name="value"></param>
    public void SetVolumn(float value)
    {
        audioSetting.volume = value;
    }
    public void SetMute(bool flag)
    {
        audioSetting.mute = flag;
    }
    public void EffectSoundPlay(EnumList.EffectSound soundType)
    {
        switch (soundType)
        {
            case EnumList.EffectSound.Explosion1:
                break;
            case EnumList.EffectSound.Explosion2:
                break;
            case EnumList.EffectSound.Explosion3:
                break;
            default:
                break;
        }
    }
    public void GetAudioFile()
    {
   
            Debug.Log(System.IO.File.Exists("Assets/SoundFiles/BGM/Piano Instrumental 1.wav"));
            AudioSource adTest = new AudioSource();
            

        //Component comp;
    }
}