using UnityEngine;
using UnityEngine.SceneManagement;

namespace GodInAugust.System
{
[AddComponentMenu("God In August/System/Scene Loader")]
public class SceneLoader : MonoBehaviour
{
    [SerializeField]
    private string sceneName;

    public void LoadScene()
    {
        SceneManager.LoadScene(sceneName);
    }
}
}
