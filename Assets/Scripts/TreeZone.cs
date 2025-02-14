using UnityEngine;

public class TreeZone : MonoBehaviour
{
    private PlayerController player;

    private Collider treeZoneCollider;

    [SerializeField]
    private Transform soundPosition;

    [SerializeField]
    private float size;

    [SerializeField]
    private AK.Wwise.Event playEvent;

    [SerializeField]
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
