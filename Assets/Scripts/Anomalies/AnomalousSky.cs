using UnityEngine;

namespace GodInAugust.Anomalies
{
/// <summary>
/// 空の異変を発生させるコンポーネント
/// Skyboxのマテリアルを変化させられる。
/// </summary>
[AddComponentMenu("God In August/Anomalies/Anomalous Sky")]
public class AnomalousSky : Anomaly
{
    [SerializeField, Tooltip("異変用マテリアル")]
    private Material skyboxMaterial;

    protected override void Start()
    {
        base.Start();

        // Skyboxのマテリアルを指定したものに変化させる。
        RenderSettings.skybox = skyboxMaterial;
    }
}
}
