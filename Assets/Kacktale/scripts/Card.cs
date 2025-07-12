using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class Card : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public RectTransform rectTransform;
    private bool isSelected = false;
    private CardSystem cardSystem;
    public LayoutElement layout;

    public int originalSiblingIndex;
    public bool isHovered { get; private set; }
    public int CardNum;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        cardSystem = FindObjectOfType<CardSystem>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (isSelected) return;

        cardSystem.HoverEnter();
        isHovered = true;
    }

    public void OnPointStay()
    {
        rectTransform.localScale = Vector3.Lerp(rectTransform.localScale, new Vector3(2f, 2f, 2f), 2 * Time.deltaTime);
        rectTransform.position = Vector3.Lerp(rectTransform.position, new Vector3(rectTransform.position.x, 200, rectTransform.position.z), 2 * Time.deltaTime);

    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (isSelected) return;

        cardSystem.HoverExit();

        isHovered = false;
    }


    public void OnPointerClick(PointerEventData eventData)
    {
        StartCoroutine(SelectCard());
    }

    IEnumerator SelectCard()
    {
        rectTransform.anchoredPosition3D = new Vector3(960, 330);
        rectTransform.localScale = new Vector3(1.3f, 1.3f, 1.3f);
        isSelected = true;
        yield return new WaitForSeconds(1.3f);
        cardSystem.Card.Remove(gameObject);
        cardSystem.Card_Sc.Remove(this);
        cardSystem.RotateCard();
        Destroy(gameObject, 0.01f);
    }

    void Update()
    {
        if (isHovered)
        {
            OnPointStay();
        }
    }
}
