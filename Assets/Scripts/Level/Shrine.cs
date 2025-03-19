using System.Collections;
using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;
using GodInAugust.System;
using GodInAugust.UI;
using GodInAugust.Agent;

namespace GodInAugust.Level
{
/// <summary>
/// 社のコンポーネント
/// </summary>
[AddComponentMenu("God In August/Level/Shrine")]
public class Shrine : MonoBehaviour, IInteractable
{
    [SerializeField, Tooltip("祈りパネル")]
    private UIPanel prayPanel;

    // その日既に祈り済みかを表すフラグ
    private bool alreadyPrayed = false;

    // シーン上のゲームマネージャー
    private GameManager gameManager;

    [field: SerializeField, Tooltip("インタラクトの対象位置")]
    public Transform TargetPoint { get; private set; }

    // シーン上のプレイヤー
    private PlayerController player;

    [SerializeField, Tooltip("祈る立ち位置")]
    private Transform prayPosition;

    [SerializeField, Tooltip("祈るシーン用のカメラ")]
    private CinemachineVirtualCamera prayCamera;

    // シーン上のCinemachineBrain
    private CinemachineBrain cinemachineBrain;

    // 祈りに入る前後のフェード用のパネル
    private OverlayPanel overlayPanel;

    // シーン上のプレイヤーインプット
    private PlayerInput playerInput;

    [SerializeField, Tooltip("祈りのシーン前後のフェード時間")]
    private float fadeTime;

    private void Start()
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

    // インタラクトした際に実行する処理
    // フェードしてカメラを変更して、カットシーンに切り替える。
    private IEnumerator OnInteract()
    {
        yield return overlayPanel.FadeOut(fadeTime);
        prayCamera.Priority = cinemachineBrain.ActiveVirtualCamera.Priority + 1;
        player.transform.SetPositionAndRotation(prayPosition.position, prayPosition.rotation);
        prayPanel.gameObject.SetActive(true);
        yield return overlayPanel.FadeIn(fadeTime);
    }

    // 祈るコマンド
    private IEnumerator Pray(PrayType prayType)
    {
        gameManager.PrayType = prayType;
        prayPanel.gameObject.SetActive(false);
        alreadyPrayed = true;

        // 祈り中は入力を受け付けない。
        playerInput.DeactivateInput();
        yield return player.Pray();
        playerInput.ActivateInput();

        yield return EndInteraction();
    }

    /// <summary>
    /// 感謝コマンド
    /// </summary>
    public void PrayForGratitude()
    {
        StartCoroutine(Pray(PrayType.Gratitude));
    }

    /// <summary>
    /// お願いコマンド
    /// </summary>
    public void PrayForWish()
    {
        StartCoroutine(Pray(PrayType.Wish));
    }

    /// <summary>
    /// 初日の祈り。初日は感謝のみ。
    /// </summary>
    public void FirstPray()
    {
        PrayForGratitude();
        FindObjectOfType<Bed>().Available = true;
    }

    // 祈りがキャンセルされた時のメソッド
    private void Cancel()
    {
        StartCoroutine(EndInteraction());
    }

    // 祈り終えた時やキャンセルした時のメソッド。
    // フェードしてカットシーンを抜けて、元のカメラに戻す。
    private IEnumerator EndInteraction()
    {
        yield return overlayPanel.FadeOut(fadeTime);
        prayCamera.Priority = 0;
        yield return overlayPanel.FadeIn(fadeTime);
    }
}
}
