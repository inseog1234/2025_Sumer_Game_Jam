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
    public bool EnemyAct = false;
    // Start is called before the first frame update
    void Start()
    {
        PlayerTrun();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PlayerTrun()
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
            EnemyRemain[EnemyTurnLeft -1].StartTurn();
            EnemyTurnLeft--;
        }
    }
    public void NextEnemyTurn()
    {
        EnemyAct = true;
        EnemyRemain[EnemyTurnLeft -1].StartTurn();
        EnemyTurnLeft--;
    }
}
