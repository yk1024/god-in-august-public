using UnityEngine;
using GodInAugust.Level;

namespace GodInAugust.Agent
{
/// <summary>
/// 足音を鳴らすためのコンポーネント
/// </summary>
[AddComponentMenu("God In August/Agent/Footstep Manager")]
public class FootstepManager : MonoBehaviour
{
    [SerializeField, Tooltip("足音のWwiseイベント")]
    private AK.Wwise.Event footstepEvent;

    [SerializeField, Tooltip("デフォルトの地面のWwiseスイッチ")]
    private AK.Wwise.Switch defaultGroundSwitch;

    /// <summary>
    /// 現在の足下の種類を表すスイッチ
    /// </summary>
    public AK.Wwise.Switch GroundSwitch { get; set; }

    // 足音を再生するメソッド
    private void TriggerFootstepSound()
    {
        GroundSwitch = GetGroundSwitch();
        GroundSwitch.SetValue(gameObject);

        footstepEvent.Post(gameObject);
    }

    // 現在の足下の状態を取得するメソッド
    private AK.Wwise.Switch GetGroundSwitch()
    {
        // 自分の下側にある最初のGroundSurfaceを検索する。
        foreach (RaycastHit hit in Physics.RaycastAll(transform.position, -transform.up, 0.1f))
        {
            if (hit.collider.TryGetComponent(out GroundSurface groundSurface))
            {
                return groundSurface.GroundSwitch;
            }
        }

        // 取得できなかった場合はデフォルトのものを使用する。
        return defaultGroundSwitch;
    }

    /// <summary>
    /// 足音を再生する。
    /// アニメーションイベントによって利用される。
    /// </summary>
    /// <param name="animationEvent">呼び出し元のアニメーションイベント</param>
    public void OnFootstep(AnimationEvent animationEvent)
    {
        if (animationEvent.animatorClipInfo.weight > 0.5f)
        {
            TriggerFootstepSound();
        }
    }
}
}
