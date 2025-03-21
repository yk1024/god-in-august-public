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

    private void OnTriggerEnter(Collider other)
    {
        // プレイヤーと接触した時にエリアを追加する。
        if (other.CompareTag(Constants.PlayerTag))
        {
            MusicManager.Instance.AddAreaState(AreaState);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // プレイヤーから離れた時にエリアを削除する。
        if (other.CompareTag(Constants.PlayerTag))
        {
            MusicManager.Instance.RemoveAreaState(AreaState);
        }
    }
}
}
