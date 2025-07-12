using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class Enemy : MonoBehaviour
{
    public string Name;
    public float HP;
    public float MaxHP;
    public float ATK;
    public float DEF;
    public enum EnemyNextAct { Attack, SpecialAttack, Support }
    public EnemyNextAct NextAct;

    public TextMeshProUGUI HpTxt;
    public TextMeshProUGUI NameSpace;
    public TextMeshProUGUI DEFTxt;
    public GameObject Hit;
    public Light HitLight;

    public Player player;

    public Debuf[] haveDebuf;
    public List<bool> DebufEnable;
    public Enemy[] LeftEnemy;
    public TurnManager TurnManager;

    void Start()
    {
        OnDamage(1);
    }

    void Update()
    {
        DEFTxt.text = "";
        if (HP > MaxHP) HP = MaxHP;
        if (DEF != 0) DEFTxt.text = $" + {DEF}";
        HpTxt.text = HP.ToString();
        NameSpace.text = Name;
    }

    public void OnDamage(float Damage)
    {
        StartCoroutine(HitEffect());

        if (DEF > 0)
            DEF -= Damage;

        if (DEF < 0)
        {
            HP -= DEF;
            DEF = 0;
        }

        if (HP <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log($"{Name} 사망");

        if (TurnManager.EnemyRemain.Contains(this))
        {
            TurnManager.EnemyRemain.Remove(this);
            TurnManager.EnemyTurnLeft = TurnManager.EnemyRemain.Count;
        }

        gameObject.SetActive(false); 
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

    public void StartTurn()
    {
        Debug.Log("적 행동 시작");
        switch (NextAct)
        {
            case EnemyNextAct.Attack:
                Invoke("Attack",1f);
                break;
            case EnemyNextAct.SpecialAttack:
                Invoke("SpecialAttack", 1f);
                break;
            case EnemyNextAct.Support:
                Invoke("Support", 1f);
                break;
        }
        NextAct = (EnemyNextAct)Random.Range(0, 3);
    }

    void Attack()
    {
        Debug.Log("적 공격 시작");
        bool randomAttack = Random.value > 0.5f;
        if (!randomAttack)
        {
            player.OnDamage(ATK);
            Debug.Log("적 공격");
        }
        else
        {
            Debug.Log("적 랜덤 공격");
            int Damage = Random.Range(1, 4);
            player.OnDamage(ATK * Damage);
        }
        bool canDebuf = Random.value > 0.5f;
        if (canDebuf)
        {
            Debug.Log("적 디버프");

            List<int> enabledDebufs = new List<int>();
            for (int i = 0; i < DebufEnable.Count; i++)
            {
                if (DebufEnable[i])
                    enabledDebufs.Add(i);
            }

            if (enabledDebufs.Count >= 1)
            {
                int selectedIndex = enabledDebufs[0];

                if (enabledDebufs.Count == 1)
                {
                    selectedIndex = enabledDebufs[0];
                    Debug.Log("디버프 하나뿐이라 그것만 적용");
                }
                else if (enabledDebufs.Count == 2)
                {
                    float rand = Random.value;
                    selectedIndex = (rand < 0.1f) ? enabledDebufs[1] : enabledDebufs[0];
                    Debug.Log($"2개 중 선택된 디버프: {selectedIndex}");
                }
                else
                {
                    int secondIndex = enabledDebufs[1];
                    List<int> rest = new List<int>(enabledDebufs);
                    rest.RemoveAt(1);

                    float rand = Random.value;
                    if (rand < 0.1f)
                    {
                        selectedIndex = secondIndex;
                        Debug.Log("10% 확률로 두 번째 디버프 선택됨");
                    }
                    else
                    {
                        int pick = Random.Range(0, rest.Count);
                        selectedIndex = rest[pick];
                        Debug.Log($"90% 중 나머지에서 선택된 디버프: {selectedIndex}");
                    }
                }

                if (selectedIndex < haveDebuf.Length && selectedIndex < player.haveDebuf.Length)
                {
                    player.haveDebuf[selectedIndex].Accure += haveDebuf[selectedIndex].Accure;
                    Debug.Log($"플레이어에게 디버프 {selectedIndex} 적용됨");
                }
            }
        }
        TurnManager.EnemyAct = false;
        if (TurnManager.EnemyTurnLeft == 0) TurnManager.PlayerTrun();
        else TurnManager.NextEnemyTurn();
    }
    void SpecialAttack()
    {
        Debug.Log("적 특별 공격 시작");
        bool randomAttack = Random.value > 0.5f;
        if (!randomAttack)
        {
            player.OnDamage(ATK * 2);
            Debug.Log("적 공격");
        }
        else
        {
            Debug.Log("적 랜덤 공격");
            int Damage = Random.Range(1, 4);
            player.OnDamage(ATK * Damage * 2);
        }
        bool canDebuf = Random.value > 0.5f;
        if (canDebuf)
        {
            Debug.Log("적 디버프");

            List<int> enabledDebufs = new List<int>();
            for (int i = 0; i < DebufEnable.Count; i++)
            {
                if (DebufEnable[i])
                    enabledDebufs.Add(i);
            }

            if (enabledDebufs.Count >= 1)
            {
                int selectedIndex = enabledDebufs[0];

                if (enabledDebufs.Count == 1)
                {
                    selectedIndex = enabledDebufs[0];
                    Debug.Log("디버프 하나뿐이라 그것만 적용");
                }
                else if (enabledDebufs.Count == 2)
                {
                    float rand = Random.value;
                    selectedIndex = (rand < 0.1f) ? enabledDebufs[1] : enabledDebufs[0];
                    Debug.Log($"2개 중 선택된 디버프: {selectedIndex}");
                }
                else
                {
                    int secondIndex = enabledDebufs[1];
                    List<int> rest = new List<int>(enabledDebufs);
                    rest.RemoveAt(1);

                    float rand = Random.value;
                    if (rand < 0.1f)
                    {
                        selectedIndex = secondIndex;
                        Debug.Log("10% 확률로 두 번째 디버프 선택됨");
                    }
                    else
                    {
                        int pick = Random.Range(0, rest.Count);
                        selectedIndex = rest[pick];
                        Debug.Log($"90% 중 나머지에서 선택된 디버프: {selectedIndex}");
                    }
                }

                if (selectedIndex < haveDebuf.Length && selectedIndex < player.haveDebuf.Length)
                {
                    player.haveDebuf[selectedIndex].Accure += haveDebuf[selectedIndex].Accure;
                    Debug.Log($"플레이어에게 디버프 {selectedIndex} 적용됨");
                }
            }
        }

        int Heal = Random.Range(1, 3);
        HP += Heal;
        TurnManager.EnemyAct = false;
        if (TurnManager.EnemyTurnLeft == 0) TurnManager.PlayerTrun();
        else TurnManager.NextEnemyTurn();
    }

    void Support()
    {
        Debug.Log("버프");
        int chooseEnemy = Random.Range(0, LeftEnemy.Length);
        int BuffType = Random.Range(0,3);
        switch( BuffType )
        {
            case 0:
                LeftEnemy[chooseEnemy].HP += 3;
            break;
            case 1:
                LeftEnemy[chooseEnemy].DEF += 3;
            break;
            case 2:
                LeftEnemy[chooseEnemy].ATK += 3;
            break;
        }
        TurnManager.EnemyAct = false;
        if (TurnManager.EnemyTurnLeft == 0) TurnManager.PlayerTrun();
        else TurnManager.NextEnemyTurn();
    }
}
