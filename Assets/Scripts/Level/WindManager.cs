using UnityEngine;

namespace GodInAugust.Level
{
[AddComponentMenu("God In August/Level/Wind Manager")]
public class WindManager : MonoBehaviour
{
    private WindZone windZone;

    [SerializeField, Tooltip("風の強さのRTPC")]
    private AK.Wwise.RTPC windStrengthParameter;

    void Start()
    {
        windZone = GetComponent<WindZone>();
        windStrengthParameter.SetGlobalValue(windZone.windMain);
    }
}
}
