using UnityEngine;

public class AnomalousMusicController : MonoBehaviour
{
    [SerializeField]
    private bool global;

    [SerializeField, Min(0)]
    private float radius;

    [SerializeField, Min(0)]
    private float blendDistance;

    private GameObject player;

    private MusicManager musicManager;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag(Constants.PlayerTag);
        musicManager = FindObjectOfType<MusicManager>();
    }

    // Update is called once per frame
    void Update()
    {
        float vicinityToAnomaly;

        if (global)
        {
            vicinityToAnomaly = 1;
        }
        else
        {
            Vector3 direction = player.transform.position - transform.position;
            float distance = direction.magnitude;

            vicinityToAnomaly = Mathf.Clamp01(((radius - distance) / blendDistance) + 1);
        }

        musicManager.SetVicinityToAnomaly(vicinityToAnomaly);
    }
}
