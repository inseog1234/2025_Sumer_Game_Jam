using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardSystem : MonoBehaviour
{
    private int cardNum = 0;
    
    public List<GameObject> Card;

    public GameObject CardPrefab;
    public GameObject Canvas;
    
    public HorizontalLayoutGroup CanvasGroup;
    
    // public void RotateCard()
    // {
    //     //보유 카드 3개 , 5
    //     cardNum++;
    //     //4개 표시 , 6
    //     int CenterCard = (cardNum) / 2; //3 나누기 2, 5 나누기 2
    //     if (Card.Count > 0)
    //     {
    //         for (int i = 0; i < cardNum -1 ; i++)//3번, 5번
    //         {
    //             //현재 i 가 3 - 1 보다 크면 true, 5 - 2
    //             int isLeft = i > Card.Count - CenterCard ? 1 : -1;
    //             
    //             int rotateangle = Mathf.Abs(i - CenterCard);
    //             
    //             //i 가 3-1 이면 (중앙), 5 - 2
    //             if(i == Card.Count - CenterCard) Card[i].transform.rotation = Quaternion.Euler(0, 0, 0); // 0
    //             // else if (Card.Count == 0) CreateCard();
    //             //아닐시 15 x 1 x -1 (15도)
    //             else Card[i].transform.rotation = Quaternion.Euler(0, 0, 15 * rotateangle * isLeft); // 0 
    //         }
    //     }
    //     CreateCard();
    // }

    public void CreateCard()
    {
        GameObject newCard = Instantiate(CardPrefab,transform.position,Quaternion.identity, Canvas.transform);
        // Card.Add(newCard);
        // if(Card.Count == 0) PickUpCard();
    }
    
    // public void PickUpCard()
    // {
    //     Card[cardNum -1].transform.rotation = Quaternion.Euler(0, 0, 15 * cardNum);
    // }

    public void FoldCard()
    {
        CanvasGroup.spacing = -2000f;
    }

    public void UnfoldCard()
    {
        CanvasGroup.spacing = 0f;
    }
}
