using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSound : MonoBehaviour
{

    private void OnCollisionEnter(Collision collision)
    {
        //OptionsData.Instance.EffectSoundPlay(EnumList.EffectSound.Explosion1);
        OptionsData.Inst.GetAudioFile();
    }
}
