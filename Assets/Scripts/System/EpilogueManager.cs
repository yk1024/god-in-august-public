using System.Collections;
using UnityEngine;
using GodInAugust.UI;

namespace GodInAugust.System
{
/// <summary>
/// エピローグシーンを制御するためのコンポーネント
/// </summary>
[AddComponentMenu("God In August/System/Epilogue Manager")]
public class EpilogueManager : MonoBehaviour
{
    [SerializeField, TextArea, Tooltip("エピローグの文章")]
    private string[] epilogueText;

    private IEnumerator Start()
    {
        Dialogue dialogue = FindObjectOfType<Dialogue>();
        SceneLoader sceneLoader = GetComponent<SceneLoader>();

        // ダイアログでエピローグの文章を流す。
        yield return dialogue.ShowText(epilogueText);

        // 次のシーンに遷移する。
        sceneLoader.LoadScene();
    }
}
}
