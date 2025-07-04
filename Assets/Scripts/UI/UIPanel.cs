using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace GodInAugust.UI
{
/// <summary>
/// ゲーム中に選択肢を表示したりするためのUIを実装するコンポーネント
/// </summary>
[AddComponentMenu("God In August/UI/UI Panel")]
public class UIPanel : MonoBehaviour
{
    // ヒエラルキー上で子孫のSelectableのコレクション
    private IEnumerable<Selectable> selectables;

    /// <summary>
    /// キャンセルボタンが押されて、パネルを閉じる時に実行されるUnityEvent
    /// </summary>
    public UnityEvent OnCancelCallback { get; } = new UnityEvent();

    // パネルを開く前のCursorLockModeを保持しておくためのフィールド
    private CursorLockMode previousLockState;

    // パネルを開く前のアクションマップを保持しておくためのフィールド
    private string previousActionMap;

    // UI用のInputActionMapの名前
    private const string UIActionMap = "UI";

    private void Awake()
    {
        selectables = GetComponentsInChildren<Selectable>(true);
    }

    private void OnEnable()
    {
        // 現在のアクションマップを保管した上で変更する。
        PlayerInput playerInput = PlayerInput.GetPlayerByIndex(0);
        previousActionMap = playerInput.currentActionMap.name;
        playerInput.SwitchCurrentActionMap(UIActionMap);

        // 選択可能な子孫のうち最初のものを選択しておく。
        selectables.First((selectable) => selectable.gameObject.activeSelf).Select();

        // 現在のCursorLockModeを保管した上で、カーソルは表示するようにする。
        previousLockState = Cursor.lockState;
        Cursor.lockState = CursorLockMode.None;
    }

    private void OnDisable()
    {
        PlayerInput playerInput = PlayerInput.GetPlayerByIndex(0);

        if (playerInput != null)
        {
            // アクションマップを元に戻す。
            playerInput.SwitchCurrentActionMap(previousActionMap);
        }

        // CursorLockModeを元に戻す。
        Cursor.lockState = previousLockState;
    }

    /// <summary>
    /// パネルを閉じてキャンセルする。
    /// </summary>
    public void OnCancel()
    {
        gameObject.SetActive(false);
        OnCancelCallback.Invoke();
    }
}
}
