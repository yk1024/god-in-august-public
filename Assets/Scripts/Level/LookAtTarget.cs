using UnityEngine;

namespace GodInAugust.Level
{
public class LookAtTarget : MonoBehaviour, ILookAtTarget
{
    public Transform TargetPoint { get => targetPoint; }

    [SerializeField]
    private Transform targetPoint;
}
}
