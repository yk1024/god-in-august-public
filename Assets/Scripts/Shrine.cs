using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shrine : MonoBehaviour, IInteractable
{
    [SerializeField]
    private GameObject prayPanel;

    private bool alreadyPrayed = false;

    private GameManager gameManager;

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
        if (!alreadyPrayed)
        {
            prayPanel.SetActive(true);
        }
    }

    private void Pray(PrayType prayType)
    {
        gameManager.PrayType = prayType;
        prayPanel.SetActive(false);
        alreadyPrayed = true;
    }

    public void PrayForGratitude()
    {
        Pray(PrayType.Gratitude);
    }

    public void PrayForWish()
    {
        Pray(PrayType.Wish);
    }
}
