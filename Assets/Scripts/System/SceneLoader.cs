using UnityEngine;
using UnityEngine.SceneManagement;

namespace GodInAugust.System
{
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
