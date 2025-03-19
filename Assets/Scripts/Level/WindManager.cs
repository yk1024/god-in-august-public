using UnityEngine;

namespace GodInAugust.Level
{
/// <summary>
/// 風の強さを制御するコンポーネント
/// </summary>
[AddComponentMenu("God In August/Level/Wind Manager")]
public class WindManager : MonoBehaviour
{
    // 同じオブジェクトに付されたWindZone
    private WindZone windZone;

    [SerializeField, Tooltip("風の強さのRTPC")]
    private AK.Wwise.RTPC windStrengthParameter;

    void Start()
    {
        // WindZoneの風の強さをRTPCに設定する。
        windZone = GetComponent<WindZone>();
        windStrengthParameter.SetGlobalValue(windZone.windMain);
    }
}
}
