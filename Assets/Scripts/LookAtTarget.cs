using UnityEngine;

public class LookAtTarget : MonoBehaviour, ILookAtTarget
{
    public Transform TargetPoint { get => targetPoint; }

    [SerializeField]
    private Transform targetPoint;
}
