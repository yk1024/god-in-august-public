using UnityEngine;
using GodInAugust.System;

namespace GodInAugust.Level
{
[AddComponentMenu("God In August/Level/Area")]
public class Area : MonoBehaviour
{
    [SerializeField, Tooltip("エリアのWwiseステート")]
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
}
