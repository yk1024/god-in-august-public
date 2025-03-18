using TMPro;
using UnityEngine;

namespace GodInAugust.Anomalies
{
[AddComponentMenu("God In August/Anomalies/Anomalous Character")]
public class AnomalousCharacter : Anomaly
{
    [SerializeField, Tooltip("異変で変更するレンダラー")]
    private Renderer anomalousRenderer;

    [SerializeField, Tooltip("異変用マテリアル")]
    private Material anomalousMaterial;

    [SerializeField, Tooltip("異変で変更するポップアップメッセージ")]
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
