using UnityEngine;

namespace GodInAugust.System
{
[AddComponentMenu("God In August/System/Activate On Specific Day")]
public class ActivateOnSpecificDay : MonoBehaviour
{
    [SerializeField, Tooltip("日付のインデックス")]
    private int dateIndex;

    [SerializeField, Tooltip("trueならその日にアクティブにし、falseならその日は非アクティブにする")]
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
