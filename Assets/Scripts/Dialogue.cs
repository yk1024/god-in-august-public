using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class Dialogue : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI textMeshProUGUI;

    [SerializeField]
    private UnityEvent onDialogueDone;

    [SerializeField]
    private InputActionReference toNextAction;

    private const float TimePerCharacter = 0.1f;

    [SerializeField, TextArea]
    private string[] initialText;

    void Start()
    {
        if (initialText.Length != 0)
        {
            StartCoroutine(ShowText(initialText));
        }
    }

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

        onDialogueDone.Invoke();
    }
}
