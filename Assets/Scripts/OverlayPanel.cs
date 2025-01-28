using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class OverlayPanel : MonoBehaviour
{
    private Animator animator;
    private const string FadeInTrigger = "FadeIn";
    private const string FadeOutTrigger = "FadeOut";
    private const string TransitionSpeedParameter = "TransitionSpeed";
    private readonly UnityEvent onAnimationEnd = new UnityEvent();
    private PlayerInput playerInput;

    void Awake()
    {
        animator = GetComponent<Animator>();
        playerInput = FindObjectOfType<PlayerInput>();
    }

    public IEnumerator FadeIn(float transitionSeconds)
    {
        yield return Animate(FadeInTrigger, transitionSeconds);
    }

    public IEnumerator FadeOut(float transitionSeconds)
    {
        yield return Animate(FadeOutTrigger, transitionSeconds);
    }

    private IEnumerator Animate(string trigger, float transitionSeconds)
    {
        playerInput.DeactivateInput();
        animator.SetFloat(TransitionSpeedParameter, 1 / transitionSeconds);
        animator.SetTrigger(trigger);

        yield return WaitForAnimationEnd();
        playerInput.ActivateInput();
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
