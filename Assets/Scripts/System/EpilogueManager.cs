using System.Collections;
using UnityEngine;
using GodInAugust.UI;

namespace GodInAugust.System
{
[AddComponentMenu("God In August/System/Epilogue Manager")]
public class EpilogueManager : MonoBehaviour
{
    [SerializeField, TextArea]
    private string[] epilogueText;

    IEnumerator Start()
    {
        Dialogue dialogue = FindObjectOfType<Dialogue>();
        SceneLoader sceneLoader = GetComponent<SceneLoader>();

        yield return dialogue.ShowText(epilogueText);
        sceneLoader.LoadScene();
    }
}
}
