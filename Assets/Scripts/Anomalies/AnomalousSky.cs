using UnityEngine;

namespace GodInAugust.Anomalies
{
[AddComponentMenu("God In August/Anomalies/Anomalous Sky")]
public class AnomalousSky : Anomaly
{
    [SerializeField, Tooltip("異変用マテリアル")]
    private Material skyboxMaterial;

    protected override void Start()
    {
        base.Start();

        RenderSettings.skybox = skyboxMaterial;
    }
}
}
