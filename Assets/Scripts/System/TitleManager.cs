using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GodInAugust.System
{
/// <summary>
/// タイトル画面を制御するためのコンポーネント
/// </summary>
[AddComponentMenu("God In August/System/Title Manager")]
public class TitleManager : MonoBehaviour
{
    [SerializeField, Tooltip("タイトルBGM再生イベント")]
    private AK.Wwise.Event playBGMEvent;

    [SerializeField, Tooltip("タイトルBGM停止イベント")]
    private AK.Wwise.Event stopBGMEvent;

    [SerializeField, Tooltip("次のシーン名")]
    private string nextSceneName;

    [SerializeField, Tooltip("タイトル画面のアニメーター")]
    private Animator animator;

    // フェードアウト用のステート名
    private const string fadeOutState = "FadeOut";

    private void Start()
    {
        // タイトル画面ではカーソルを表示する。
        Cursor.lockState = CursorLockMode.None;

        // タイトル用BGMを再生
        playBGMEvent.Post(gameObject);
    }

    /// <summary>
    /// ゲームを開始するメソッド
    /// </summary>
    public void StartGame()
    {
        StartCoroutine(LoadNextScene());
    }

    // タイトル画面を終えて、次のシーンを読み込む。
    private IEnumerator LoadNextScene()
    {
        // タイトル用BGMを停止
        stopBGMEvent.Post(gameObject);

        // 画面をフェードアウト
        animator.Play(fadeOutState);

        // アニメーターに付されたイベントハンドラーを介してフェードアウト終了をまで待つ。
        EventHandler eventHandler = animator.GetComponent<EventHandler>();
        yield return Utilities.WaitForEvent(eventHandler.UnityEvent);

        // 次のシーンをロード
        SceneManager.LoadScene(nextSceneName);
    }
}
}
