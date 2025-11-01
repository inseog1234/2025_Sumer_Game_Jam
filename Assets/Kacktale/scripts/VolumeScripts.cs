using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeScripts : MonoBehaviour
{
    [Range(0,1)]
    public float bgmVolume;
    [Range(0, 1)]
    public float sfxVolume;
    public static VolumeScripts Instance;
    // Start is called before the first frame update
    void Awake()
    {
        Screen.SetResolution(1920, 1080, FullScreenMode.FullScreenWindow);
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
