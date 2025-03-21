using TMPro;
using UnityEngine;

namespace GodInAugust.Anomalies
{
/// <summary>
/// 異変が発生するキャラクターに関するコンポーネント
/// 異変が発生すると、指定したレンダラーのマテリアルが変化し、メッセージが意味不明な文字列になる。
/// </summary>
[AddComponentMenu("God In August/Anomalies/Anomalous Character")]
public class AnomalousCharacter : Anomaly
{
    [SerializeField, Tooltip("異変で変更するレンダラー")]
    private Renderer anomalousRenderer;

    [SerializeField, Tooltip("異変用マテリアル")]
    private Material anomalousMaterial;

    [SerializeField, Tooltip("異変で変更するポップアップメッセージ")]
    private TextMeshProUGUI textMeshProUGUI;

    // ランダムな文字列を生成するために使える文字
    private const string AnomalousTextCharacters = "!#$%&'()-^¥@[;:],./=~|`{+*}<>?_";

    private void Start()
    {
        Material[] materials = anomalousRenderer.materials;

        for (int i = 0; i < materials.Length; i++)
        {
            // 指定されたレンダラーの各マテリアルを変更する。
            materials[i] = anomalousMaterial;
        }

        anomalousRenderer.materials = materials;

        string text = "";

        // ランダムな文字列の長さは、元の文字列と同じにする。
        int availableCharacterCount = AnomalousTextCharacters.Length;

        for (int i = 0; i < textMeshProUGUI.text.Length; i++)
        {
            text += AnomalousTextCharacters[Random.Range(0, availableCharacterCount)];
        }

        textMeshProUGUI.text = text;
    }
}
}
