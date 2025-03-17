using UnityEngine;

namespace GodInAugust.Agent
{
public class VehicleController : AgentController
{
    [SerializeField]
    private Transform[] frontTires;

    [SerializeField]
    private AK.Wwise.Event playEngineEvent;

    [SerializeField]
    private AK.Wwise.RTPC speedParameter;

    protected override void Start()
    {
        base.Start();

        playEngineEvent.Post(gameObject);
    }

    protected override void Update()
    {
        base.Update();

        speedParameter.SetValue(gameObject, speed);
    }

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
}
