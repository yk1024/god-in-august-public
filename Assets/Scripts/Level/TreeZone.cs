using UnityEngine;
using GodInAugust.Agent;

namespace GodInAugust.Level
{
/// <summary>
/// 林の木のざわめきを実装するコンポーネント
/// </summary>
[AddComponentMenu("God In August/Level/Tree Zone")]
public class TreeZone : MonoBehaviour
{
    // 同じオブジェクトに付された、林の範囲を表すコライダー
    private Collider treeZoneCollider;

    [SerializeField, Tooltip("発音位置のトランスフォーム")]
    private Transform soundPosition;

    [SerializeField, Tooltip("林の大きさ")]
    private float size;

    [SerializeField, Tooltip("木のざわめき用のWwiseイベント")]
    private AK.Wwise.Event playEvent;

    [SerializeField, Tooltip("林の大きさのRTPC")]
    private AK.Wwise.RTPC treeZoneSizeParameter;

    private void Start()
    {
        treeZoneCollider = GetComponent<Collider>();
        treeZoneSizeParameter.SetValue(soundPosition.gameObject, size);
        playEvent.Post(soundPosition.gameObject);
    }

    private void Update()
    {
        // プレイヤーの位置に最も近い林内のポイントを取得して、発音位置をそこに指定する。
        Vector3 position = treeZoneCollider.ClosestPoint(PlayerController.Instance.transform.position);
        soundPosition.position = position;
    }
}
}
