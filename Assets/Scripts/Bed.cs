using UnityEngine;

public class Bed : MonoBehaviour, IInteractable
{
    [SerializeField]
    private GameObject confirmationPanel;

    private GameManager gameManager;

    [field: SerializeField]
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
