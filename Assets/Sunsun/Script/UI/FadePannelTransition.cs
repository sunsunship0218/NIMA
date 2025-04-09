using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class FadePannelTransition : MonoBehaviour
{
    [SerializeField] CanvasGroup canvasGroup;
    Tween fadeTween;

    private void Awake()
    {
        StartCoroutine(TransIN(1f));
    }
    // 淡入效果：使 canvasGroup.alpha 漸變到 1 (全不透明)
    void FadeIn(float duration)
    {
        Fade(0f, duration, () =>
        {
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
        });
    }
    // 淡出效果：使 canvasGroup.alpha 漸變到 0 (全透明)
    void FadeOut(float duration)
    {
        Fade(1f, duration, () =>
        {
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
        });
    }
    void Fade(float endValue, float duration, TweenCallback onEnd)
    {
        if (fadeTween != null)
        {
            fadeTween.Kill(false);
        }
        fadeTween = canvasGroup.DOFade(endValue, duration);
        fadeTween.onComplete += onEnd;
    }
    public IEnumerator TransitionScene(int sceneIndex, float fadeOutDuration)
    {
        // 先淡出 (變成全不透明)
        FadeOut(fadeOutDuration);
        yield return fadeTween.WaitForCompletion();

        // 切換場景 (使用非同步方式)
        SceneManager.LoadScene(sceneIndex);
    }
    // 場景切換完成後，淡入 (恢復透明)
    IEnumerator TransIN(float fadeInDuration)
    {
        FadeIn(fadeInDuration);
        yield return fadeTween.WaitForCompletion();
    }
}
