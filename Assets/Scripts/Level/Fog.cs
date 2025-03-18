using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace GodInAugust.Level
{
[VolumeComponentMenu("God In August/Level/Fog")]
public class Fog : VolumeComponent, IPostProcessComponent
{
    public MinFloatParameter Intensity = new MinFloatParameter(0, 0);
    public NoInterpColorParameter FogColor = new NoInterpColorParameter(Color.white);

    public bool IsActive() => Intensity.value != 0;
    public bool IsTileCompatible() => false;
}
}
