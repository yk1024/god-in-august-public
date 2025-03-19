using UnityEngine;
using UnityEngine.SceneManagement;

namespace GodInAugust.System
{
/// <summary>
/// シーンをロードするためのコンポーネント。
/// </summary>
[AddComponentMenu("God In August/System/Scene Loader")]
public class SceneLoader : MonoBehaviour
{
    [SerializeField, Tooltip("読み込むシーン名")]
    private string sceneName;

    /// <summary>
    /// シーンをロードする。
    /// </summary>
    public void LoadScene()
    {
        SceneManager.LoadScene(sceneName);
    }
}
}
