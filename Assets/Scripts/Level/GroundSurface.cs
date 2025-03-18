using UnityEngine;

namespace GodInAugust.Level
{
[AddComponentMenu("God In August/Level/Ground Surface")]
public class GroundSurface : MonoBehaviour
{
    [field: SerializeField, Tooltip("地面のWwiseスイッチ")]
    public AK.Wwise.Switch GroundSwitch { get; private set; }
}
}
