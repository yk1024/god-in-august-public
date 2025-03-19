using UnityEngine;
using UnityEngine.InputSystem;

namespace GodInAugust.UI
{
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

    private GameObject[] panels;

    void Awake()
    {
        menuAction.action.performed += OpenMenu;
        panels = new GameObject[] { menuPanel, aboutGamePanel, controlPanel, creditPanel };
    }

    void OnDestroy()
    {
        menuAction.action.performed -= OpenMenu;
    }

    private void OpenMenu(InputAction.CallbackContext context)
    {
        OpenMenu();
    }

    public void OpenMenu()
    {
        menuPanel.SetActive(true);
    }

    public void ToOtherPanel(GameObject panel)
    {
        CloseAllPanels();
        panel.SetActive(true);
    }

    private void CloseAllPanels()
    {
        foreach (var p in panels) { p.SetActive(false); }
    }
}
}
