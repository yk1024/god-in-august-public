using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [SerializeField, Header("Event")]
    private AK.Wwise.Event playBGMEvent;

    [SerializeField]
    private AK.Wwise.Event stopBGMEvent;

    [SerializeField, Header("Area State")]
    private AK.Wwise.State defaultAreaState;

    public AK.Wwise.State AreaState { get; set; }

    private List<AK.Wwise.State> areaStates = new List<AK.Wwise.State>();

    [SerializeField, Header("Proximity To Anomaly RTPC")]
    private AK.Wwise.RTPC proximityToAnomalyRTPC;

    public float ProximityToAnomaly { get; set; }

    [SerializeField, Header("Move Speed Switch")]
    private AK.Wwise.Switch stopSwitch;

    [SerializeField]
    private AK.Wwise.Switch walkSwitch;

    [SerializeField]
    private AK.Wwise.Switch sprintSwitch;

    public AK.Wwise.Switch MoveSpeedSwitch { get; set; }

    void Start()
    {
        defaultAreaState.SetValue();
        playBGMEvent.Post(gameObject);
    }

    public void AddAreaState(AK.Wwise.State areaState)
    {
        areaStates.Add(areaState);
        SetAreaState();
    }

    public void RemoveAreaState(AK.Wwise.State areaState)
    {
        areaStates.Remove(areaState);
        SetAreaState();
    }

    private void SetAreaState()
    {
        if (areaStates.Count == 0)
        {
            AreaState = defaultAreaState;
        }
        else
        {
            AreaState = areaStates[0];
        }

        AreaState.SetValue();
    }

    public void FadeOut()
    {
        stopBGMEvent.Post(gameObject);
    }

    public void SetProximityToAnomaly(float proximity)
    {
        ProximityToAnomaly = proximity;
        proximityToAnomalyRTPC.SetValue(gameObject, proximity);
    }

    public void SetMoveSpeed(MoveSpeed moveSpeed)
    {
        MoveSpeedSwitch =
            moveSpeed switch
            {
                MoveSpeed.Walk => walkSwitch,
                MoveSpeed.Sprint => sprintSwitch,
                _ => stopSwitch
            };

        MoveSpeedSwitch.SetValue(gameObject);
    }
}
