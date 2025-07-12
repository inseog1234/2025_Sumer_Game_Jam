using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Title : MonoBehaviour
{
    public GameObject Logo_1;
    public GameObject Logo_2;
    public GameObject Start;
    public GameObject Quit;
    public GameObject Setting;

    void Update()
    {
        Logo_1.GetComponent<RectTransform>().anchoredPosition = new Vector2(Logo_1.GetComponent<RectTransform>().anchoredPosition.x, 180 + Mathf.Cos(Time.deltaTime));

        float pulse = Mathf.Pow(Mathf.Sin(Time.time * 2f), 3f); // 빠르게 up, 천천히 down
        float scale = 1f + 0.2f * Mathf.Abs(pulse);
        Logo_2.GetComponent<RectTransform>().localScale = new Vector3(scale, scale, 1f);


        Start.GetComponent<RectTransform>().anchoredPosition = new Vector2(Start.GetComponent<RectTransform>().anchoredPosition.x, -208.8f + Mathf.Cos(Time.deltaTime));
        Setting.GetComponent<RectTransform>().anchoredPosition = new Vector2(Setting.GetComponent<RectTransform>().anchoredPosition.x, -212 + Mathf.Cos(Time.deltaTime));
        Quit.GetComponent<RectTransform>().anchoredPosition = new Vector2(Quit.GetComponent<RectTransform>().anchoredPosition.x, -211.9f + Mathf.Cos(Time.deltaTime));

    }

}
