using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
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
    public TextMeshProUGUI NameTxt;

    public Enemy[] enemies;

    public CameraController cam; // 인스펙터에 할당
    public AnounceUI anounceUI; // 인스펙터에 할당
    public GameObject anounceUIObj; // AnounceUI가 있는 GameObject (예: Panel 등)
    public Canvas rootCanvas; // 전체 UI가 포함된 최상위 Canvas
    public TurnManager turnManager;
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
        else if (DEF == 0)
        {
            HP -= Damage;
        }

        HPText.text = HP.ToString();

        if (HP <= 0)
        {
            StartCoroutine(DieSequence());
        }
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
