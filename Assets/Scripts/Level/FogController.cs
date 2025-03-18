using UnityEngine;
using UnityEngine.Rendering;

namespace GodInAugust.Level
{
[AddComponentMenu("God In August/Level/Fog Controller")]
public class FogController : MonoBehaviour
{
    [SerializeField]
    private FullScreenPassRendererFeature rendererFeature;

    private Fog fog;

    void Start()
    {
        fog = VolumeManager.instance.stack.GetComponent<Fog>();
    }

    void Update()
    {
        if (fog.IsActive())
        {
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
