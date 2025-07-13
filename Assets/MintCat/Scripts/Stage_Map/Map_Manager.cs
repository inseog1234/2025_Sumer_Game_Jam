using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Map_Manager : MonoBehaviour
{
    public List<GameObject> Stages = new List<GameObject>();
    public Stage_Manager stage_Manager;
    void Start()
    {
        
    }

    void Update()
    {
        if (stage_Manager == null)
        {
            stage_Manager = GameObject.FindWithTag("Stage_Manager").GetComponent<Stage_Manager>();
        }
        else
        {
            Stages[stage_Manager.Stage_Num - 1].SetActive(true);
        }
        
    }


}
