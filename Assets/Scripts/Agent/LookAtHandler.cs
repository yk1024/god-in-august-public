using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using GodInAugust.Level;

namespace GodInAugust.Agent
{
[AddComponentMenu("God In August/Agent/Look At Handler")]
public class LookAtHandler : MonoBehaviour
{
    [SerializeField, Tooltip("視野角")]
    private float viewingAngle;

    [SerializeField, Tooltip("首を動かす速度（ラジアン / 秒）")]
    private float rotationSpeed;

    [SerializeField, Tooltip("反応速度（単位 / 秒）")]
    private float reactionSpeed;

    [SerializeField, Tooltip("視対象のトランスフォーム")]
    private Transform lookAtPosition;

    private Dictionary<GameObject, ILookAtTarget> targets = new Dictionary<GameObject, ILookAtTarget>();
    private Transform head;
    private Animator animator;
    private float ikLookAtWeight = 0;

    [SerializeField, Tooltip("一時的に見るのをやめる。（アニメーション用）")]
    private bool suspended = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        head = animator.GetBoneTransform(HumanBodyBones.Head);
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

        return (target, distance);
    }

    void PrepareAnimation(ILookAtTarget target)
    {
        float ikWeightReaction = reactionSpeed * Time.deltaTime;

        if (!suspended && target != null)
        {
            CalculateNextPosition(target.TargetPoint.position);
            ikLookAtWeight += ikWeightReaction;
        }
        else
        {
            ikLookAtWeight -= ikWeightReaction;
        }

        ikLookAtWeight = Mathf.Clamp01(ikLookAtWeight);
    }

    private void CalculateNextPosition(Vector3 targetPosition)
    {
        if (ikLookAtWeight == 0 || lookAtPosition.position == targetPosition) return;

        Vector3 targetDirection = targetPosition - head.position;
        Vector3 currentDirection = lookAtPosition.position - head.position;

        Vector3 nextDirection = Vector3.RotateTowards(currentDirection, targetDirection, rotationSpeed * Time.deltaTime, 0);

        lookAtPosition.position = nextDirection + head.position;
    }

    void OnAnimatorIK(int layerIndex)
    {
        animator.SetLookAtPosition(lookAtPosition.position);
        animator.SetLookAtWeight(ikLookAtWeight);
    }
}
}
