using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sound_ : MonoBehaviour
{
    public Slider Sound_BGM;
    public Slider Sound_SFX;

    public AudioSource Title;
    public AudioSource Sound_Zone;
    public float Sound_Value_BGM;
    public float Sound_Value_SFX;
    private void Awake()
    {
        var obj = FindObjectsOfType<Sound_>();
        if (obj.Length == 1)
        {
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        
    }

    void Update()
    {
        if (Sound_Zone == null) {
            Sound_Zone = GameObject.FindWithTag("SoundZone").GetComponent<AudioSource>();
        }
        if (Sound_BGM != null)
        {
            Sound_Value_BGM = Sound_BGM.value;
        }

        if (Sound_SFX != null)
        {
            Sound_Value_SFX = Sound_SFX.value;
        }
        
        Sound_Zone.volume = Sound_Value_BGM;
        Title.volume = Sound_Value_BGM;
    }
}
