using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Interact : MonoBehaviour
{
    [SerializeField]
    private Vector2 area;

    private Vector3 halfExtent;

    [SerializeField]
    private float distance;

    [SerializeField]
    private Transform interactionPosition;

    // Start is called before the first frame update
    void Start()
    {
        halfExtent = 0.5f * area;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnInteract(InputValue inputValue)
    {
        if (Physics.BoxCast(interactionPosition.position, halfExtent, interactionPosition.forward, out RaycastHit hit, Quaternion.identity, distance))
        {
            if (hit.collider.gameObject.TryGetComponent(out IInteractable interactable))
            {
                interactable.Interact();
            }
        }
    }
}
