using System.Collections;
using UnityEngine;
using GodInAugust.UI;

namespace GodInAugust.System
{
/// <summary>
/// プロローグシーンを制御するためのコンポーネント
/// </summary>
[AddComponentMenu("God In August/System/Prologue Manager")]
public class PrologueManager : MonoBehaviour
{
    [SerializeField, TextArea, Tooltip("プロローグの文章")]
    private string[] prologueText;

    private IEnumerator Start()
    {
        Dialogue dialogue = FindObjectOfType<Dialogue>();
        SceneLoader sceneLoader = GetComponent<SceneLoader>();

        // ダイアログでプロローグの文章を流す。
        yield return dialogue.ShowText(prologueText);

        // 次のシーンに遷移する。
        sceneLoader.LoadScene();
    }
}
}
