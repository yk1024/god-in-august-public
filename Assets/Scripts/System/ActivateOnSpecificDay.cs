using UnityEngine;

namespace GodInAugust.System
{
[AddComponentMenu("God In August/System/Activate On Specific Day")]
public class ActivateOnSpecificDay : MonoBehaviour
{
    [SerializeField]
    private int dateIndex;

    [SerializeField]
    private bool activationMode;

    public void ActivateOrDeactivate()
    {
        if (GameState.State.DateIndex == dateIndex)
        {
            gameObject.SetActive(activationMode);
        }
    }
}
}
