using System.Collections.Generic;
using UnityEngine;
using GodInAugust.Agent;

namespace GodInAugust.System
{
/// <summary>
/// BGMを制御するコンポーネント
/// </summary>
[AddComponentMenu("God In August/System/Music Manager")]
public class MusicManager : SingletonBehaviour<MusicManager>
{
    [SerializeField, Header("Event"), Tooltip("BGM再生のWwiseイベント")]
    private AK.Wwise.Event playBGMEvent;

    [SerializeField, Header("Area State"), Tooltip("デフォルトのエリアのステート")]
    private AK.Wwise.State defaultAreaState;

    /// <summary>
    /// 現在のエリアの状態
    /// </summary>
    public AK.Wwise.State AreaState { get; set; }

    // 現在いるエリアのリスト
    // エリアの境界部では重複する可能性があるので、リストで保持する。
    private readonly List<AK.Wwise.State> areaStates = new List<AK.Wwise.State>();

    [SerializeField, Header("Proximity To Anomaly RTPC"), Tooltip("異変からの近さのRTPC")]
    private AK.Wwise.RTPC proximityToAnomalyRTPC;

    /// <summary>
    /// 現在の異変までの近さ
    /// </summary>
    public float ProximityToAnomaly { get; set; }

    [SerializeField, Tooltip("異変の向きのX軸のRTPC")]
    private AK.Wwise.RTPC directionToAnomalyXRTPC;

    [SerializeField, Tooltip("異変の向きのY軸のRTPC")]
    private AK.Wwise.RTPC directionToAnomalyYRTPC;

    [SerializeField, Tooltip("異変の向きのZ軸のRTPC")]
    private AK.Wwise.RTPC directionToAnomalyZRTPC;

    [SerializeField, Header("Move Speed Switch"), Tooltip("停止のWwiseスイッチ")]
    private AK.Wwise.Switch stopSwitch;

    [SerializeField, Tooltip("歩行のWwiseスイッチ")]
    private AK.Wwise.Switch walkSwitch;

    [SerializeField, Tooltip("走行のWwiseスイッチ")]
    private AK.Wwise.Switch sprintSwitch;

    /// <summary>
    /// 現在の移動速度を表すスイッチ
    /// </summary>
    public AK.Wwise.Switch MoveSpeedSwitch { get; set; }

    private void Start()
    {
        defaultAreaState.SetValue();
        playBGMEvent.Post(gameObject);
    }

    /// <summary>
    /// エリアを追加する。新しいエリアに入った時に実行する。
    /// </summary>
    /// <param name="areaState">新しいエリアのWwiseステート</param>
    public void AddAreaState(AK.Wwise.State areaState)
    {
        areaStates.Add(areaState);
        SetAreaState();
    }

    /// <summary>
    /// エリアを削除する。エリアを離れた時に実行する。
    /// </summary>
    /// <param name="areaState">離れたエリアのWwiseステート</param>
    public void RemoveAreaState(AK.Wwise.State areaState)
    {
        areaStates.Remove(areaState);
        SetAreaState();
    }

    // エリアを更新する。
    private void SetAreaState()
    {
        if (areaStates.Count == 0)
        {
            // エリアのリストが空の場合はデフォルトを選ぶ。
            AreaState = defaultAreaState;
        }
        else
        {
            // エリアのリストに要素がある場合は、最初のものを選ぶ。
            AreaState = areaStates[0];
        }

        AreaState.SetValue();
    }

    /// <summary>
    /// 異変への近さを設定する。
    /// </summary>
    /// <param name="proximity">異変への近さ。異変の中にいる時は1、異変から離れている時は0。</param>
    /// <param name="anomalyPosition">異変の位置</param>
    public void SetProximityToAnomaly(float proximity, Vector3 anomalyPosition)
    {
        ProximityToAnomaly = proximity;

        // カメラから見た異変の相対的な方向を計算
        Camera mainCamera = Camera.main;
        Vector3 direction = mainCamera.transform.InverseTransformDirection(anomalyPosition - mainCamera.transform.position);

        // 異変への近さに対応するように、ベクトルの大きさを変更
        direction = direction.normalized * (1 - proximity);

        proximityToAnomalyRTPC.SetValue(gameObject, proximity);
        directionToAnomalyXRTPC.SetValue(gameObject, direction.x);
        directionToAnomalyYRTPC.SetValue(gameObject, direction.y);
        directionToAnomalyZRTPC.SetValue(gameObject, direction.z);
    }

    /// <summary>
    /// 移動速度を設定する。
    /// </summary>
    /// <param name="moveSpeed">移動速度</param>
    public void SetMoveSpeed(MoveSpeed moveSpeed)
    {
        MoveSpeedSwitch =
            moveSpeed switch
            {
                MoveSpeed.Walk => walkSwitch,
                MoveSpeed.Sprint => sprintSwitch,
                _ => stopSwitch
            };

        MoveSpeedSwitch.SetValue(gameObject);
    }
}
}
