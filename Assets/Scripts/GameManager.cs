using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField, Header("Anomaly Settings")]
    private GameObject[] anomalies;

    [SerializeField, Range(0, 1)]
    private float probability;

    [SerializeField, Header("Ending Settings")]
    private int duration;

    [SerializeField]
    private string nextSceneName;

    private OverlayPanel overlayPanel;

    private PlayerInput playerInput;

    private PrayType prayType = PrayType.None;

    public PrayType PrayType { set => prayType = value; }

    private bool anomalyExists;

    private GameState gameState;

    void Start()
    {
        gameState = GameState.State ?? GameState.NewGame();

        playerInput = FindObjectOfType<PlayerInput>();
        overlayPanel = FindObjectOfType<OverlayPanel>();

        StartCoroutine(StartDay());

        if (gameState.LoopIndex != 0 || gameState.DateIndex != 0)
        {
            SetupAnomaly();
        }

        Cursor.lockState = CursorLockMode.Locked;

        Debug.Log($"Date: {gameState.DateIndex}; Loop: {gameState.LoopIndex}; anomalyExists: {anomalyExists}");
    }

    private void SetupAnomaly()
    {
        float f = Random.value;
        anomalyExists = probability > f;

        if (anomalyExists)
        {
            int i = Random.Range(0, anomalies.Length);
            GameObject anomaly = anomalies[i];
            anomaly.SetActive(true);
        }
    }

    private IEnumerator StartDay()
    {
        yield return overlayPanel.FadeIn();
        EnableInput();
    }

    private void EnableInput()
    {
        playerInput.SwitchCurrentActionMap(Constants.PlayerActionMap);
    }

    public IEnumerator EndDay()
    {
        playerInput.DeactivateInput();
        yield return overlayPanel.FadeOut();
        LoadNextDay();
    }

    private void LoadNextDay()
    {
        if (anomalyExists && prayType == PrayType.Wish)
        {
            gameState.LoopIndex++;
        }
        else if (!anomalyExists && prayType == PrayType.Gratitude)
        {
            gameState.DateIndex++;
        }
        else
        {
            gameState.DateIndex = 0;
            gameState.LoopIndex++;
        }

        string sceneName;

        if (gameState.DateIndex > duration)
        {
            sceneName = nextSceneName;
            Cursor.lockState = CursorLockMode.None;
            GameState.State = null;
        }
        else
        {
            sceneName = SceneManager.GetActiveScene().name;
        }

        SceneManager.LoadScene(sceneName);
    }
}
