using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GodInAugust.UI
{
[AddComponentMenu("God In August/UI/Dialogue")]
public class Dialogue : MonoBehaviour
{
    [SerializeField, Tooltip("ダイアログのテキスト")]
    private TextMeshProUGUI textMeshProUGUI;

    [SerializeField, Tooltip("文字送りのアクション")]
    private InputActionReference toNextAction;

    private const float TimePerCharacter = 0.1f;

    public IEnumerator ShowText(params string[] text)
    {
        foreach (string sentence in text)
        {
            textMeshProUGUI.text = "";

            foreach (char character in sentence)
            {
                textMeshProUGUI.text += character;
                yield return new WaitForSeconds(TimePerCharacter);
            }

            textMeshProUGUI.text += "▼";

            yield return new WaitUntil(() => toNextAction.action.IsPressed());
        }
    }
}
}
