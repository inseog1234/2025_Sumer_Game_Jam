using System.Collections;
using TMPro;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int type;
    public Mobs Enemys;
    public TextMeshProUGUI HpTxt;
    public TextMeshProUGUI NameSpace;
    public GameObject Hit;
    public Light HitLight;

    void Start()
    {
        OnDamage(1);
    }

    void Update()
    {
        HpTxt.text = Enemys.mob[type].HP.ToString();
        NameSpace.text = Enemys.mob[type].Name;
    }

    public void OnDamage(float Damage)
    {
        StartCoroutine(HitEffect());

        if (Enemys.mob[type].DEF > 0)
            Enemys.mob[type].DEF -= Damage;

        if (Enemys.mob[type].DEF < 0)
        {
            Enemys.mob[type].HP -= Enemys.mob[type].DEF;
            Enemys.mob[type].DEF = 0;
        }
    }

    IEnumerator HitEffect()
    {
        Hit.SetActive(true);

        float duration = 0.3f;
        float elapsed = 0f;

        Vector3 startScale = Vector3.zero;
        Vector3 peakScale = Vector3.one;

        Quaternion startRot = Quaternion.Euler(0, 0, 0);
        Quaternion midRot = Quaternion.Euler(0, 0, 180);
        Quaternion endRot = Quaternion.Euler(0, 0, 360);

        Vector2 startArea = HitLight.areaSize;

        Vector2 peakArea = new Vector2(13f, 13f); // 원하는 최대 radius
        Vector2 ENDpeakArea = new Vector2(20f, 20f); // 원하는 최대 radius

        // 커지면서 회전, areaSize 증가
        while (elapsed < duration)
        {
            float t = elapsed / duration;
            Hit.transform.localScale = Vector3.Lerp(startScale, peakScale, t);
            Hit.transform.rotation = Quaternion.Lerp(startRot, midRot, t);
            HitLight.areaSize = Vector2.Lerp(startArea, peakArea, t);
            elapsed += Time.deltaTime;
            yield return null;
        }

        // 바로 작아지면서 회전 복귀
        elapsed = 0f;
        while (elapsed < duration)
        {
            float t = elapsed / duration;
            Hit.transform.localScale = Vector3.Lerp(peakScale, startScale, t);
            Hit.transform.rotation = Quaternion.Lerp(midRot, endRot, t);
            HitLight.areaSize = Vector2.Lerp(peakArea, ENDpeakArea, t);
            elapsed += Time.deltaTime;
            yield return null;
        }

        Hit.transform.localScale = startScale;
        Hit.transform.rotation = startRot;

        Hit.SetActive(false);
    }

}
