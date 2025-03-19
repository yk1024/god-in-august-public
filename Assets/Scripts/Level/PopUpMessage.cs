using UnityEngine;
using GodInAugust.System;

namespace GodInAugust.Level
{
/// <summary>
/// プレイヤーが近くを通った時にポップアップメッセージを表示するためのコンポーネント
/// </summary>
[AddComponentMenu("God In August/Level/Pop Up Message")]
public class PopUpMessage : MonoBehaviour
{
    [SerializeField, Tooltip("ポップアップするゲームオブジェクト")]
    private GameObject popUpObject;

    // シーン上のメインカメラ
    private GameObject mainCamera;

    private void Start()
    {
        mainCamera = GameObject.FindWithTag(Constants.MainCameraTag);
    }

    private void Update()
    {
        if (popUpObject.activeInHierarchy)
        {
            // ポップアップしているとき、ポップアップしているオブジェクトが常にカメラと同じ方向を向いているようにする。
            popUpObject.transform.forward = mainCamera.transform.forward;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constants.PlayerTag))
        {
            // プレイヤーとトリガーが接触している時、ポップアップするオブジェクトをアクティブにする。
            popUpObject.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(Constants.PlayerTag))
        {
            // プレイヤーとトリガーが離れた時、ポップアップするオブジェクトを非アクティブにする。
            popUpObject.SetActive(false);
        }
    }
}
}
