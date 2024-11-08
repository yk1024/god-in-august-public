using System.Collections.Generic;

public class GameState
{
    private GameState() { }
    public int DateIndex { get; set; } = 0;
    public int LoopIndex { get => DailyLoopIndex + OverallLoopIndex; }
    public int DailyLoopIndex { get; set; } = 0;
    public int OverallLoopIndex { get; set; } = 0;
    public List<PrayHistory> PrayHistory { get; } = new List<PrayHistory>();

    public static GameState State;

    public static GameState NewGame()
    {
        State = new GameState();
        return State;
    }
}
