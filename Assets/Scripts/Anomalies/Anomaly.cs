using UnityEngine;
using GodInAugust.Agent;
using GodInAugust.System;

namespace GodInAugust.Anomalies
{
/// <summary>
/// 異変用のコンポーネント
/// このクラスを継承して、別の異変用のコンポーネントを作れる。
/// </summary>
[AddComponentMenu("God In August/Anomalies/Anomaly")]
public class Anomaly : MonoBehaviour
{
    [SerializeField, Tooltip("ステージ全体で発生する異変かどうか")]
    private bool global;

    [SerializeField, Min(0), Tooltip("異変が発生する半径")]
    private float radius;

    [SerializeField, Min(0), Tooltip("異変に近づいた時に異変の音楽が流れ始める距離")]
    private float blendDistance;

    protected virtual void Update()
    {
        float proximityToAnomaly;

        if (global)
        {
            // globalな異変の場合、異変への近さは常に1である。
            proximityToAnomaly = 1;
        }
        else
        {
            // プレイヤーから異変への方向と距離を計算
            Vector3 direction = PlayerController.Instance.transform.position - transform.position;
            float distance = direction.magnitude;

            // 異変への近さは、
            // ・radius > distance （＝異変の中にプレイヤーがいる）の場合、1になる。
            // ・radius + blendDistance < distance （＝プレイヤーが異変にまだ近づいていない）の場合、0になる。
            // ・radius < distance < radius + blendDistance （＝プレイヤーが異変に近づいている）の場合、0から1の間の値をとる。
            proximityToAnomaly = Mathf.Clamp01(((radius - distance) / blendDistance) + 1);
        }

        MusicManager.Instance.SetProximityToAnomaly(proximityToAnomaly, transform.position);
    }

    /// <summary>
    /// 異変が選ばれて発生した時に呼ばれるメソッド
    /// 付されているゲームオブジェクトをアクティブにし、このコンポーネントを有効にする。
    /// </summary>
    public void OnOccur()
    {
        gameObject.SetActive(true);
        enabled = true;
    }
}
}
