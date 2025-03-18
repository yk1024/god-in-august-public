using UnityEngine;
using GodInAugust.Level;

namespace GodInAugust.Agent
{
[AddComponentMenu("God In August/Agent/Footstep Manager")]
public class FootstepManager : MonoBehaviour
{
    [SerializeField, Tooltip("足音のWwiseイベント")]
    private AK.Wwise.Event footstepEvent;

    [SerializeField, Tooltip("デフォルトの地面のWwiseスイッチ")]
    private AK.Wwise.Switch defaultGroundSwitch;

    public AK.Wwise.Switch GroundSwitch { get; set; }

    private void TriggerFootstepSound()
    {
        GroundSwitch = GetGroundSwitch();
        GroundSwitch.SetValue(gameObject);

        footstepEvent.Post(gameObject);
    }

    private AK.Wwise.Switch GetGroundSwitch()
    {
        foreach (RaycastHit hit in Physics.RaycastAll(transform.position, -transform.up, 0.1f))
        {
            if (hit.collider.TryGetComponent(out GroundSurface groundSurface))
            {
                return groundSurface.GroundSwitch;
            }
        }

        return defaultGroundSwitch;
    }

    public void OnFootstep(AnimationEvent animationEvent)
    {
        if (animationEvent.animatorClipInfo.weight > 0.5f)
        {
            TriggerFootstepSound();
        }
    }
}
}
