using TMPro;
using UnityEngine;

public class Debugger : MonoBehaviour
{
    private GameManager gameManager;
    private PlayerController player;
    private FootstepManager footstepManager;
    private MusicManager musicManager;

    [SerializeField]
    private TextMeshProUGUI textMeshProUGUI;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        player = FindObjectOfType<PlayerController>();
        footstepManager = player.GetComponentInChildren<FootstepManager>();
        musicManager = FindObjectOfType<MusicManager>();
    }

    void Update()
    {
        GameState gameState = GameState.State;

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

    public void Sleep()
    {
        StartCoroutine(gameManager.EndDay());
    }

    public void PrayForGratitude()
    {
        gameManager.PrayType = PrayType.Gratitude;
    }

    public void PrayForWish()
    {
        gameManager.PrayType = PrayType.Wish;
    }
}
