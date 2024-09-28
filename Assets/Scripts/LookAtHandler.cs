using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LookAtHandler : MonoBehaviour
{
    [SerializeField]
    private float viewingAngle;

    [SerializeField]
    private float rotationSpeed;

    [SerializeField]
    private float reactionSpeed;

    private Dictionary<GameObject, ILookAtTarget> targets = new Dictionary<GameObject, ILookAtTarget>();
    private Transform head;
    private Animator animator;
    private float ikLookAtWeight = 0;
    private Vector3 ikLookAtPosition;
    private Vector3 lastHeadPosition;

    void Start()
    {
        animator = GetComponent<Animator>();
        head = animator.GetBoneTransform(HumanBodyBones.Head);
        lastHeadPosition = head.position;
        ikLookAtPosition = head.position + head.forward;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.isTrigger && other.TryGetComponent(out ILookAtTarget target))
        {
            targets.TryAdd(other.gameObject, target);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.isTrigger)
        {
            targets.Remove(other.gameObject);
        }
    }

    public (ILookAtTarget target, float distance) LookAtFirstTarget()
    {
        float distance = 0;

        (GameObject _, ILookAtTarget target) =
            targets
            .OrderBy((target) => Vector3.Distance(head.position, target.Value.TargetPoint.position))
            .FirstOrDefault((target) =>
            {
                Vector3 direction = target.Value.TargetPoint.position - head.position;
                float angle = Vector3.Angle(transform.forward, direction);
                if (angle > viewingAngle) return false;

                if (!Physics.Raycast(head.position, direction, out RaycastHit hit, direction.magnitude)) return false;

                if (hit.collider.gameObject != target.Key) return false;

                distance = hit.distance;
                return true;
            });

        PrepareAnimation(target);
        lastHeadPosition = head.position;

        return (target, distance);
    }

    void PrepareAnimation(ILookAtTarget target)
    {
        float ikWeightReaction = reactionSpeed * Time.deltaTime;

        if (target != null)
        {
            ikLookAtPosition = CalculateNextPosition(target.TargetPoint.position);
            ikLookAtWeight += ikWeightReaction;
        }
        else
        {
            ikLookAtWeight -= ikWeightReaction;
        }

        ikLookAtWeight = Mathf.Clamp01(ikLookAtWeight);
    }

    Vector3 CalculateNextPosition(Vector3 targetPosition)
    {
        if (ikLookAtWeight == 0 || ikLookAtPosition == targetPosition) return targetPosition;

        Vector3 targetDirection = targetPosition - head.position;
        Vector3 currentDirection = ikLookAtPosition - lastHeadPosition;

        Vector3 nextDirection = Vector3.RotateTowards(currentDirection, targetDirection, rotationSpeed * Time.deltaTime, 0);

        return nextDirection + head.position;
    }

    void OnAnimatorIK(int layerIndex)
    {
        animator.SetLookAtPosition(ikLookAtPosition);
        animator.SetLookAtWeight(ikLookAtWeight);
    }
}
