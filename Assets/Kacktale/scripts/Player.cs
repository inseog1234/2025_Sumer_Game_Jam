using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerStat stat;
    public TextMeshProUGUI HPText;
    public TextMeshProUGUI Name;

    public Enemy[] enemies;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        HPText.text = stat.HP.ToString();
        Name.text = stat.Name;
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
        if (stat.DEF > 0) stat.DEF -= Damage;
        if (stat.DEF < 0)
        {
            stat.HP -= stat.DEF;
            stat.DEF = 0;
        }
    }
}
