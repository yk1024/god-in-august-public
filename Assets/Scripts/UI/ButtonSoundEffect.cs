using UnityEngine;
using UnityEngine.UI;

namespace GodInAugust.UI
{
/// <summary>
/// ボタンの押下時にクリック音を鳴らすためのコンポーネント
/// </summary>
[AddComponentMenu("God In August/UI/Button Sound Effect"), RequireComponent(typeof(Button))]
public class ButtonSoundEffect : MonoBehaviour
{
    [SerializeField, Tooltip("クリック音のイベント")]
    private AK.Wwise.Event onClickSoundEffect;

    // 同じオブジェクトに付されたボタン
    private Button button;

    private void Awake()
    {
        button = GetComponent<Button>();

        if (GetComponent<AkGameObj>() == null)
        {
            gameObject.AddComponent<AkGameObj>();
        }
    }

    private void OnEnable()
    {
        button.onClick.AddListener(OnClick);
    }

    private void OnDisable()
    {
        button.onClick.RemoveListener(OnClick);
    }

    private void OnClick()
    {
        onClickSoundEffect.Post(gameObject);
    }
}
}
