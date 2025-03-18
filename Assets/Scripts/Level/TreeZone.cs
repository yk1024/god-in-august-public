using UnityEngine;
using GodInAugust.Agent;

namespace GodInAugust.Level
{
[AddComponentMenu("God In August/Level/Tree Zone")]
public class TreeZone : MonoBehaviour
{
    private PlayerController player;

    private Collider treeZoneCollider;

    [SerializeField, Tooltip("発音位置のトランスフォーム")]
    private Transform soundPosition;

    [SerializeField, Tooltip("林の大きさ")]
    private float size;

    [SerializeField, Tooltip("木のざわめき用のWwiseイベント")]
    private AK.Wwise.Event playEvent;

    [SerializeField, Tooltip("林の大きさのRTPC")]
    private AK.Wwise.RTPC treeZoneSizeParameter;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerController>();
        treeZoneCollider = GetComponent<Collider>();
        treeZoneSizeParameter.SetValue(soundPosition.gameObject, size);
        playEvent.Post(soundPosition.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 position = treeZoneCollider.ClosestPoint(player.transform.position);
        soundPosition.position = position;
    }
}
}
