using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace GodInAugust.UI
{

/// <summary>
/// ボタンをInputActionに紐付けるためのコンポーネント
/// </summary>
[AddComponentMenu("God In August/UI/Input Action Button"), RequireComponent(typeof(Button))]
public class InputActionButton : MonoBehaviour
{
    [SerializeField, Tooltip("紐付けたいInputAction")]
    private InputActionReference inputAction;

    // 同じオブジェクトに付されたボタン
    private Button button;

    private void Awake()
    {
        button = GetComponent<Button>();
    }

    private void OnEnable()
    {
        inputAction.action.performed += OnActionPerformed;
    }

    private void OnDisable()
    {
        inputAction.action.performed -= OnActionPerformed;
    }

    private void OnActionPerformed(InputAction.CallbackContext context)
    {
        button.onClick.Invoke();
    }
}
}
