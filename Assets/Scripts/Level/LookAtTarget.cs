using UnityEngine;

namespace GodInAugust.Level
{
/// <summary>
/// 視対象となるオブジェクト用のコンポーネント
/// </summary>
[AddComponentMenu("God In August/Level/Look At Target")]
public class LookAtTarget : MonoBehaviour, ILookAtTarget
{
    [field: SerializeField, Tooltip("視対象の位置")]
    public Transform TargetPoint { get; private set; }
}
}
