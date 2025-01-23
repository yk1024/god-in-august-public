using StarterAssets;
using UnityEngine;

public class FootstepManager : MonoBehaviour
{
    [SerializeField, Header("Footstep Settings")]
    private AK.Wwise.Event footstepEvent;

    [SerializeField]
    private AK.Wwise.Switch defaultGroundSwitch;

    private StarterAssetsInputs starterAssetsInputs;

    [SerializeField, Header("BGM Settings")]
    private AK.Wwise.Switch stopSwitch;

    [SerializeField]
    private AK.Wwise.Switch walkSwitch;

    [SerializeField]
    private AK.Wwise.Switch sprintSwitch;

    private MusicManager musicManager;

    private void Start()
    {
        starterAssetsInputs = GetComponent<StarterAssetsInputs>();
        musicManager = FindObjectOfType<MusicManager>();
    }

    private void Update()
    {
        if (starterAssetsInputs.move == Vector2.zero)
        {
            stopSwitch.SetValue(musicManager.gameObject);
        }
        else if (starterAssetsInputs.sprint)
        {
            sprintSwitch.SetValue(musicManager.gameObject);
        }
        else
        {
            walkSwitch.SetValue(musicManager.gameObject);
        }
    }

    public void TriggerFootstepSound()
    {
        AK.Wwise.Switch groundSwitch = GetGroundSwitch();
        groundSwitch.SetValue(gameObject);

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
