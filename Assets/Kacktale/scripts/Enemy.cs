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

    public CameraController cam; // 인스펙터에 할당
    private Vector3 originalCamPos;
    public AudioSource AttackSound;

    public Animator Animator;

    void Start()
    {
        // OnDamage(1);
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
        Animator.Play("Zombie_Attack");

        if (DEF > 0)
            DEF -= Damage;
        else if (DEF < 0)
        {
            HP += DEF;
            DEF = 0;
        }
        else
        {
            HP -= Damage;
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
                StartCoroutine(AttackAnim());
                break;
            case EnemyNextAct.SpecialAttack:
                StartCoroutine(SpecialAttackAnim());
                break;
            case EnemyNextAct.Support:
                StartCoroutine (SupportAnim());
                break;
        }
        NextAct = (EnemyNextAct)Random.Range(0, 3);
    }

    IEnumerator AttackAnim()
    {
        yield return new WaitForSeconds(0.3f);

        // 카메라 줌인 + 적에게 포커스
        originalCamPos = cam.transform.position;
        cam.ZoomToTarget(transform, cam.zoomedSize);

        Vector3 origin = transform.position;
        Vector3 target = player.transform.position - Vector3.left * 4f;

        float duration = 0.3f;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            if (TurnManager.isBattleEnded) yield break;
            transform.position = Vector3.Lerp(origin, target, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        transform.position = target;

        yield return new WaitForSeconds(0.5f);

        // 공격 처리
        bool randomAttack = Random.value > 0.5f;
        float damage = randomAttack ? ATK * Random.Range(1, 4) : ATK;
        player.OnDamage(damage);
        AttackSound.Play();
        Animator.Play("Zombie_Attack");

        // 디버프 처리 (생략 가능)

        // 복귀
        elapsed = 0f;
        while (elapsed < duration)
        {
            if (TurnManager.isBattleEnded) yield break;
            transform.position = Vector3.Lerp(target, origin, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        transform.position = origin;

        yield return new WaitForSeconds(0.3f);

        // 카메라 줌아웃 + 원위치 복귀
        cam.ResetZoom(Vector3.zero);

        yield return new WaitForSeconds(0.3f);

        TurnManager.EnemyAct = false;
        if (TurnManager.EnemyTurnLeft == 0) TurnManager.PlayerTrun();
        else TurnManager.NextEnemyTurn();
    }

    IEnumerator SpecialAttackAnim()
    {
        yield return new WaitForSeconds(0.3f); // 시작 전 딜레이

        originalCamPos = cam.transform.position;
        cam.ZoomToTarget(transform, cam.zoomedSize);
        Vector3 origin = transform.position;
        Vector3 target = player.transform.position - (Vector3.left * 4f);

        float duration = 0.3f;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            if (TurnManager.isBattleEnded) yield break;
            transform.position = Vector3.Lerp(origin, target, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        transform.position = target;

        yield return new WaitForSeconds(0.5f);

        // 데미지 계산
        bool randomAttack = Random.value > 0.5f;
        float damage = randomAttack ? ATK * Random.Range(1, 4) * 2 : ATK * 2;
        player.OnDamage(damage);
        Debug.Log("특수 공격 데미지 적용");
        AttackSound.Play();
        Animator.Play("Zombie_Attack");

        // 디버프 처리
        if (Random.value > 0.5f)
        {
            List<int> enabledDebufs = new List<int>();
            for (int i = 0; i < DebufEnable.Count; i++)
            {
                if (DebufEnable[i])
                    enabledDebufs.Add(i);
            }

            if (enabledDebufs.Count >= 1)
            {
                int selectedIndex = enabledDebufs[0];
                if (enabledDebufs.Count == 2)
                    selectedIndex = (Random.value < 0.1f) ? enabledDebufs[1] : enabledDebufs[0];
                else if (enabledDebufs.Count > 2)
                {
                    int second = enabledDebufs[1];
                    var rest = new List<int>(enabledDebufs);
                    rest.RemoveAt(1);
                    selectedIndex = (Random.value < 0.1f) ? second : rest[Random.Range(0, rest.Count)];
                }

                if (selectedIndex < haveDebuf.Length && selectedIndex < player.haveDebuf.Length)
                {
                    player.haveDebuf[selectedIndex].Accure += haveDebuf[selectedIndex].Accure;
                    Debug.Log($"디버프 {selectedIndex} 적용됨");
                }
            }
                    yield return new WaitForSeconds(1f);
        }

        // 힐
        int heal = Random.Range(1, 3);
        HP += heal;

        // 복귀
        elapsed = 0f;
        while (elapsed < duration)
        {
            if (TurnManager.isBattleEnded) yield break;
            transform.position = Vector3.Lerp(target, origin, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        transform.position = origin;

        yield return new WaitForSeconds(0.3f); // 복귀 후 딜레이
        cam.ResetZoom(Vector3.zero);
        yield return new WaitForSeconds(1f);
        TurnManager.EnemyAct = false;
        if (TurnManager.EnemyTurnLeft == 0) TurnManager.PlayerTrun();
        else TurnManager.NextEnemyTurn();
    }

    IEnumerator SupportAnim()
    {
        yield return new WaitForSeconds(0.3f); // 시작 전 딜레이

        // Step 1: 시전자에게 줌인
        originalCamPos = cam.transform.position;
        cam.ZoomToTarget(transform, cam.zoomedSize);
        yield return new WaitForSeconds(0.6f); // 살짝 딜레이

        // Step 2: 버프 대상 선택
        int chooseEnemy = Random.Range(0, LeftEnemy.Length);
        Enemy targetEnemy = LeftEnemy[chooseEnemy];

        // Step 3: 대상에게 카메라 이동
        cam.ZoomToTarget(targetEnemy.transform, cam.zoomedSize);
        yield return new WaitForSeconds(0.6f);

        // Step 4: 버프 연출 및 적용
        Debug.Log($"버프 → {targetEnemy.Name}");
        int BuffType = Random.Range(0, 3);
        switch (BuffType)
        {
            case 0:
                targetEnemy.HP += 3;
                break;
            case 1:
                targetEnemy.DEF += 3;
                break;
            case 2:
                targetEnemy.ATK += 3;
                break;
        }

        // Step 5: 카메라 줌아웃 및 마무리
        yield return new WaitForSeconds(0.3f);
        cam.ResetZoom(Vector3.zero);
        yield return new WaitForSeconds(1f);

        TurnManager.EnemyAct = false;
        if (TurnManager.EnemyTurnLeft == 0) TurnManager.PlayerTrun();
        else TurnManager.NextEnemyTurn();
    }

}
