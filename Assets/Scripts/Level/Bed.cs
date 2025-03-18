using UnityEngine;
using GodInAugust.System;

namespace GodInAugust.Level
{
[AddComponentMenu("God In August/Level/Bed")]
public class Bed : MonoBehaviour, IInteractable
{
    [SerializeField, Tooltip("確認パネル")]
    private GameObject confirmationPanel;

    private GameManager gameManager;

    [field: SerializeField, Tooltip("インタラクトの対象位置")]
    public Transform TargetPoint { get; private set; }

    public bool Available { get; set; } = true;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        if (GameState.State.DateIndex == 0) Available = false;
    }

    public void Interact()
    {
        if (Available)
        {
            confirmationPanel.SetActive(true);
        }
    }

    public void Sleep()
    {
        confirmationPanel.SetActive(false);
        StartCoroutine(gameManager.EndDay());
    }
}
}
