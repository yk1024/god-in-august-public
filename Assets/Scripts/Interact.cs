using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Interact : MonoBehaviour
{
    [SerializeField]
    private float distance;

    private bool interacting;
    private LookAtHandler lookAt;

    // Start is called before the first frame update
    void Start()
    {
        lookAt = GetComponent<LookAtHandler>();
    }

    // Update is called once per frame
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
