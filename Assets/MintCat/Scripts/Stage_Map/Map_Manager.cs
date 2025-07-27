using System.Collections.Generic;
using UnityEngine;

public class Map_Manager : MonoBehaviour
{
    public List<GameObject> Stages = new List<GameObject>();
    public Stage_Manager stage_Manager;
    void Start()
    {
        Stage_Manager.instance.MakeEnemyTurn(Stages);
    }
}
