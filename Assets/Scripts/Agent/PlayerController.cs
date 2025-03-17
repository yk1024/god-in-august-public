using System.Collections;
using StarterAssets;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using GodInAugust.System;
using GodInAugust.Level;

namespace GodInAugust.Agent
{
public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float distance;

    private bool interacting;
    private LookAtHandler lookAt;

    private StarterAssetsInputs starterAssetsInputs;

    private MusicManager musicManager;

    private Animator animator;

    private readonly UnityEvent onAnimationEnd = new UnityEvent();

    void Start()
    {
        lookAt = GetComponent<LookAtHandler>();
        starterAssetsInputs = GetComponent<StarterAssetsInputs>();
        musicManager = FindObjectOfType<MusicManager>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        (ILookAtTarget target, float distance) = lookAt.LookAtFirstTarget();

        InteractTarget(target, distance);

        interacting = false;

        MoveSpeed moveSpeed = GetMoveSpeed();

        musicManager.SetMoveSpeed(moveSpeed);
    }

    public void OnInteract(InputValue inputValue)
    {
        interacting = true;
    }

    private void InteractTarget(ILookAtTarget target, float distance)
    {
        if (interacting && target is IInteractable interactable && distance <= this.distance)
        {
            interactable.Interact();
        }
    }

    private MoveSpeed GetMoveSpeed()
    {
        if (starterAssetsInputs.move == Vector2.zero)
        {
            return MoveSpeed.Stop;
        }
        else if (starterAssetsInputs.sprint)
        {
            return MoveSpeed.Sprint;
        }
        else
        {
            return MoveSpeed.Walk;
        }
    }

    public IEnumerator Pray()
    {
        animator.SetTrigger(Constants.PrayAnimatorTrigger);
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
}
