using TMPro;
using UnityEngine;

namespace GodInAugust.Anomalies
{
public class AnomalousCharacter : Anomaly
{
    [SerializeField]
    private Renderer anomalousRenderer;

    [SerializeField]
    private Material anomalousMaterial;

    [SerializeField]
    private TextMeshProUGUI textMeshProUGUI;

    private const string AnomalousTextCharacters = "!#$%&'()-^¥@[;:],./=~|`{+*}<>?_";

    protected override void Start()
    {
        base.Start();

        Material[] materials = anomalousRenderer.materials;

        for (int i = 0; i < materials.Length; i++)
        {
            materials[i] = anomalousMaterial;
        }

        anomalousRenderer.materials = materials;

        string text = "";
        int availableCharacterCount = AnomalousTextCharacters.Length;

        for (int i = 0; i < textMeshProUGUI.text.Length; i++)
        {
            text += AnomalousTextCharacters[Random.Range(0, availableCharacterCount)];
        }

        textMeshProUGUI.text = text;
    }
}
}
