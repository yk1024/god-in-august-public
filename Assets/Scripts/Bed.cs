using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Bed : MonoBehaviour, IInteractable
{
    [SerializeField]
    private Animator overlayPanelAnimator;

    [SerializeField]
    private string nextSceneName;

    [SerializeField]
    private GameObject confirmationPanel;

    private PlayerInput playerInput;

    // Start is called before the first frame update
    void Start()
    {
        playerInput = FindObjectOfType<PlayerInput>();
        WakeUp();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Interact()
    {
        confirmationPanel.SetActive(true);
    }

    private void WakeUp()
    {
        overlayPanelAnimator.SetTrigger(Constants.FadeInTrigger);
        Invoke("EnableInput", 5);
    }

    private void EnableInput()
    {
        playerInput.SwitchCurrentActionMap(Constants.PlayerActionMap);
    }

    public void Sleep()
    {
        playerInput.DeactivateInput();
        confirmationPanel.SetActive(false);
        overlayPanelAnimator.SetTrigger(Constants.FadeOutTrigger);
        Invoke("LoadNextScene", 5);
    }

    private void LoadNextScene()
    {
        SceneManager.LoadScene(nextSceneName);
    }
}
