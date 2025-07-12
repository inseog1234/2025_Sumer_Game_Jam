using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


[System.Serializable]
public class Debuf
{
    public string DebufName;
    public float Accure;
}

public class Player : MonoBehaviour
{
    public string Name;
    public float HP;
    public float MaxHP;
    public float ATK;
    public float DEF;
    public int cost;
    public int Maxcost;
    public Debuf[] haveDebuf;

    public TextMeshProUGUI HPText;
    public TextMeshProUGUI DEFText;
    public TextMeshProUGUI ATKText;
    public TextMeshProUGUI NameTxt;
    public TextMeshProUGUI CostTxt;

    public Image[] DebufImage;
    public TextMeshProUGUI[] DebufText;

    public Enemy[] enemies;

    void Start()
    {
        NameTxt.text = Name;
        HPText.text = HP.ToString();
        OnDefence(0);
        OnAttack(0);
    }


    void Update()
    {
        if (cost > Maxcost) cost = Maxcost;
        CostTxt.text = cost.ToString();

        // 디버프 UI 갱신
        for (int i = 0; i < haveDebuf.Length && i < DebufImage.Length && i < DebufText.Length; i++)
        {
            if (haveDebuf[i].Accure > 0)
            {
                DebufImage[i].gameObject.SetActive(true);
                DebufText[i].gameObject.SetActive(true);
                DebufText[i].text = $"{haveDebuf[i].DebufName} x{haveDebuf[i].Accure}";
            }
            else
            {
                DebufImage[i].gameObject.SetActive(false);
                DebufText[i].gameObject.SetActive(false);
            }
        }
    }
    public void AttackOneEnemy(float Damage, int selectEnemy)
    {
        enemies[selectEnemy].OnDamage(Damage);
    }
    public void AttackAllEnemy(float Damage)
    {
        for (int i = 0; i < enemies.Length; i++)
        {
            enemies[i].OnDamage(Damage);
        }
    }

    public void OnDamage(float Damage)
    {
        if (DEF > 0) DEF -= Damage;
        if (DEF < 0)
        {
            HP -= DEF;
            DEF = 0;
        }
        else if (DEF == 0)
        {
            HP -= Damage;
        }
        HPText.text = HP.ToString();
        if (HP <= 0)
        {
            Time.timeScale = 0;
        }
    }

    public void OnDefence(float Defence)
    {
        
        DEF += Defence;

        if (DEF <= 0)
        {
            DEFText.text = "";
        }
        else
        {
            DEFText.text = $"+{DEF}";
        }
    }
    
    public void OnAttack(float Attack)
    {

        ATK += Attack;

        if ((ATK - 1) <= 0)
        {
            ATKText.text = "";
        }
        else
        {
            ATKText.text = $"+{ATK - 1}";
        }
    }

    public void OnHeal(float Heal)
    {

        HP += Heal;

        if (HP > MaxHP)
        {
            HP = MaxHP;
            HPText.text = HP.ToString();
        }
        else if (HP <= 0)
        {
            HP = 0;
            HPText.text = HP.ToString();
            Time.timeScale = 0;
        }
        else
        {
            HPText.text = HP.ToString();
        }
    }
}
