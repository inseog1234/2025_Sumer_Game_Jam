using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public bool IsPlayerTurn;
    public int giveCost;
    public int PlusCost;
    public int giveCard;
    public CardSystem cardSystem;
    public Player player;

    public Enemy[] EnemyRemain;
    public int EnemyTurnLeft;
    // Start is called before the first frame update
    void Start()
    {
        PlayerTrun();
    }

    // Update is called once per frame
    void Update()
    {
        if(!IsPlayerTurn && EnemyTurnLeft <= 0)
        {
            PlayerTrun();
        }
    }

    void PlayerTrun()
    {
        IsPlayerTurn = true;
        player.cost += giveCost + PlusCost;
        PlusCost = 0;
        for (int i = 0; i < giveCard; i++)
        {
            cardSystem.CreateCard();
        }
    }
    public void EnemyTurn()
    {
        if (IsPlayerTurn)
        {
            IsPlayerTurn = false;
            EnemyTurnLeft = EnemyRemain.Length;
            EnemyRemain[0].StartTurn();
        }
    }
}
