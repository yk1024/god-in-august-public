using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField, Header("Anomaly Settings")]
    private Anomaly[] anomalies;

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

    public PrayType PrayType { get; set; } = PrayType.None;

    public bool AnomalyExists { get; private set; }
    public Anomaly Anomaly { get; private set; }

    private GameState gameState;

    [SerializeField, Header("Event")]
    private AK.Wwise.Event playAmbienceEvent;

    void Start()
    {
        gameState = GameState.State ?? GameState.NewGame();

        playerInput = FindObjectOfType<PlayerInput>();
        overlayPanel = FindObjectOfType<OverlayPanel>();
        dialogue = FindObjectOfType<Dialogue>(true);

        StartCoroutine(StartDay());

        SetupAnomaly();

        ActivateDailyObjects();

        Cursor.lockState = CursorLockMode.Locked;
    }

    private void SetupAnomaly()
    {
        if (gameState.DateIndex == 0)
        {
            AnomalyExists = false;
        }
        else if (gameState.LoopIndex == 0)
        {
            AnomalyExists = true;
        }
        else
        {
            float f = Random.value;
            AnomalyExists = probability > f;
        }

        if (AnomalyExists)
        {
            int i = Random.Range(0, anomalies.Length);
            Anomaly = anomalies[i];
            Anomaly.OnOccur();
        }
    }

    private IEnumerator StartDay()
    {
        playAmbienceEvent.Post(gameObject);

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
        FindObjectOfType<MusicManager>().FadeOut();
        yield return overlayPanel.FadeOut();
        LoadNextDay();
    }

    private void LoadNextDay()
    {
        PrayHistory prayHistory = new PrayHistory(PrayType, AnomalyExists);
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
