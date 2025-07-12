// File: CardSystem.cs
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class CardSystem : MonoBehaviour
{
    private int cardNum = 0;

    public List<GameObject> Card = new List<GameObject>();
    public List<Card> Card_Sc = new List<Card>();
    public GameObject CardPrefab;
    public GameObject Canvas;
    public HorizontalLayoutGroup CanvasGroup;

    public CardType AttackType;
    public CardType DefType;
    public CardType HealType;
    public CardType BuffType;
    public CardType GatchaType;

    private float MAX_ANGLE = 20f;

    private bool isHovered = false;
    public int isHovered_Index;
    private float hoverScale = 1.2f;
    private float normalScale = 1.0f;
    private float hoverY = 0f;
    private float normalY = -260f;
    private float lerpSpeed = 10f;

    private bool SPC_Trans = false;

    void Start()
    {
        CanvasGroup.spacing = -1000f;
    }

    void Update()
    {
        bool needRefresh = !isHovered;

        for (int i = 0; i < Card.Count; i++)
        {
            var card = Card[i];
            if (!card) continue;

            RectTransform rect = card.GetComponent<RectTransform>();
            if (rect == null) continue;

            CanvasGroup.enabled = !isHovered;

            float targetScale = isHovered ? hoverScale : normalScale;
            float targetY = isHovered ? hoverY : normalY;

            rect.localScale = Vector3.Lerp(rect.localScale, Vector3.one * targetScale, Time.deltaTime * lerpSpeed);

            Vector3 pos = rect.anchoredPosition3D;

            if (Card_Sc[i].isHovered)
            {
                card.transform.SetAsLastSibling();
                isHovered_Index = i;

                pos.y = Mathf.Lerp(pos.y, 200, Time.deltaTime * lerpSpeed);
            }
            else
            {
                pos.y = Mathf.Lerp(pos.y, targetY, Time.deltaTime * lerpSpeed);
            }

            rect.anchoredPosition3D = pos;


        }

        if (needRefresh)
        {
            RotateCard();
            Card_Refresh();
        }

        MAX_ANGLE = 5 * Card.Count;

        for (int i = 0; i < Card.Count; i++)
        {

        }

        if (isHovered_Index != cardNum)
        {
            Card_Refresh();
            cardNum = isHovered_Index;
        }
    }

    void CardDetail(Card cardComp)
    { 
        int a = Random.Range(0, 5);
        switch (a)
        {
            case 0:
                int AttackCardChoose = Random.Range(0, AttackType.haveCards.Length);
                cardComp.cardType = AttackType.haveCards[AttackCardChoose];
                break;
            case 1:
                int DefCardChoose = Random.Range(0, DefType.haveCards.Length);
                cardComp.cardType = DefType.haveCards[DefCardChoose];
                break;
            case 2:
                int HealCardChoose = Random.Range(0, HealType.haveCards.Length);
                cardComp.cardType = HealType.haveCards[HealCardChoose];
                break;
            case 3:
                int BuffCardChoose = Random.Range(0, BuffType.haveCards.Length);
                cardComp.cardType = BuffType.haveCards[BuffCardChoose];
                break;
            case 4:
                int GatchaCardChoose = Random.Range(0, GatchaType.haveCards.Length);
                cardComp.cardType = GatchaType.haveCards[GatchaCardChoose];
                break;
        }
    }

    public void Card_Refresh() {
        for (int i = 0; i < Card.Count; i++)
        {
            Card[i].transform.SetSiblingIndex(i);
        }
    }

    public void CreateCard()
    {
        isHovered = false;
        GameObject newCard = Instantiate(CardPrefab, transform.position, Quaternion.identity, Canvas.transform);
        Card.Add(newCard);
        Card card_ = newCard.GetComponent<Card>();
        Card_Sc.Add(card_);
        card_.CardNum = Card.IndexOf(newCard);
        RotateCard();
        CardDetail(card_);
    }

    public void RotateCard()
    {
        int count = Card.Count;

        if (count == 1)
        {
            Card[0].transform.rotation = Quaternion.Euler(0, 0, 0);
            return;
        }

        float angleStep = (MAX_ANGLE * 2f) / (count - 1);

        for (int i = 0; i < count; i++)
        {
            float angle = -MAX_ANGLE + angleStep * i;
            Card[i].transform.rotation = Quaternion.Euler(0, 0, -angle);
        }
    }



    public void HoverEnter()
    {
        isHovered = true;
    }

    public void HoverExit()
    {
        isHovered = false;
    }

    public void FoldCard()
    {
        CanvasGroup.spacing = -1500f;
        LayoutRebuilder.ForceRebuildLayoutImmediate(CanvasGroup.GetComponent<RectTransform>());
    }

    public void UnfoldCard()
    {
        CanvasGroup.spacing = -1000;
        LayoutRebuilder.ForceRebuildLayoutImmediate(CanvasGroup.GetComponent<RectTransform>());
    }
    
}
