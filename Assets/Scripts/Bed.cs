using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bed : MonoBehaviour, IInteractable
{
    [SerializeField]
    private GameObject confirmationPanel;

    private GameManager gameManager;

    public Transform TargetPoint { get => targetPoint; }

    [SerializeField]
    private Transform targetPoint;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {

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
