using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Mob
{
    public string Name;
    public float HP;
    public float MaxHP;
    public float ATK;
    public float DEF;
    public enum EnemyNextAct { Attack,SpecialAttack,Support}
    public EnemyNextAct NextAct;
}

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

[System.Serializable]
public class Debuf
{
    public string name;
    public float Accure;
}


[CreateAssetMenu(fileName ="Mobs",menuName ="Phaze/Mobs",order =1)]
public class Mobs : ScriptableObject
{
    public Mob[] mob;
}
