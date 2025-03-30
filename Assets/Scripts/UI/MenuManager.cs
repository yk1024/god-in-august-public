using UnityEngine;

namespace GodInAugust.UI
{
/// <summary>
/// 操作説明などのメニューを表示するパネルのためのコンポーネント
/// </summary>
[AddComponentMenu("God In August/UI/Menu Manager")]
public class MenuManager : MonoBehaviour
{
    [SerializeField, Tooltip("メニューパネル")]
    private GameObject menuPanel;

    [SerializeField, Tooltip("ゲームについてパネル")]
    private GameObject aboutGamePanel;

    [SerializeField, Tooltip("操作説明パネル")]
    private GameObject controlPanel;

    [SerializeField, Tooltip("クレジットパネル")]
    private GameObject creditPanel;

    [SerializeField, Tooltip("ゲームをやめるパネル")]
    private GameObject quitPanel;

    // すべてのパネルを配列にしておいて、まとめて操作しやすくする。
    private GameObject[] panels;

    private void Awake()
    {
        panels = new GameObject[] { menuPanel, aboutGamePanel, controlPanel, creditPanel, quitPanel };
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

    /// <summary>
    /// ゲームを終了するメソッド
    /// </summary>
    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
}
