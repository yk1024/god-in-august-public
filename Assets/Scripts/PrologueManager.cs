using System.Collections;
using UnityEngine;

public class PrologueManager : MonoBehaviour
{
    [SerializeField, TextArea]
    private string[] prologueText;

    IEnumerator Start()
    {
        Dialogue dialogue = FindObjectOfType<Dialogue>();
        SceneLoader sceneLoader = GetComponent<SceneLoader>();

        yield return dialogue.ShowText(prologueText);
        sceneLoader.LoadScene();
    }
}
