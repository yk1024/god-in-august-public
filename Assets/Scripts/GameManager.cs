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

    [SerializeField, Header("Text Settings")]
    private string strangenessText;

    private OverlayPanel overlayPanel;
    private Dialogue dialogue;

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
        dialogue = FindObjectOfType<Dialogue>(true);

        StartCoroutine(StartDay());

        if (gameState.DateIndex != 0)
        {
            SetupAnomaly();
        }

        ActivateDailyObjects();

        Cursor.lockState = CursorLockMode.Locked;

        Debug.Log($"Date: {gameState.DateIndex}; DailyLoop: {gameState.DailyLoopIndex}; OverallLoop: {gameState.OverallLoopIndex}; anomalyExists: {anomalyExists}");
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

        if (gameState.LoopIndex == 1 && gameState.PrayHistory[^1].IsLoop())
        {
            playerInput.SwitchCurrentActionMap(Constants.UIActionMap);
            dialogue.gameObject.SetActive(true);
            yield return dialogue.ShowText(strangenessText);
            dialogue.gameObject.SetActive(false);
        }

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
        PrayHistory prayHistory = new PrayHistory(prayType, anomalyExists);
        gameState.PrayHistory.Add(prayHistory);

        if (prayHistory.IsDailyLoop())
        {
            gameState.DailyLoopIndex++;
        }
        else if (prayHistory.IsProceed())
        {
            gameState.DateIndex++;
        }
        else
        {
            gameState.DateIndex = 1;
            gameState.OverallLoopIndex++;
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

    private void ActivateDailyObjects()
    {
        foreach (ActivateOnSpecificDay activateOnSpecificDay in FindObjectsOfType<ActivateOnSpecificDay>(true))
        {
            activateOnSpecificDay.ActivateOrDeactivate();
        }
    }
}
