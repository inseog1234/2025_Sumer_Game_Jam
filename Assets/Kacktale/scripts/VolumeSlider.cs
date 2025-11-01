using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class VolumeSlider : MonoBehaviour
{
    public AudioSource bgmVolume;
    public AudioSource sfxVolume;

    public void Start()
    {
        bgmVolume.volume = VolumeScripts.Instance.bgmVolume;
        sfxVolume.volume = VolumeScripts.Instance.sfxVolume;
    }

    public void ChangeBGMVolume(float value)
    {
        VolumeScripts.Instance.bgmVolume = value;
        bgmVolume.volume = value;
    }
    public void ChangeFsxVolume(float value)
    {
        VolumeScripts.Instance.sfxVolume = value;
        sfxVolume.volume = value;
    }
}
