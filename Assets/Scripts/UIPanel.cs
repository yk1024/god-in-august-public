using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class UIPanel : MonoBehaviour
{
    private PlayerInput playerInput;
    private IEnumerable<Selectable> selectables;

    [SerializeField]
    private InputActionReference cancelAction;

    public UnityEvent OnCancelCallback { get; } = new UnityEvent();

    private CursorLockMode previousLockState;

    void Awake()
    {
        playerInput = FindObjectOfType<PlayerInput>();
        selectables = GetComponentsInChildren<Selectable>(true);
    }

    void OnEnable()
    {
        playerInput.SwitchCurrentActionMap(Constants.UIActionMap);
        selectables.First((selectable) => selectable.gameObject.activeSelf).Select();
        previousLockState = Cursor.lockState;
        Cursor.lockState = CursorLockMode.None;
        cancelAction.action.performed += OnCancel;
    }

    void OnDisable()
    {
        if (playerInput != null)
        {
            playerInput.SwitchCurrentActionMap(Constants.PlayerActionMap);
        }

        cancelAction.action.performed -= OnCancel;

        Cursor.lockState = previousLockState;
    }

    public void OnCancel()
    {
        gameObject.SetActive(false);
        OnCancelCallback.Invoke();
    }

    public void OnCancel(InputAction.CallbackContext context)
    {
        OnCancel();
    }
}
