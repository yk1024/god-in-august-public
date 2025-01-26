using UnityEngine;

public class Area : MonoBehaviour
{
    [SerializeField]
    private AK.Wwise.State AreaState;

    private MusicManager musicManager;

    void Start()
    {
        musicManager = FindObjectOfType<MusicManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constants.PlayerTag))
        {
            musicManager.AddAreaState(AreaState);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(Constants.PlayerTag))
        {
            musicManager.RemoveAreaState(AreaState);
        }
    }
}
