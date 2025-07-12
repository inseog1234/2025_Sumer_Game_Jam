using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


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
    public TextMeshProUGUI NameTxt;

    public Enemy[] enemies;
    // Start is called before the first frame update
    void Start()
    {
        NameTxt.text = Name;
    }

    // Update is called once per frame
    void Update()
    {
        HPText.text = HP.ToString();
    }
    public void AttackOneEnemy(float Damage, int selectEnemy)
    {
        enemies[selectEnemy].OnDamage(Damage);
    }
    public void AttackAllEnemy(float Damage)
    {
        for(int i = 0; i < enemies.Length; i++)
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
    }
}
