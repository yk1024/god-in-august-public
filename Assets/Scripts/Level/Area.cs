using UnityEngine;
using GodInAugust.System;

namespace GodInAugust.Level
{
/// <summary>
/// フィールド上のエリアを指定するためのコンポーネント
/// </summary>
[AddComponentMenu("God In August/Level/Area")]
public class Area : MonoBehaviour
{
    [SerializeField, Tooltip("エリアのWwiseステート")]
    private AK.Wwise.State AreaState;

    // シーン上のMusicManager
    private MusicManager musicManager;

    private void Start()
    {
        musicManager = FindObjectOfType<MusicManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        // プレイヤーと接触した時にエリアを追加する。
        if (other.CompareTag(Constants.PlayerTag))
        {
            musicManager.AddAreaState(AreaState);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // プレイヤーから離れた時にエリアを削除する。
        if (other.CompareTag(Constants.PlayerTag))
        {
            musicManager.RemoveAreaState(AreaState);
        }
    }
}
}
