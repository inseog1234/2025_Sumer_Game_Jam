using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class Card : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] List<Sprite> Card_Sprite = new List<Sprite>();
    public RectTransform rectTransform;
    private bool isSelected = false;
    public HaveCard cardType;
    public int Type;
    private CardSystem cardSystem;
    public LayoutElement layout;

    private Image OBJ_IMG;
    [SerializeField] private TextMeshProUGUI Name_Txt;
    [SerializeField] private TextMeshProUGUI Description_Txt;
    [SerializeField] private TextMeshProUGUI Cost_Txt;

    public int Card_Type_A;
    public int Card_Type_B;

    public int originalSiblingIndex;
    public bool isHovered { get; private set; }
    public int CardNum;
    public TurnManager turnManager;

    public bool Clicked;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        cardSystem = FindObjectOfType<CardSystem>();
        OBJ_IMG = GetComponent<Image>();
    }
    public void Set_Sprite(int _Type, string Name, string Description, int _Cost)
    {
        Type = _Type;

        OBJ_IMG.sprite = Card_Sprite[Type];
        Name_Txt.text = Name;
        Description_Txt.text = Description;
        Cost_Txt.text = $"{_Cost}";
    }
    public void Set_Target(Enemy enemy)
    {
        cardSystem.Target = enemy;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!turnManager.IsPlayerTurn) return;
        if (isSelected) return;

        cardSystem.HoverEnter();
        isHovered = true;
    }

    public void OnPointStay()
    {
        if (!turnManager.IsPlayerTurn) return;
        
        if (cardSystem.Stop && Clicked)
        {
            rectTransform.localScale = Vector3.Lerp(rectTransform.localScale, new Vector3(2f, 2f, 2f), 2 * Time.deltaTime);
            rectTransform.position = Vector3.Lerp(rectTransform.position, new Vector3(rectTransform.position.x, 200, rectTransform.position.z), 2 * Time.deltaTime);
        }
        else if (!cardSystem.Stop)
        {
            rectTransform.localScale = Vector3.Lerp(rectTransform.localScale, new Vector3(2f, 2f, 2f), 2 * Time.deltaTime);
            rectTransform.position = Vector3.Lerp(rectTransform.position, new Vector3(rectTransform.position.x, 200, rectTransform.position.z), 2 * Time.deltaTime);
        }

    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (isSelected) return;
        if (!turnManager.IsPlayerTurn) return;

        cardSystem.HoverExit();

        isHovered = false;
    }


    public void OnPointerClick(PointerEventData eventData)
    {
        if (!turnManager.IsPlayerTurn) return;

        if (Type == 0 && cardSystem.Target)
        {
            StartCoroutine(SelectCard());
        }
        else if (Type != 0)
        {
            StartCoroutine(SelectCard());
        }
    }

    IEnumerator SelectCard()
    {
        cardSystem.Stop = true;
        Clicked = true;
        rectTransform.anchoredPosition3D = new Vector3(960, 330);
        rectTransform.localScale = new Vector3(1.3f, 1.3f, 1.3f);
        rectTransform.rotation = Quaternion.Euler(0, 0, 0);
        isSelected = true;
        yield return new WaitForSeconds(1.3f);
        cardSystem.Card.Remove(gameObject);
        cardSystem.Card_Sc.Remove(this);
        cardSystem.RotateCard();
        Destroy(gameObject, 0.01f);
        cardSystem.Card_Stat_Apply(Card_Type_A, Card_Type_B, cardType);
        Clicked = false;
        cardSystem.Stop = false;
    }

    void Update()
    {
        if (isHovered)
        {
            OnPointStay();
        }

        
    }
}
