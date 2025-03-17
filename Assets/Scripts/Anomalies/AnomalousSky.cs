using UnityEngine;

namespace GodInAugust.Anomalies
{
public class AnomalousSky : Anomaly
{
    [SerializeField]
    private Material skyboxMaterial;

    protected override void Start()
    {
        base.Start();

        RenderSettings.skybox = skyboxMaterial;
    }
}
}
