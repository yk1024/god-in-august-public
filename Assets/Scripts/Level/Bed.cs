using UnityEngine;
using GodInAugust.System;

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

    // 現在利用可能かどうか。初日は祈るまで利用できない。
    public bool Available { get; set; } = true;

    private void Start()
    {
        if (GameState.State.DateIndex == 0) Available = false;
    }

    public void Interact()
    {
        if (Available)
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
