public class GameState
{
    private GameState() { }
    public int DateIndex { get; set; } = 0;
    public int LoopIndex { get; set; } = 0;

    public static GameState State;

    public static GameState NewGame()
    {
        State = new GameState();
        return State;
    }
}
