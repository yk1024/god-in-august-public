using System.Collections.Generic;
using UnityEngine;
using GodInAugust.Agent;

namespace GodInAugust.System
{
[AddComponentMenu("God In August/System/Music Manager")]
public class MusicManager : MonoBehaviour
{
    [SerializeField, Header("Event")]
    private AK.Wwise.Event playBGMEvent;

    [SerializeField, Header("Area State")]
    private AK.Wwise.State defaultAreaState;

    public AK.Wwise.State AreaState { get; set; }

    private List<AK.Wwise.State> areaStates = new List<AK.Wwise.State>();

    [SerializeField, Header("Proximity To Anomaly RTPC")]
    private AK.Wwise.RTPC proximityToAnomalyRTPC;

    public float ProximityToAnomaly { get; set; }

    [SerializeField]
    private AK.Wwise.RTPC directionToAnomalyXRTPC;

    [SerializeField]
    private AK.Wwise.RTPC directionToAnomalyYRTPC;

    [SerializeField]
    private AK.Wwise.RTPC directionToAnomalyZRTPC;

    [SerializeField, Header("Move Speed Switch")]
    private AK.Wwise.Switch stopSwitch;

    [SerializeField]
    private AK.Wwise.Switch walkSwitch;

    [SerializeField]
    private AK.Wwise.Switch sprintSwitch;

    public AK.Wwise.Switch MoveSpeedSwitch { get; set; }

    private GameObject mainCamera;

    void Start()
    {
        defaultAreaState.SetValue();
        playBGMEvent.Post(gameObject);
        mainCamera = GameObject.FindWithTag(Constants.MainCameraTag);
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

    public void SetProximityToAnomaly(float proximity, Vector3 anomalyPosition)
    {
        ProximityToAnomaly = proximity;

        Vector3 direction = mainCamera.transform.InverseTransformDirection(anomalyPosition - mainCamera.transform.position);

        direction = direction.normalized * (1 - proximity);

        proximityToAnomalyRTPC.SetValue(gameObject, proximity);
        directionToAnomalyXRTPC.SetValue(gameObject, direction.x);
        directionToAnomalyYRTPC.SetValue(gameObject, direction.y);
        directionToAnomalyZRTPC.SetValue(gameObject, direction.z);
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
}
