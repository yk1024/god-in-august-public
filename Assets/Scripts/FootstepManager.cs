using UnityEngine;

public class FootstepManager : MonoBehaviour
{
    [SerializeField]
    private AK.Wwise.Event footstepEvent;

    [SerializeField]
    private AK.Wwise.Switch defaultGroundSwitch;

    public AK.Wwise.Switch GroundSwitch { get; set; }

    public void TriggerFootstepSound()
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
                return groundSurface.groundSwitch;
            }
        }

        return defaultGroundSwitch;
    }
}
