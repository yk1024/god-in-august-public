using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class UIPanel : MonoBehaviour
{
    private PlayerInput playerInput;
    private Selectable firstSelectable;

    [SerializeField]
    private InputActionReference cancelAction;

    void Awake()
    {
        playerInput = FindObjectOfType<PlayerInput>();
        firstSelectable = GetComponentInChildren<Selectable>(true);
    }

    void OnEnable()
    {
        playerInput.SwitchCurrentActionMap(Constants.UIActionMap);
        firstSelectable.Select();
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

        Cursor.lockState = CursorLockMode.Locked;
    }

    public void OnCancel()
    {
        gameObject.SetActive(false);
    }

    public void OnCancel(InputAction.CallbackContext context)
    {
        OnCancel();
    }
}
