using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 이팩트사운드 테스트용
/// </summary>
public class TestSound : MonoBehaviour
{

    private void OnCollisionEnter(Collision collision)
    {
        //OptionsData.Instance.EffectSoundPlay(EnumList.EffectSound.Explosion1);
        OptionsData.Instance.GetAudioFile();
    }
}
