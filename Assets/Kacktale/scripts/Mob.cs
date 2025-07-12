using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Player", menuName = "Phaze/Player", order = 1)]
public class PlayerStat : ScriptableObject
{
    public string Name;
    public float HP;
    public float MaxHP;
    public float ATK;
    public float DEF;
    public int cost;
    public int Maxcost;
    public Debuf[] haveDebuf;
}

[CreateAssetMenu(fileName ="Mobs",menuName ="Phaze/Mobs",order =1)]
public class Mobs : ScriptableObject
{

}
