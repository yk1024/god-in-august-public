using UnityEngine;
using UnityEngine.InputSystem;

namespace GodInAugust.UI
{
/// <summary>
/// 操作説明などのメニューを表示するパネルのためのコンポーネント
/// </summary>
[AddComponentMenu("God In August/UI/Menu Manager")]
public class MenuManager : MonoBehaviour
{
    [SerializeField, Tooltip("メニューを開くアクション")]
    private InputActionReference menuAction;

    [SerializeField, Tooltip("メニューパネル")]
    private GameObject menuPanel;

    [SerializeField, Tooltip("ゲームについてパネル")]
    private GameObject aboutGamePanel;

    [SerializeField, Tooltip("操作説明パネル")]
    private GameObject controlPanel;

    [SerializeField, Tooltip("クレジットパネル")]
    private GameObject creditPanel;

    // すべてのパネルを配列にしておいて、まとめて操作しやすくする。
    private GameObject[] panels;

    private void Awake()
    {
        menuAction.action.performed += OpenMenu;
        panels = new GameObject[] { menuPanel, aboutGamePanel, controlPanel, creditPanel };
    }

    private void OnDestroy()
    {
        menuAction.action.performed -= OpenMenu;
    }

    // メニューを開く入力をプレイヤーが行なった時に、メニューを開くメソッド
    private void OpenMenu(InputAction.CallbackContext context)
    {
        OpenMenu();
    }

    /// <summary>
    /// メニューを開く
    /// </summary>
    public void OpenMenu()
    {
        menuPanel.SetActive(true);
    }

    /// <summary>
    /// 現在のパネルを閉じて、他のパネルを開く
    /// </summary>
    /// <param name="panel">開きたいパネル</param>
    public void ToOtherPanel(GameObject panel)
    {
        // 先にすべてのパネルを閉じてから、対象のパネルを開く。
        CloseAllPanels();
        panel.SetActive(true);
    }

    private void CloseAllPanels()
    {
        // すべてのパネルを閉じる
        foreach (var p in panels) { p.SetActive(false); }
    }
}
}
