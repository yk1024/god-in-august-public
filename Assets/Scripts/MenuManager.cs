using UnityEngine;
using UnityEngine.InputSystem;

public class MenuManager : MonoBehaviour
{
    [SerializeField]
    private InputActionReference menuAction;

    [SerializeField]
    private GameObject menuPanel;

    [SerializeField]
    private GameObject aboutGamePanel;

    [SerializeField]
    private GameObject controlPanel;

    [SerializeField]
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
