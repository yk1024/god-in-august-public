using System.Collections;
using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;
using GodInAugust.System;
using GodInAugust.UI;
using GodInAugust.Agent;

namespace GodInAugust.Level
{
[AddComponentMenu("God In August/Level/Shrine")]
public class Shrine : MonoBehaviour, IInteractable
{
    [SerializeField, Tooltip("祈りパネル")]
    private UIPanel prayPanel;

    private bool alreadyPrayed = false;

    private GameManager gameManager;

    [field: SerializeField, Tooltip("インタラクトの対象位置")]
    public Transform TargetPoint { get; private set; }

    private PlayerController player;

    [SerializeField, Tooltip("祈る立ち位置　")]
    private Transform prayPosition;

    [SerializeField, Tooltip("祈るシーン用のカメラ")]
    private CinemachineVirtualCamera prayCamera;

    private CinemachineBrain cinemachineBrain;

    private OverlayPanel overlayPanel;
    private PlayerInput playerInput;

    [SerializeField, Tooltip("祈りのシーン前後のフェード時間")]
    private float fadeTime;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        player = FindObjectOfType<PlayerController>();
        overlayPanel = FindObjectOfType<OverlayPanel>();
        prayPanel.OnCancelCallback.AddListener(Cancel);
        playerInput = FindObjectOfType<PlayerInput>();
        cinemachineBrain = FindObjectOfType<CinemachineBrain>();
    }

    private void OnDestroy()
    {
        prayPanel.OnCancelCallback.RemoveListener(Cancel);
    }

    public void Interact()
    {
        if (!alreadyPrayed)
        {
            StartCoroutine(OnInteract());
        }
    }

    private IEnumerator OnInteract()
    {
        yield return overlayPanel.FadeOut(fadeTime);
        prayCamera.Priority = cinemachineBrain.ActiveVirtualCamera.Priority + 1;
        player.transform.SetPositionAndRotation(prayPosition.position, prayPosition.rotation);
        prayPanel.gameObject.SetActive(true);
        yield return overlayPanel.FadeIn(fadeTime);
    }

    private IEnumerator Pray(PrayType prayType)
    {
        gameManager.PrayType = prayType;
        prayPanel.gameObject.SetActive(false);
        alreadyPrayed = true;
        playerInput.DeactivateInput();
        yield return player.Pray();
        playerInput.ActivateInput();
        yield return EndInteraction();
    }

    public void PrayForGratitude()
    {
        StartCoroutine(Pray(PrayType.Gratitude));
    }

    public void PrayForWish()
    {
        StartCoroutine(Pray(PrayType.Wish));
    }

    public void FirstPray()
    {
        PrayForGratitude();
        FindObjectOfType<Bed>().Available = true;
    }

    private void Cancel()
    {
        StartCoroutine(EndInteraction());
    }

    private IEnumerator EndInteraction()
    {
        yield return overlayPanel.FadeOut(fadeTime);
        prayCamera.Priority = 0;
        yield return overlayPanel.FadeIn(fadeTime);
    }
}
}
