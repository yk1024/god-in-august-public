using UnityEngine;

namespace GodInAugust.Level
{
public class WindManager : MonoBehaviour
{
    private WindZone windZone;

    [SerializeField]
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
