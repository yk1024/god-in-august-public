using TMPro;
using UnityEngine;
using GodInAugust.System;

namespace GodInAugust.Level
{
/// <summary>
/// カレンダーの日付を制御するコンポーネント
/// </summary>
[AddComponentMenu("God In August/Level/Calendar")]
public class Calendar : MonoBehaviour
{
    [SerializeField, Tooltip("日付用のテキスト")]
    private TextMeshPro dateText;

    private void Start()
    {
        GameState gameState = GameState.State;

        // GameManagerで指定されている最初の日付と現在の日付のインデックスを加算して、現在の日付を計算
        dateText.SetText((GameManager.Instance.StartDate + gameState.DateIndex).ToString());
    }
}
}
