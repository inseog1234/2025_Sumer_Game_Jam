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
        if (isPlaying) yield break;
        isPlaying = true;

        float duration = 0.2f;
        float elapsed = 0f;

        Vector3 startScale = new Vector3(1f, 0f, 1f);
        Vector3 targetScale = new Vector3(1f, 1f, 1f);

        while (elapsed < duration)
        {
            if (TurnManager.isBattleEnded) yield break;
            float t = elapsed / duration;
            Pannel.localScale = Vector3.Lerp(startScale, targetScale, t);
            elapsed += Time.deltaTime;
            yield return null;
        }
        Pannel.localScale = targetScale;

        yield return new WaitForSeconds(1f);

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
