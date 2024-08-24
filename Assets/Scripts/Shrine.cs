using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shrine : MonoBehaviour, IInteractable
{
    [SerializeField]
    private GameObject prayPanel;

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
        prayPanel.SetActive(true);
    }

    public void PrayForGratitude()
    {
        Debug.Log("Prayed For Gratitude");
        prayPanel.SetActive(false);
    }

    public void PrayForWish()
    {
        Debug.Log("Prayed For Wish");
        prayPanel.SetActive(false);
    }
}
