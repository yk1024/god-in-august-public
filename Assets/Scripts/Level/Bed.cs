using UnityEngine;
using GodInAugust.System;
using GodInAugust.UI;
using System.Collections;
using UnityEngine.InputSystem;

namespace GodInAugust.Level
{
/// <summary>
/// 布団のコンポーネント
/// </summary>
[AddComponentMenu("God In August/Level/Bed")]
public class Bed : SingletonBehaviour<Bed>, IInteractable
{
    [SerializeField, Tooltip("確認パネル")]
    private GameObject confirmationPanel;

    [field: SerializeField, Tooltip("インタラクトの対象位置")]
    public Transform TargetPoint { get; private set; }

    [SerializeField, Tooltip("初日の祈り前に寝ようとすると表示されるメッセージ"), TextArea]
    private string[] tutorialText;

    [SerializeField, Tooltip("寝る際のジングルのイベント")]
    private AK.Wwise.Event sleepJingle;

    // Wwise側で設定するフェードを始めるタイミングのマーカー
    private const string StardFadeMarker = "StartFade";

    public void Interact()
    {
        if (GameState.State.DateIndex == 0 && GameManager.Instance.PrayType == PrayType.None)
        {
            // 初日は祈るまで、寝れない。代わりのメッセージを表示する。
            StartCoroutine(Dialogue.Instance.ShowText(tutorialText));
        }
        else
        {
            confirmationPanel.SetActive(true);
        }
    }

    /// <summary>
    /// 寝るコマンド。一日を終える。
    /// </summary>
    public void Sleep()
    {
        StartCoroutine(EndDay());
    }

    private IEnumerator EndDay()
    {
        // パネルを非表示にする
        confirmationPanel.SetActive(false);

        // 入力を無効にする
        PlayerInput playerInput = PlayerInput.GetPlayerByIndex(0);
        playerInput.DeactivateInput();

        // UIのクリック音とジングルが被らないように少し待つ。
        yield return new WaitForSeconds(1);

        // 寝るジングルを呼び、フェードするタイミングまで待つ。
        bool startFade = false;
        sleepJingle.Post(gameObject, (uint)AkCallbackType.AK_Marker, WaitForStartFade);
        yield return new WaitUntil(() => startFade);

        // 日を終える処理を呼ぶ。
        yield return GameManager.Instance.EndDay();

        void WaitForStartFade(object cookie, AkCallbackType type, AkCallbackInfo callbackInfo)
        {
            // フェードし始めるタイミングはWwise側のマーカーで設定する。
            AkMarkerCallbackInfo markerCallbackInfo = (AkMarkerCallbackInfo)callbackInfo;

            if (markerCallbackInfo.strLabel == StardFadeMarker)
            {
                startFade = true;
            }
        }
    }
}
}
