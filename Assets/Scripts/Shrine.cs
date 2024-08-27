using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shrine : MonoBehaviour, IInteractable
{
    [SerializeField]
    private GameObject prayPanel;

    private bool alreadPrayed = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Interact()
    {
        if (!alreadPrayed)
        {
            prayPanel.SetActive(true);
        }
    }

    public void Pray(PrayType prayType)
    {
        Debug.Log($"Prayed For {prayType}");
        prayPanel.SetActive(false);
        alreadPrayed = true;
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
