using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Card : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler,IPointerClickHandler
{
    private bool isSelected = false;
    public RectTransform rectTransform;
    public HaveCard cardType;
    public int Type;
    public void OnPointerEnter(PointerEventData eventData)
    {
        if(!isSelected) rectTransform.anchoredPosition3D += new Vector3(0,300);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!isSelected)
        {
            var vector3 = rectTransform.anchoredPosition3D;
            vector3.y = -260;
            rectTransform.anchoredPosition3D = vector3;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        StartCoroutine(SelectCard());
    }

    IEnumerator SelectCard()
    {
        rectTransform.anchoredPosition3D = new Vector3(960,330);
        rectTransform.localScale = new Vector3(1.3f,1.3f,1.3f);
        isSelected = true;
        yield return new WaitForSeconds(1.3f);
        Destroy(gameObject);
    }
}
