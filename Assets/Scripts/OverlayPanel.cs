using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class OverlayPanel : MonoBehaviour
{
    private Animator animator;
    private const string FadeInTrigger = "FadeIn";
    private const string FadeOutTrigger = "FadeOut";
    private readonly UnityEvent onAnimationEnd = new UnityEvent();

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public IEnumerator FadeIn()
    {
        animator.SetTrigger(FadeInTrigger);

        yield return WaitForAnimationEnd();
    }

    public IEnumerator FadeOut()
    {
        animator.SetTrigger(FadeOutTrigger);

        yield return WaitForAnimationEnd();
    }

    private IEnumerator WaitForAnimationEnd()
    {
        bool triggered = false;
        void callback() => triggered = true;
        onAnimationEnd.AddListener(callback);
        yield return new WaitUntil(() => triggered);
        onAnimationEnd.RemoveListener(callback);
    }

    public void EndAnimation()
    {
        onAnimationEnd.Invoke();
    }
}
