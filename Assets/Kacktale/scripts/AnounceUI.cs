using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AnounceUI : MonoBehaviour
{
    public int TextType;
    public RectTransform Pannel;
    public TextMeshProUGUI Text;
    public string[] AnounceName;

    private bool isPlaying = false;
    public TurnManager TurnManager;

    public void Start()
    {
        StartCoroutine(AnounceAnim());
    }

    public IEnumerator AnounceAnim()
    {
        Text.text = AnounceName[TextType];
        if (isPlaying) yield break; // 중복 방지
        isPlaying = true;

        float duration = 0.2f;
        float elapsed = 0f;

        Vector3 startScale = new Vector3(1f, 0f, 1f);
        Vector3 targetScale = new Vector3(1f, 1f, 1f);

        // 열기 애니메이션
        while (elapsed < duration)
        {
            if (TurnManager.isBattleEnded) yield break; // 전투 종료 시 중단
            float t = elapsed / duration;
            Pannel.localScale = Vector3.Lerp(startScale, targetScale, t);
            elapsed += Time.deltaTime;
            yield return null;
        }
        Pannel.localScale = targetScale;

        // 유지 시간
        yield return new WaitForSeconds(1f);

        // 닫기 애니메이션
        elapsed = 0f;
        while (elapsed < duration)
        {
            if (TurnManager.isBattleEnded) yield break;
            float t = elapsed / duration;
            Pannel.localScale = Vector3.Lerp(targetScale, startScale, t);
            elapsed += Time.deltaTime;
            yield return null;
        }
        Pannel.localScale = startScale;

        isPlaying = false;
    }
}
