using System.Collections;
using GodInAugust.System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GodInAugust.UI
{
/// <summary>
/// 会話文などの文章を表示するためのコンポーネント
/// </summary>
[AddComponentMenu("God In August/UI/Dialogue")]
public class Dialogue : SingletonBehaviour<Dialogue>
{
    [SerializeField, Tooltip("ダイアログのテキスト")]
    private TextMeshProUGUI textMeshProUGUI;

    [SerializeField, Tooltip("文字送りのアクション")]
    private InputActionReference toNextAction;

    [SerializeField, Tooltip("文字を表示する際の音を再生するイベント")]
    private AK.Wwise.Event nextCharacterEvent;

    [SerializeField, Tooltip("次の文章を表示する際の音を再生するイベント")]
    private AK.Wwise.Event nextTextEvent;

    // 次の文字が表示されるまでの時間
    private const float TimePerCharacter = 0.1f;

    /// <summary>
    /// 複数の文を表示して、すべての文を読み終えるまで待つメソッド
    /// </summary>
    /// <param name="text">表示したい文章。複数の文を表示したい場合は、複数の文字列を渡す</param>
    /// <returns>すべての文を読み終えるまで待つためのIEnumerator</returns>
    public IEnumerator ShowText(params string[] text)
    {
        PlayerInput playerInput = PlayerInput.GetPlayerByIndex(0);
        string previousActionMap = playerInput.currentActionMap.name;
        bool previouslyActive = gameObject.activeSelf;

        // アクションマップを設定し、アクティブにする。
        playerInput.SwitchCurrentActionMap(toNextAction.action.actionMap.name);
        gameObject.SetActive(true);

        foreach (string sentence in text)
        {
            // まずダイアログの文字を消す。
            textMeshProUGUI.text = "";

            foreach (char character in sentence)
            {
                // ダイアログに次の文字を追記する。
                textMeshProUGUI.text += character;

                // SFX再生
                nextCharacterEvent.Post(gameObject);

                // 次の文字が表示されるまで待つ。
                yield return new WaitForSeconds(TimePerCharacter);
            }

            // 一文が終わったら、▼を表示する。
            textMeshProUGUI.text += "▼";

            // 次の文に進めるための入力をプレイヤーが行うまで待つ。
            yield return new WaitUntil(() => toNextAction.action.IsPressed());

            // SFX再生
            nextTextEvent.Post(gameObject);
        }

        // 元のアクションマップに戻し、アクティブの状態も戻す。
        playerInput.SwitchCurrentActionMap(previousActionMap);
        gameObject.SetActive(previouslyActive);
    }
}
}
