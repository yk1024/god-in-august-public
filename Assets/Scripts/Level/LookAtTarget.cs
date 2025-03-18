using UnityEngine;

namespace GodInAugust.Level
{
[AddComponentMenu("God In August/Level/Look At Target")]
public class LookAtTarget : MonoBehaviour, ILookAtTarget
{
    public Transform TargetPoint { get => targetPoint; }

    [SerializeField]
    private Transform targetPoint;
}
}
