using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour
{
    public GameObject Logo_1;
    public GameObject Logo_2;
    public GameObject Start;
    public GameObject Quit;
    public GameObject Setting;

    public float Timer_;
    public float Timer_2;
    public float Timer_3;

    public bool AAA = false;
    public bool AAAA = false;
    public bool AAAAA = false;

    public GameObject settingPanel;

    void Air_Lojic()
    {
        Timer_ += Time.deltaTime;
        Timer_2 += Time.deltaTime;
        Timer_3 += Time.deltaTime;


        Logo_1.GetComponent<RectTransform>().anchoredPosition = new Vector2(Logo_1.GetComponent<RectTransform>().anchoredPosition.x, 250 + Mathf.Sin(Time.time * 2) * 30);

        float pulse = Mathf.Pow(Mathf.Sin(Time.time * 2f), 3f); // 빠르게 up, 천천히 down
        float scale = 1f + 0.2f * Mathf.Abs(pulse);
        Logo_2.GetComponent<RectTransform>().localScale = new Vector3(scale, scale, 1f);


        if (Timer_ > 0.5f)
        {
            if (!AAA)
            {
                Timer_ = 0;
                AAA = true;
            }

            Setting.GetComponent<RectTransform>().anchoredPosition = new Vector2(Setting.GetComponent<RectTransform>().anchoredPosition.x, -212 + Mathf.Sin(Timer_ * 1.5f) * 30);
        }

        if (Timer_2 > 1f)
        {
            if (!AAAA)
            {
                Timer_2 = 0;
                AAAA = true;
            }

            Start.GetComponent<RectTransform>().anchoredPosition = new Vector2(Start.GetComponent<RectTransform>().anchoredPosition.x, -208.8f + Mathf.Sin(Timer_2 * 1.5f) * 30);
        }

        if (Timer_3 > 1.5f)
        {
            if (!AAAAA)
            {
                Timer_3 = 0;
                AAAAA = true;
            }
            Quit.GetComponent<RectTransform>().anchoredPosition = new Vector2(Quit.GetComponent<RectTransform>().anchoredPosition.x, -211.9f + Mathf.Sin(Timer_3 * 1.5f) * 30);
        }

    }
    void Update()
    {
        Air_Lojic();

        if (Input.GetKeyDown(KeyCode.Escape)) {
            settingPanel.SetActive(false);
        }
    }

    public void Map_Start()
    {
        SceneManager.LoadScene("Stage_map");
    }

    public void Settings()
    {
        settingPanel.SetActive(true);
    }

    public void _Quit()
    {
        Application.Quit();
    }
}
