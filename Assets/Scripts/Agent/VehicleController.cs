using UnityEngine;

namespace GodInAugust.Agent
{
/// <summary>
/// 車両の移動に使うコンポーネント
/// </summary>
[AddComponentMenu("God In August/Agent/Vehicle Controller")]
public class VehicleController : AgentController
{
    [SerializeField, Tooltip("前輪")]
    private Transform[] frontTires;

    [SerializeField, Tooltip("エンジン音再生Wwiseイベント")]
    private AK.Wwise.Event playEngineEvent;

    [SerializeField, Tooltip("速度RTPC")]
    private AK.Wwise.RTPC speedParameter;

    protected override void Start()
    {
        base.Start();

        // エンジン音を開始
        playEngineEvent.Post(gameObject);
    }

    protected override void Update()
    {
        base.Update();

        // 速度を設定
        speedParameter.SetValue(gameObject, speed);
    }

    private void LateUpdate()
    {
        // 車両の水平面上で、車両が進んでいる方向を計算
        Vector3 direction = Vector3.ProjectOnPlane(navMeshAgent.desiredVelocity, transform.up);

        // このフレームで曲がれる速度を計算
        float maxRadiansDelta = Time.deltaTime * navMeshAgent.angularSpeed * Mathf.Deg2Rad;

        if (direction != Vector3.zero)
        {
            // 車両が進んでいるベクトルが0ではない（＝止まっていない）場合、
            // 各タイヤの向きを、進行方向に向ける。
            foreach(Transform tire in frontTires)
            {
                tire.forward = Vector3.RotateTowards(tire.forward, direction, maxRadiansDelta, 0);
            }
        }
    }
}
}
