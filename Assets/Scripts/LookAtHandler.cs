using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UniVRM10;

public class LookAtHandler : MonoBehaviour
{
    private float viewingAngle = 60;
    private Dictionary<GameObject, ILookAtTarget> targets = new Dictionary<GameObject, ILookAtTarget>();
    private Vrm10Instance vrm10Instance;
    private Transform head;
    private Transform neck;
    private Action rotateHeadOnLateUpdate;
    private float rotateSpeed = 90;

    // Start is called before the first frame update
    void Start()
    {
        vrm10Instance = GetComponent<Vrm10Instance>();
        vrm10Instance.TryGetBoneTransform(HumanBodyBones.Head, out head);
        vrm10Instance.TryGetBoneTransform(HumanBodyBones.Neck, out neck);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void LateUpdate()
    {
        if (rotateHeadOnLateUpdate != null)
        {
            rotateHeadOnLateUpdate();
            rotateHeadOnLateUpdate = null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out ILookAtTarget target))
        {
            targets.Add(other.gameObject, target);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        targets.Remove(other.gameObject);
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
                RotateHead(target.Value);
                return true;
            });

        return (target, distance);
    }

    private void RotateHead(ILookAtTarget target)
    {

    }
}
