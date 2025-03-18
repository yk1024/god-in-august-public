using UnityEngine;

namespace GodInAugust.Anomalies
{
[AddComponentMenu("God In August/Anomalies/Anomalous Sky")]
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
