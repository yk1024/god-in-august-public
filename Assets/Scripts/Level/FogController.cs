using UnityEngine;
using UnityEngine.Rendering;

namespace GodInAugust.Level
{
/// <summary>
/// フォグを制御するためのコンポーネント
/// </summary>
[AddComponentMenu("God In August/Level/Fog Controller")]
public class FogController : MonoBehaviour
{
    [SerializeField, Tooltip("フォグ表示用のFullScreenPassRendererFeature")]
    private FullScreenPassRendererFeature rendererFeature;

    // シーン上のフォグVolume
    private Fog fog;

    private void Start()
    {
        fog = VolumeManager.instance.stack.GetComponent<Fog>();
    }

    private void Update()
    {
        if (fog.IsActive())
        {
            // フォグ表示用のRendererFeatureを有効化し、マテリアルにフォグの色と濃度を設定する。
            rendererFeature.SetActive(true);
            rendererFeature.passMaterial.SetColor("_FogColor", fog.FogColor.value);
            rendererFeature.passMaterial.SetFloat("_Intensity", fog.Intensity.value);
        }
        else
        {
            rendererFeature.SetActive(false);
        }
    }
}
}
