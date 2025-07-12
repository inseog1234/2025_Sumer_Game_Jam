using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


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

    public CameraController cam; // 인스펙터에 할당
    public AnounceUI anounceUI; // 인스펙터에 할당
    public GameObject anounceUIObj; // AnounceUI가 있는 GameObject (예: Panel 등)
    public Canvas rootCanvas; // 전체 UI가 포함된 최상위 Canvas
    public TurnManager turnManager;

    public Animator animator;

    public AudioSource Player_Hit;
    public AudioSource Player_Attack;
    // Start is called before the first frame update
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
        animator.Play("Hurt");
        if (DEF > 0) DEF -= Damage;
        if (DEF < 0)
        {
            HP += DEF;
            DEF = 0;
        }
        else if (DEF == 0)
        {
            HP -= Damage;
        }
        HPText.text = HP.ToString();
        OnDefence(0);
        Player_Hit.Play();
        if (HP <= 0)
        {
            StartCoroutine(DieSequence());
        }
    }

    public void OnDefence(float Defence)
    {

        DEF += Defence;

        if (DEF == 0)
        {
            DEFText.text = "";
        }
        else if (DEF > 0)
        {
            DEFText.text = $"+{DEF}";
        }
        else
        {
            DEFText.text = $"{DEF}";
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

        HPText.text = HP.ToString();
    }

    private IEnumerator DieSequence()
    {
        turnManager.isBattleEnded = true; // 모든 적 행동 코루틴 종료 신호
        // 카메라 플레이어 줌인
        cam.ZoomToTarget(transform, cam.zoomedSize); 

        // AnounceUI 외 모든 UI 오브젝트 비활성화
        foreach (Transform child in rootCanvas.transform)
        {
            if (child.gameObject != anounceUIObj)
                child.gameObject.SetActive(false);
        }

        anounceUI.TextType = 3;
        // 애니메이션 재생
        yield return StartCoroutine(anounceUI.AnounceAnim());

        // 2초 대기
        yield return new WaitForSeconds(2f);

        // 씬 전환
        SceneManager.LoadScene(1);
    }
}
