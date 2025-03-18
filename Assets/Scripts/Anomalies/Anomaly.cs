using UnityEngine;
using GodInAugust.Agent;
using GodInAugust.System;

namespace GodInAugust.Anomalies
{
[AddComponentMenu("God In August/Anomalies/Anomaly")]
public class Anomaly : MonoBehaviour
{
    [SerializeField, Tooltip("ステージ全体で発生する異変かどうか")]
    private bool global;

    [SerializeField, Min(0), Tooltip("異変が発生する半径")]
    private float radius;

    [SerializeField, Min(0), Tooltip("異変に近づいた時に異変の音楽が流れ始める距離")]
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

        musicManager.SetProximityToAnomaly(proximityToAnomaly, transform.position);
    }

    public void OnOccur()
    {
        gameObject.SetActive(true);
        enabled = true;
    }
}
}
