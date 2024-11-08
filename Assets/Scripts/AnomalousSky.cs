using UnityEngine;

public class AnomalousSky : MonoBehaviour, IAnomaly
{
    [SerializeField]
    private Material skyboxMaterial;

    public void OnOccur()
    {
        RenderSettings.skybox = skyboxMaterial;
    }
}
