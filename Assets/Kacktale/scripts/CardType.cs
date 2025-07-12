using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu(fileName = "CardType", menuName = "Card/Card")]
[System.Serializable]
public class CardType
{
    public string Name;
    public enum card { Attack,Defence,Heal,Buff,Gatcha};
    public card cardName;
    public float accureStat;
    public bool IsRandom;
    public int MinAttack;
    public int MaxAttack;
    public int cost;
}
[CreateAssetMenu(fileName ="CardTag",menuName = "Card/CardTag")]
public class HaveCard : ScriptableObject
{
    public CardType[] CardType;
}
