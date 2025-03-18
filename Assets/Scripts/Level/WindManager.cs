using UnityEngine;

namespace GodInAugust.Level
{
[AddComponentMenu("God In August/Level/Wind Manager")]
public class WindManager : MonoBehaviour
{
    private WindZone windZone;

    [SerializeField, Tooltip("風の強さのRTPC")]
    private AK.Wwise.RTPC windStrengthParameter;

    // Start is called before the first frame update
    void Start()
    {
        windZone = GetComponent<WindZone>();
        windStrengthParameter.SetGlobalValue(windZone.windMain);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
}
