using TMPro;
using UnityEngine;
using GodInAugust.Agent;

namespace GodInAugust.System
{
/// <summary>
/// デバッグ時に便利な機能を実装するためのコンポーネント
/// </summary>
[AddComponentMenu("God In August/Debug/Debugger")]
public class Debugger : MonoBehaviour
{
    // シーン上のゲームマネージャー
    private GameManager gameManager;

    // シーン上のFootstepManager
    private FootstepManager footstepManager;

    [SerializeField, Tooltip("ステータス表示用のテキスト")]
    private TextMeshProUGUI textMeshProUGUI;

    private void Start()
    {
        gameManager = GameManager.Instance;
        footstepManager = PlayerController.Instance.GetComponentInChildren<FootstepManager>();
    }

    private void Update()
    {
        GameState gameState = GameState.State;
        MusicManager musicManager = MusicManager.Instance;

        // 現在の各種ステータスを表示する。
        string text =
            $"日付: {gameState.DateIndex} | " +
            $"日単位のループ: {gameState.DailyLoopIndex} | " +
            $"振り出しループ: {gameState.OverallLoopIndex} | " +
            $"異変有無: {gameManager.AnomalyExists} | " +
            $"異変: {gameManager.Anomaly?.name} | " +
            $"祈り種別: {gameManager.PrayType}\n" +
            $"床: {footstepManager.GroundSwitch} | " +
            $"移動速度: {musicManager.MoveSpeedSwitch} | " +
            $"エリア: {musicManager.AreaState} | " +
            $"異変からの近さ: {musicManager.ProximityToAnomaly}";

        textMeshProUGUI.SetText(text);
    }

    /// <summary>
    /// デバッグ用の寝るコマンド。エディタ拡張で使用する。
    /// </summary>
    public void Sleep()
    {
        StartCoroutine(gameManager.EndDay());
    }

    /// <summary>
    /// デバッグ用の感謝コマンド。エディタ拡張で使用する。
    /// </summary>
    public void PrayForGratitude()
    {
        gameManager.PrayType = PrayType.Gratitude;
    }

    /// <summary>
    /// デバッグ用のお願いコマンド。エディタ拡張で使用する。
    /// </summary>
    public void PrayForWish()
    {
        gameManager.PrayType = PrayType.Wish;
    }
}
}
