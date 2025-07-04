using System.Collections;
using StarterAssets;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using GodInAugust.System;
using GodInAugust.Level;

namespace GodInAugust.Agent
{
/// <summary>
/// 操作可能キャラクターをコントロールするコンポーネント
/// </summary>
[AddComponentMenu("God In August/Agent/Player Controller")]
public class PlayerController : SingletonBehaviour<PlayerController>
{
    [SerializeField, Tooltip("インタラクトできる距離")]
    private float distance;

    [SerializeField, Tooltip("インタラクト時に再生する音")]
    private AK.Wwise.Event onInteractEvent;

    // インタラクトが発生しているかどうか。
    private bool interacting;

    // 同じオブジェクトに付されたLookAtHandler
    private LookAtHandler lookAt;

    // 同じオブジェクトに付されたStarterAssetsInputs
    private StarterAssetsInputs starterAssetsInputs;

    // 同じオブジェクトに付されたアニメーター
    private Animator animator;

    // アニメーションが終わるまで待つために使用するUnityEvent
    private readonly UnityEvent onAnimationEnd = new UnityEvent();

    // 祈りアニメーションをトリガーするためのアニメーションパラメータ
    private const string PrayAnimatorParameter = "Praying";

    private void Start()
    {
        lookAt = GetComponent<LookAtHandler>();
        starterAssetsInputs = GetComponent<StarterAssetsInputs>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        (ILookAtTarget target, float distance) = lookAt.LookAtFirstTarget();

        // 見ている対象にインタラクトできる。
        InteractTarget(target, distance);

        interacting = false;

        // 移動速度の状態を取得して、インタラクティブミュージックに反映させる。
        MoveSpeed moveSpeed = GetMoveSpeed();
        MusicManager.Instance.SetMoveSpeed(moveSpeed);
    }

    /// <summary>
    /// インタラクトの操作をプレイヤーが行なった時に、PlayerInputによって呼ばれるメソッド
    /// </summary>
    /// <param name="inputValue">プレイヤーが行なった操作</param>
    public void OnInteract(InputValue inputValue)
    {
        interacting = true;
    }

    // 対象にインタラクトするメソッド
    private void InteractTarget(ILookAtTarget target, float distance)
    {
        // インタラクトが発生中で、見ている対象がインタラクト可能で、対象への距離がインタラクト可能な距離以下である場合、インタラクトする。
        if (interacting && target is IInteractable interactable && distance <= this.distance)
        {
            onInteractEvent.Post(gameObject);
            interactable.Interact();
        }
    }

    // 移動速度の状態を取得するメソッド
    private MoveSpeed GetMoveSpeed()
    {
        if (starterAssetsInputs.move == Vector2.zero)
        {
            // 移動方向を示すベクトルがゼロの場合は停止
            return MoveSpeed.Stop;
        }
        else if (starterAssetsInputs.sprint)
        {
            // 走行フラグが立っている場合は走行
            return MoveSpeed.Sprint;
        }
        else
        {
            // それ以外の場合は歩行
            return MoveSpeed.Walk;
        }
    }

    /// <summary>
    /// 神社に祈りを捧げるモーションを開始する。
    /// </summary>
    /// <returns>祈りアニメーションが終了するまで待つためのIEnumerator</returns>
    public IEnumerator StartPray()
    {
        animator.SetBool(PrayAnimatorParameter, true);
        yield return Utilities.WaitForEvent(onAnimationEnd);
    }

    /// <summary>
    /// 神社に祈りを捧げるモーションを終了する。
    /// </summary>
    /// <returns>祈りアニメーションが終了するまで待つためのIEnumerator</returns>
    public IEnumerator EndPray()
    {
        animator.SetBool(PrayAnimatorParameter, false);
        yield return Utilities.WaitForEvent(onAnimationEnd);
    }

    /// <summary>
    /// アニメーションが終わった時に、アニメーションイベントで呼ぶためのメソッド
    /// </summary>
    public void EndAnimation()
    {
        onAnimationEnd.Invoke();
    }
}
}
