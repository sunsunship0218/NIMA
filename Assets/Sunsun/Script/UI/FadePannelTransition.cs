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
    // �H�J�ĪG�G�� canvasGroup.alpha ���ܨ� 1 (�����z��)
    void FadeIn(float duration)
    {
        Fade(0f, duration, () =>
        {
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
        });
    }
    // �H�X�ĪG�G�� canvasGroup.alpha ���ܨ� 0 (���z��)
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
        // ���H�X (�ܦ������z��)
        FadeOut(fadeOutDuration);
        yield return fadeTween.WaitForCompletion();

        // �������� (�ϥΫD�P�B�覡)
        SceneManager.LoadScene(sceneIndex);
    }
    // ��������������A�H�J (��_�z��)
    IEnumerator TransIN(float fadeInDuration)
    {
        FadeIn(fadeInDuration);
        yield return fadeTween.WaitForCompletion();
    }
}
