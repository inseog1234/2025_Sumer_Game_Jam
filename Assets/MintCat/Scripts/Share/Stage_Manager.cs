using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;

public class Stage_Manager : MonoBehaviour
{
    public static Stage_Manager instance { get; private set; }
    public int Stage_Num;

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        } 
    }

    public void GameStart(int Stage)
    {
        Stage_Num = Stage;
        SceneManager.LoadScene("Loading");
    }

    public void MakeEnemyTurn(List<GameObject> list)
    {
        list[Stage_Num - 1].SetActive(true);
    }
}
