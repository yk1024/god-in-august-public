using UnityEngine;

public class Bed : MonoBehaviour, IInteractable
{
    [SerializeField]
    private GameObject confirmationPanel;

    private GameManager gameManager;

    public Transform TargetPoint { get => targetPoint; }

    [SerializeField]
    private Transform targetPoint;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    public void Interact()
    {
        confirmationPanel.SetActive(true);
    }

    public void Sleep()
    {
        gameManager.EndDay();
        confirmationPanel.SetActive(false);
    }
}
