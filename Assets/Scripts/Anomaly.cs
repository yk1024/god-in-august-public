using UnityEngine;

public class Anomaly : MonoBehaviour
{
    [SerializeField]
    private bool global;

    [SerializeField, Min(0)]
    private float radius;

    [SerializeField, Min(0)]
    private float blendDistance;

    private PlayerController player;

    private MusicManager musicManager;

    protected virtual void Start()
    {
        player = FindObjectOfType<PlayerController>();
        musicManager = FindObjectOfType<MusicManager>();
    }

    protected virtual void Update()
    {
        float proximityToAnomaly;

        if (global)
        {
            proximityToAnomaly = 1;
        }
        else
        {
            Vector3 direction = player.transform.position - transform.position;
            float distance = direction.magnitude;

            proximityToAnomaly = Mathf.Clamp01(((radius - distance) / blendDistance) + 1);
        }

        musicManager.SetProximityToAnomaly(proximityToAnomaly);
    }

    public void OnOccur()
    {
        gameObject.SetActive(true);
        enabled = true;
    }
}
