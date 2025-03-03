using System.Collections.Generic;

public class GameState
{
    private GameState() { }
    public int DateIndex { get; set; } = 0;
    public int LoopIndex { get => DailyLoopIndex + OverallLoopIndex; }
    public int DailyLoopIndex { get; set; } = 0;
    public int OverallLoopIndex { get; set; } = 0;
    public List<PrayHistory> PrayHistory { get; } = new List<PrayHistory>();

    private static GameState state;

    public static GameState State
    {
        private set => state = value;
        get => state ?? NewGame();
    }

    public static void EndGame()
    {
        state = null;
    }

    private static GameState NewGame()
    {
        State = new GameState();
        return State;
    }
}
