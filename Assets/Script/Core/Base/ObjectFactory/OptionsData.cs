using System.IO;
using UnityEngine;

public class OptionsData : SingletonBase<OptionsData>
{
    AudioSource audioSetting;
    protected override void Awake()
    {
        base.Awake();
        audioSetting = GetComponent<AudioSource>();
    }

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
            

        Component comp;
    }
}