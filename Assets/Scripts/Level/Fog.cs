using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace GodInAugust.Level
{
[VolumeComponentMenu("God In August/Level/Fog")]
public class Fog : VolumeComponent, IPostProcessComponent
{
    [field: SerializeField, Tooltip("フォグの深さ")]
    public MinFloatParameter Intensity { get; private set; } = new MinFloatParameter(0, 0);

    [field: SerializeField, Tooltip("フォグの色")]
    public NoInterpColorParameter FogColor { get; private set; } = new NoInterpColorParameter(Color.white);

    public bool IsActive() => Intensity.value != 0;
    public bool IsTileCompatible() => false;
}
}
