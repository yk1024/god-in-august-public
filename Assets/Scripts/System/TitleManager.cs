using UnityEngine;

namespace GodInAugust.System
{
/// <summary>
/// タイトル画面を制御するためのコンポーネント
/// </summary>
[AddComponentMenu("God In August/System/Title Manager")]
public class TitleManager : MonoBehaviour
{
    private void Start()
    {
        // タイトル画面ではカーソルを表示する。
        Cursor.lockState = CursorLockMode.None;
    }
}
}
