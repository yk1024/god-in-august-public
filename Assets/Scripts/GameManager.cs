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

    private OverlayPanel overlayPanel;

    private PlayerInput playerInput;

    private PrayType prayType = PrayType.None;

    public PrayType PrayType { set => prayType = value; }

    private bool anomalyExists;

    void Start()
    {
        playerInput = FindObjectOfType<PlayerInput>();
        overlayPanel = FindObjectOfType<OverlayPanel>();

        StartCoroutine(StartDay());

        if (GameState.LoopIndex != 0 || GameState.DateIndex != 0)
        {
            SetupAnomaly();
        }

        Debug.Log($"Date: {GameState.DateIndex}; Loop: {GameState.LoopIndex}; anomalyExists: {anomalyExists}");
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
            GameState.LoopIndex++;
        }
        else if (!anomalyExists && prayType == PrayType.Gratitude)
        {
            GameState.DateIndex++;
        }
        else
        {
            GameState.DateIndex = 0;
            GameState.LoopIndex++;
        }

        string sceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(sceneName);
    }
}
