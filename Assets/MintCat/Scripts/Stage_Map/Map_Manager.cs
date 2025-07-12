using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Map_Manager : MonoBehaviour
{
    public List<GameObject> Stages = new List<GameObject>();
    private Stage_Manager stage_Manager;
    void Start()
    {
        stage_Manager = GameObject.FindWithTag("Stage_Manager").GetComponent<Stage_Manager>();
    }

    void Update()
    {
        // for (int i = 0; i < Stages.Count; i++) {
        //     i == 
        // }

        Stages[stage_Manager.Stage_Num - 1].SetActive(true);
    }


}
