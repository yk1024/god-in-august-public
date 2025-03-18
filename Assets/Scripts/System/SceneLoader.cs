using UnityEngine;
using UnityEngine.SceneManagement;

namespace GodInAugust.System
{
[AddComponentMenu("God In August/System/Scene Loader")]
public class SceneLoader : MonoBehaviour
{
    [SerializeField, Tooltip("読み込むシーン名")]
    private string sceneName;

    public void LoadScene()
    {
        SceneManager.LoadScene(sceneName);
    }
}
}
