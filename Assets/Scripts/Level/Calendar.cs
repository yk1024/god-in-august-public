using TMPro;
using UnityEngine;
using GodInAugust.System;

namespace GodInAugust.Level
{
[AddComponentMenu("God In August/Level/Calendar")]
public class Calendar : MonoBehaviour
{
    [SerializeField]
    private TextMeshPro dateText;

    void Start()
    {
        GameManager gameManager = FindObjectOfType<GameManager>();
        GameState gameState = GameState.State;
        dateText.SetText((gameManager.StartDate + gameState.DateIndex).ToString());
    }
}
}
