using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField, Header("Anomaly Settings")]
    private GameObject[] anomalies;

    [SerializeField, Range(0, 1)]
    private float probability;

    [SerializeField, Header("UI")]
    private Animator overlayPanelAnimator;

    private PlayerInput playerInput;

    private PrayType prayType = PrayType.None;

    public PrayType PrayType
    {
        set
        {
            prayType = value;
        }
    }

    private bool anomalyExists;

    // Start is called before the first frame update
    void Start()
    {
        playerInput = FindObjectOfType<PlayerInput>();

        StartDay();

        if (GameState.LoopIndex != 0 || GameState.DateIndex != 0)
        {
            SetupAnomaly();
        }

        Debug.Log($"Date: {GameState.DateIndex}; Loop: {GameState.LoopIndex}; anomalyExists: {anomalyExists}");
    }

    // Update is called once per frame
    void Update()
    {

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

    private void StartDay()
    {
        overlayPanelAnimator.SetTrigger(Constants.FadeInTrigger);
        Invoke("EnableInput", 5);
    }

    private void EnableInput()
    {
        playerInput.SwitchCurrentActionMap(Constants.PlayerActionMap);
    }

    public void EndDay()
    {
        playerInput.DeactivateInput();
        overlayPanelAnimator.SetTrigger(Constants.FadeOutTrigger);
        Invoke("LoadNextDay", 5);
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
