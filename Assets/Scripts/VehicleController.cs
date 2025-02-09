using UnityEngine;

public class VehicleController : AgentController
{
    [SerializeField]
    private Transform[] frontTires;

    private void LateUpdate()
    {
        Vector3 direction = Vector3.ProjectOnPlane(navMeshAgent.desiredVelocity, transform.up);
        float maxRadiansDelta = Time.deltaTime * navMeshAgent.angularSpeed * Mathf.Deg2Rad;

        if (direction != Vector3.zero)
        {
            foreach(Transform tire in frontTires)
            {
                tire.forward = Vector3.RotateTowards(tire.forward, direction, maxRadiansDelta, 0);
            }
        }
    }
}
