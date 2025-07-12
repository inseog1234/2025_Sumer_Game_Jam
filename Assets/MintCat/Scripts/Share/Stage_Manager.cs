using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;

public class Stage_Manager : MonoBehaviour
{
    public int Stage_Num;
    void Start()
    {
        var obj = FindObjectsOfType<Stage_Manager>();
        if (obj.Length == 1)
        {
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        } 
    }

    public void GameStart(int Stage)
    {
        Stage_Num = Stage;
        SceneManager.LoadScene("Loading");
    }

    void Update()
    {
        
    }
}
