using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using GodInAugust.Anomalies;
using GodInAugust.UI;

namespace GodInAugust.System
{
/// <summary>
/// ゲームプレイ全体を制御するコンポーネント
/// </summary>
[AddComponentMenu("God In August/System/Game Manager")]
public class GameManager : SingletonBehaviour<GameManager>
{
    [SerializeField, Header("Anomaly Settings"), Tooltip("発生する異変")]
    private Anomaly[] anomalies;

    [SerializeField, Range(0, 1), Tooltip("異変発生確率")]
    private float probability;

    [SerializeField, Header("Ending Settings"), Tooltip("異変が発生する日数")]
    private int duration;

    [SerializeField, Tooltip("次のシーン名")]
    private string nextSceneName;

    [SerializeField, Header("Text Settings"), Tooltip("初日に目覚めた時のメッセージ"), TextArea]
    private string[] tutorialText;

    [SerializeField, Tooltip("初めて日付が巻き戻った時のメッセージ"), TextArea]
    private string strangenessText;

    [field: SerializeField, Header("Date Settings"), Tooltip("初日の日付")]
    public int StartDate { get; private set; }

    /// <summary>
    /// その日行った祈りの種類
    /// </summary>
    public PrayType PrayType { get; set; } = PrayType.None;

    /// <summary>
    /// 異変が発生しているか否か
    /// </summary>
    public bool AnomalyExists { get; private set; }

    /// <summary>
    /// 発生している異変
    /// </summary>
    public Anomaly Anomaly { get; private set; }

    // ゲームの状態
    private GameState gameState;

    [SerializeField, Header("Event"), Tooltip("全てのサウンドを止めるWwiseイベント")]
    private AK.Wwise.Event stopAllEvent;

    private void Start()
    {
        gameState = GameState.State;

        // 一日を始めるための処理を実行
        StartCoroutine(StartDay());

        // 異変を設定
        SetupAnomaly();

        // その日にアクティブ化するオブジェクトがあればする。
        ActivateDailyObjects();

        // ゲームプレイ中はカーソルはロックしておく。
        Cursor.lockState = CursorLockMode.Locked;
    }

    // 異変を設定する。
    private void SetupAnomaly()
    {
        if (gameState.DateIndex == 0)
        {
            // 初日は異変が発生しない。
            AnomalyExists = false;
        }
        else if (gameState.LoopIndex == 0)
        {
            // 初日以外でまだループしていない場合（＝2日目）は必ず異変が発生する。
            AnomalyExists = true;
        }
        else
        {
            // それ以外の場合はランダムで異変が発生する。
            float f = Random.value;
            AnomalyExists = probability > f;
        }

        if (AnomalyExists)
        {
            // 異変が発生する場合、異変リストからランダムに取得する。
            int i = Random.Range(0, anomalies.Length);
            Anomaly = anomalies[i];
            Anomaly.OnOccur();
        }
    }

    // 一日の最初に行う処理
    private IEnumerator StartDay()
    {
        // まずフェードインする
        yield return OverlayPanel.Instance.FadeIn(5);

        if (gameState.DateIndex == 0)
        {
            // 初日はチュートリアル用のメッセージを表示する。
            yield return Dialogue.Instance.ShowText(tutorialText);
        }
        else if (gameState.LoopIndex == 1 && gameState.PrayHistory[^1].IsLoop())
        {
            // ループ回数が1で前日にループしている（＝初めてループした）時、違和感を感じるメッセージを表示する。
            yield return Dialogue.Instance.ShowText(strangenessText);
        }
    }

    /// <summary>
    /// 一日を終える時に実行するメソッド
    /// </summary>
    /// <returns>フェードアウトが終わるまで待つためのIEnumerator</returns>
    public IEnumerator EndDay()
    {
        // すべての音を停止する。
        stopAllEvent.Post(gameObject);

        // フェードアウトする。
        yield return OverlayPanel.Instance.FadeOut(5);

        // 次の日を読み込む。
        LoadNextDay();
    }

    // 次の日を読み込む処理。
    private void LoadNextDay()
    {
        // 当日の祈りの内容と異変の有無に応じて、祈りの記録を作って追加する。
        PrayHistory prayHistory = new PrayHistory(PrayType, AnomalyExists);
        gameState.PrayHistory.Add(prayHistory);

        if (prayHistory.IsDailyLoop())
        {
            // 当日を繰り返す場合
            gameState.DailyLoopIndex++;
        }
        else if (prayHistory.IsProceed())
        {
            // 次の日に進める場合
            gameState.DateIndex++;
        }
        else
        {
            // それ以外の場合は、異変初日（DateIndex = 1）に戻る。
            gameState.DateIndex = 1;
            gameState.OverallLoopIndex++;
        }

        string sceneName;

        if (gameState.DateIndex > duration)
        {
            // 日を進めた結果、ゲーム全体の長さを超過していたら、ゲームを終了し、次のシーンに進む。
            sceneName = nextSceneName;
            Cursor.lockState = CursorLockMode.None;
            GameState.EndGame();
        }
        else
        {
            // ゲームが続く場合、現在と同じシーンに進む。
            sceneName = SceneManager.GetActiveScene().name;
        }

        SceneManager.LoadScene(sceneName);
    }

    // 日ごとのオブジェクトを有効化・無効化する。
    private void ActivateDailyObjects()
    {
        foreach (ActivateOnSpecificDay activateOnSpecificDay in FindObjectsOfType<ActivateOnSpecificDay>(true))
        {
            // ActivateOnSpecificDayを検索して、有効化・無効化する。
            activateOnSpecificDay.ActivateOrDeactivate();
        }
    }
}
}
