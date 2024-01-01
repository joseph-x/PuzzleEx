using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class TransitionManager : Singleton<TransitionManager>
{

    public SceneItems startScene = SceneItems.H1;
    public CanvasGroup fadeCanvasGroup;
    public float fadeDuration;

    private bool isFade;

    private void Start()
    {
        StartCoroutine(TransitionToScene(string.Empty, Scenes.Instance.GetSceneName(startScene)));
    }


    public void Transition(SceneItems from, SceneItems to)
    {
        string fromName = Scenes.Instance.GetSceneName(from);
        string toName = Scenes.Instance.GetSceneName(to);

        if (fromName != null && toName != null)
        {

            if (!isFade)
            {
                StartCoroutine(TransitionToScene(fromName, toName));
            }
        }
        else
        {
            Debug.LogError("New scene is not existed.");
        }
    }

    private IEnumerator TransitionToScene(string from, string to)
    {
        yield return Fade(1);

        if (from != string.Empty)
        {
            EventHandler.CallBeforeSceneUnloadEvent();
            yield return SceneManager.UnloadSceneAsync(from);
        }
        
        yield return SceneManager.LoadSceneAsync(to, LoadSceneMode.Additive);

        Scene newScene = SceneManager.GetSceneByName(to);
        SceneManager.SetActiveScene(newScene);

        EventHandler.CallAfterSceneLoadedEvent();
        yield return Fade(0);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="targetAlpha">1 is to black, 0 is to transparent</param>
    /// <returns></returns>
    private IEnumerator Fade(float targetAlpha)
    {
        isFade = true;
        fadeCanvasGroup.blocksRaycasts = true;

        float speed = Mathf.Abs(fadeCanvasGroup.alpha - targetAlpha) / fadeDuration;

        while (!Mathf.Approximately(fadeCanvasGroup.alpha, targetAlpha))
        {
            fadeCanvasGroup.alpha = Mathf.MoveTowards(fadeCanvasGroup.alpha, targetAlpha, speed * Time.deltaTime);
            yield return null;
        }

        fadeCanvasGroup.blocksRaycasts = false;
        isFade = false;
    }
}

