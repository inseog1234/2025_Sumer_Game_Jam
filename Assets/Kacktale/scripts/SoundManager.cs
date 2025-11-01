using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource bgmSound;
    public AudioSource[] sfxSound;

    public void Start()
    {
        bgmSound.volume = VolumeScripts.Instance.bgmVolume;
        for (int i = 0; i < sfxSound.Length; i++)
        {
            sfxSound[i].volume = VolumeScripts.Instance.sfxVolume;
        }
    }
    public void Update()
    {
        if(Input.GetMouseButton(0))
        {
            sfxSound[0].Play();
        }
    }
}
