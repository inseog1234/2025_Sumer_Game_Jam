using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Loading_Scene : MonoBehaviour
{
    public static string nextScene;
    [SerializeField] Image progressBar;

    private void Start()
    {
        nextScene = @"MobTest";
        StartCoroutine(LoadScene());
    }

    public static void LoadScene(string sceneName)
    {
        nextScene = sceneName;
        SceneManager.LoadScene("Loading");
    }

    public IEnumerator LoadScene()
    {
        yield return null;

        AsyncOperation op = SceneManager.LoadSceneAsync(nextScene);
        op.allowSceneActivation = false;

        float fakeProgress = 0f;

        while (fakeProgress < 0.9f)
        {
            fakeProgress += Time.deltaTime * 0.5f;
            progressBar.fillAmount = fakeProgress;
            yield return null;
        }

        progressBar.fillAmount = 0.9f;

        while (op.progress < 0.9f)
        {
            yield return null;
        }

        float timer = 0f;
        while (progressBar.fillAmount < 1.0f)
        {
            timer += Time.deltaTime;
            progressBar.fillAmount = Mathf.Lerp(progressBar.fillAmount, 1.0f, timer);
            if (progressBar.fillAmount >= 0.995f)
            {
                progressBar.fillAmount = 1.0f;
                break;
            }
            yield return null;
        }

        op.allowSceneActivation = true;
    }
}
