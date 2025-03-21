using UnityEngine;
using GodInAugust.System;
using GodInAugust.UI;

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
        confirmationPanel.SetActive(false);
        StartCoroutine(GameManager.Instance.EndDay());
    }
}
}
