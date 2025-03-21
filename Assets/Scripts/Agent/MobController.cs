using UnityEngine;
using GodInAugust.System;

namespace GodInAugust.Agent
{
/// <summary>
/// NPCキャラクターを動かすためのコンポーネント
/// </summary>
[AddComponentMenu("God In August/Agent/Mob Controller")]
public class MobController : AgentController
{
    // 同じオブジェクトに付されたLookAtHandler
    private LookAtHandler lookAtHandler;

    // モーションスピードのアニメーションパラメータ名
    private const string MotionSpeedAnimatorParameter = "MotionSpeed";


    protected override void Start()
    {
        base.Start();

        lookAtHandler = GetComponent<LookAtHandler>();

        animator.SetFloat(MotionSpeedAnimatorParameter, 1);
    }

    protected override void Update()
    {
        base.Update();

        lookAtHandler.LookAtFirstTarget();
    }
}
}
