using UnityEngine;

public class Shrine : MonoBehaviour, IInteractable
{
    [SerializeField]
    private GameObject prayPanel;

    private bool alreadyPrayed = false;

    private GameManager gameManager;

    [field: SerializeField]
    public Transform TargetPoint { get; private set; }

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
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

    public void FirstPray()
    {
        PrayForGratitude();
        FindObjectOfType<Bed>().Available = true;
    }
}
