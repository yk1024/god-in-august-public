using UnityEngine;
using UnityEngine.InputSystem;

public class Interact : MonoBehaviour
{
    [SerializeField]
    private float distance;

    private bool interacting;
    private LookAtHandler lookAt;

    void Start()
    {
        lookAt = GetComponent<LookAtHandler>();
    }

    void Update()
    {
        (ILookAtTarget target, float distance) = lookAt.LookAtFirstTarget();

        InteractTarget(target, distance);

        interacting = false;
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
}
