using System.Collections.Generic;

namespace GodInAugust.System
{
/// <summary>
/// ゲームの状態を管理するためのクラス
/// </summary>
public class GameState
{
    // コンストラクターはプライベートにする。
    private GameState() { }

    /// <summary>
    /// 初日から経過した日数のインデックス
    /// </summary>
    public int DateIndex { get; set; } = 0;

    /// <summary>
    /// 通算で何回ループしたか
    /// </summary>
    public int LoopIndex => DailyLoopIndex + OverallLoopIndex;

    /// <summary>
    /// 当日に戻るループ（異変が存在して「お願い」した日）の回数
    /// </summary>
    public int DailyLoopIndex { get; set; } = 0;

    /// <summary>
    /// 異変初日に戻るループ（祈りに失敗した日）の回数
    /// </summary>
    public int OverallLoopIndex { get; set; } = 0;

    /// <summary>
    /// これまでの祈りの全記録を保持するリスト
    /// </summary>
    public List<PrayHistory> PrayHistory { get; } = new List<PrayHistory>();

    // Singletonパターンで使うためのバッキングフィールド
    private static GameState state;

    /// <summary>
    /// 現在のゲームの状態を返す。
    /// 新規ゲームの場合は、新しいインスタンスを作成する。
    /// </summary>
    public static GameState State
    {
        private set => state = value;
        get => state ?? NewGame();
    }

    /// <summary>
    /// ゲームを終了する時に実行するメソッド
    /// </summary>
    public static void EndGame()
    {
        state = null;
    }

    private static GameState NewGame()
    {
        // 新しいインスタンスを生成して返す。
        State = new GameState();
        return State;
    }
}
}
