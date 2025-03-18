using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using GodInAugust.Anomalies;
using GodInAugust.UI;

namespace GodInAugust.System
{
[AddComponentMenu("God In August/System/Game Manager")]
public class GameManager : MonoBehaviour
{
    [SerializeField, Header("Anomaly Settings"), Tooltip("発生する異変")]
    private Anomaly[] anomalies;

    [SerializeField, Range(0, 1), Tooltip("異変発生確率")]
    private float probability;

    [SerializeField, Header("Ending Settings"), Tooltip("異変が発生する日数")]
    private int duration;

    [SerializeField, Tooltip("次のシーン名")]
    private string nextSceneName;

    [SerializeField, Header("Text Settings"), Tooltip("初めて日付が巻き戻った時のメッセージ")]
    private string strangenessText;

    [field: SerializeField, Header("Date Settings"), Tooltip("初日の日付")]
    public int StartDate { get; private set; }

    private OverlayPanel overlayPanel;
    private Dialogue dialogue;

    private PlayerInput playerInput;

    public PrayType PrayType { get; set; } = PrayType.None;

    public bool AnomalyExists { get; private set; }
    public Anomaly Anomaly { get; private set; }

    private GameState gameState;

    [SerializeField, Header("Event"), Tooltip("全てのサウンドを止めるWwiseイベント")]
    private AK.Wwise.Event stopAllEvent;

    void Start()
    {
        gameState = GameState.State;

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
        yield return overlayPanel.FadeIn(5);

        if (gameState.LoopIndex == 1 && gameState.PrayHistory[^1].IsLoop())
        {
            playerInput.SwitchCurrentActionMap(Constants.UIActionMap);
            dialogue.gameObject.SetActive(true);
            yield return dialogue.ShowText(strangenessText);
            playerInput.SwitchCurrentActionMap(Constants.PlayerActionMap);
            dialogue.gameObject.SetActive(false);
        }
    }

    public IEnumerator EndDay()
    {
        stopAllEvent.Post(gameObject);
        yield return overlayPanel.FadeOut(5);
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
            GameState.EndGame();
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
}
