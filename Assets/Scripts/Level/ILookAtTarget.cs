using UnityEngine;

namespace GodInAugust.Level
{
/// <summary>
/// 視対象となるオブジェクトに設定するコンポーネント用のインターフェース
/// </summary>
public interface ILookAtTarget
{
    /// <summary>
    /// 視対象となる時に見る点の位置を表すトランスフォーム
    /// </summary>
    public Transform TargetPoint { get; }
}
}
