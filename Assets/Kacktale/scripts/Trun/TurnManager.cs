using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TurnManager : MonoBehaviour
{
    public bool IsPlayerTurn;
    public int giveCost;
    public int PlusCost;
    public int giveCard;
    public CardSystem cardSystem;
    public Player player;

    public List<Enemy> EnemyRemain;
    public int EnemyTurnLeft;
    public bool EnemyAct = false;

    public AnounceUI AnounceUI;

    public bool isBattleEnded = false;

    // Start is called before the first frame update
    void Start()
    {
        PlayerTrun();
    }

    // Update is called once per frame
    void Update()
    {
        if(EnemyRemain.Count <= 0)
        {
            SceneManager.LoadScene(1);
        }
    }

    public void PlayerTrun()
    {
        AnounceUI.TextType = 0;
        StartCoroutine(AnounceUI.AnounceAnim());
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
            cardSystem.ClearCard();

            AnounceUI.TextType = 1;
            StartCoroutine(AnounceUI.AnounceAnim());

            IsPlayerTurn = false;
            EnemyTurnLeft = EnemyRemain.Count;
            EnemyRemain[EnemyTurnLeft -1].StartTurn();
            EnemyTurnLeft--;

        }
    }
    public void NextEnemyTurn()
    {
        EnemyAct = true;

        AnounceUI.TextType = 1;
        StartCoroutine(AnounceUI.AnounceAnim());

        EnemyRemain[EnemyTurnLeft -1].StartTurn();
        EnemyTurnLeft--;

    }
}
