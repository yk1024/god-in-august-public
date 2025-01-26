using StarterAssets;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float distance;

    private bool interacting;
    private LookAtHandler lookAt;

    private StarterAssetsInputs starterAssetsInputs;

    private MusicManager musicManager;

    void Start()
    {
        lookAt = GetComponent<LookAtHandler>();
        starterAssetsInputs = GetComponent<StarterAssetsInputs>();
        musicManager = FindObjectOfType<MusicManager>();
    }

    void Update()
    {
        (ILookAtTarget target, float distance) = lookAt.LookAtFirstTarget();

        InteractTarget(target, distance);

        interacting = false;

        MoveSpeed moveSpeed = GetMoveSpeed();

        musicManager.SetMoveSpeed(moveSpeed);
    }

    public void OnInteract(InputValue inputValue)
    {
        interacting = true;
    }

    private void InteractTarget(ILookAtTarget target, float distance)
    {
        if (interacting && target is IInteractable interactable && distance <= this.distance)
        {
            interactable.Interact();
        }
    }

    private MoveSpeed GetMoveSpeed()
    {
        if (starterAssetsInputs.move == Vector2.zero)
        {
            return MoveSpeed.Stop;
        }
        else if (starterAssetsInputs.sprint)
        {
            return MoveSpeed.Sprint;
        }
        else
        {
            return MoveSpeed.Walk;
        }
    }
}
