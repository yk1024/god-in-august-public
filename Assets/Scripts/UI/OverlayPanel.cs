using System.Collections;
using GodInAugust.System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace GodInAugust.UI
{
/// <summary>
/// 画面のフェードイン・フェードアウトに使うための、画面を全体を覆うパネル用のコンポーネント
/// </summary>
[AddComponentMenu("God In August/UI/Overlay Panel")]
public class OverlayPanel : SingletonBehaviour<OverlayPanel>
{
    // 同じゲームオブジェクトに付されたアニメーター
    // フェードイン・フェードアウトをアニメーションで行う。
    private Animator animator;

    // アニメーションで使われるフェードイン用のステート名
    private const string FadeInState = "FadeIn";

    // アニメーションで使われるフェードアウト用のステート名
    private const string FadeOutState = "FadeOut";

    // アニメーションで使われる、フェードの速度に関するパラメータ
    private const string TransitionSpeedParameter = "TransitionSpeed";

    // アニメーションが終わったことを検知するために使うUnityEvent
    private readonly UnityEvent onAnimationEnd = new UnityEvent();

    protected override void Awake()
    {
        base.Awake();

        animator = GetComponent<Animator>();
    }

    /// <summary>
    /// フェードインする。
    /// </summary>
    /// <param name="transitionSeconds">フェードインにかける時間（秒）</param>
    /// <returns>フェードインするまで待つIEnumerator</returns>
    public IEnumerator FadeIn(float transitionSeconds)
    {
        yield return Animate(FadeInState, transitionSeconds);
    }

    /// <summary>
    /// フェードアウトする。
    /// </summary>
    /// <param name="transitionSeconds">フェードアウトにかける時間（秒）</param>
    /// <returns>フェードアウトするまで待つIEnumerator</returns>
    public IEnumerator FadeOut(float transitionSeconds)
    {
        yield return Animate(FadeOutState, transitionSeconds);
    }

    // フェードイン・アウトする処理
    private IEnumerator Animate(string stateName, float transitionSeconds)
    {
        PlayerInput playerInput = PlayerInput.GetPlayerByIndex(0);
        // フェード中は入力を受け付けない
        playerInput.DeactivateInput();

        // アニメーターに速度とトリガーを渡す。
        animator.SetFloat(TransitionSpeedParameter, 1 / transitionSeconds);
        animator.Play(stateName);

        // アニメーションが終わるまで待つ。
        yield return Utilities.WaitForEvent(onAnimationEnd);

        // フェードが終了したら入力を受け付ける。
        playerInput.ActivateInput();
    }

    /// <summary>
    /// アニメーションが終わった時にアニメーションイベントで呼ぶためのメソッド
    /// </summary>
    public void EndAnimation()
    {
        onAnimationEnd.Invoke();
    }
}
}
