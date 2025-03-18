using UnityEngine;
using GodInAugust.System;

namespace GodInAugust.Agent
{
[AddComponentMenu("God In August/Agent/Mob Controller")]
public class MobController : AgentController
{
    private LookAtHandler lookAtHandler;

    protected override void Start()
    {
        base.Start();

        lookAtHandler = GetComponent<LookAtHandler>();

        animator.SetFloat(Constants.MotionSpeedAnimatorParameter, 1);
    }

    protected override void Update()
    {
        base.Update();

        lookAtHandler.LookAtFirstTarget();
    }

    public void OnFootstep(AnimationEvent animationEvent)
    {

    }

    public void OnLand(AnimationEvent animationEvent)
    {

    }
}
}
