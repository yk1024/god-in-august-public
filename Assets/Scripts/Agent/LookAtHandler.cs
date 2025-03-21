using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using GodInAugust.Level;

namespace GodInAugust.Agent
{
/// <summary>
/// 視対象との位置関係に応じて、そちらの方を向くように首を動かすためのコンポーネント
/// </summary>
[AddComponentMenu("God In August/Agent/Look At Handler")]
public class LookAtHandler : MonoBehaviour
{
    [SerializeField, Tooltip("視野角")]
    private float viewingAngle;

    [SerializeField, Tooltip("首を動かす速度（ラジアン / 秒）")]
    private float rotationSpeed;

    [SerializeField, Tooltip("反応速度（単位 / 秒）")]
    private float reactionSpeed;

    [SerializeField, Tooltip("視対象のトランスフォーム")]
    private Transform lookAtPosition;

    // 視対象の候補を保管しておく辞書
    // キーはゲームオブジェクトで、値はそのオブジェクトに付されたILookAtTarget
    private Dictionary<GameObject, ILookAtTarget> targets = new Dictionary<GameObject, ILookAtTarget>();

    // 頭のTransform
    private Transform head;

    // 同じオブジェクトに付されたアニメーター
    private Animator animator;

    // IKで使うための重み
    // 0の時はIKが働かず、1の時にIKに最も従う。
    private float ikLookAtWeight = 0;

    [SerializeField, Tooltip("一時的に見るのをやめる。（アニメーション用）")]
    private bool suspended = false;

    private void Start()
    {
        animator = GetComponent<Animator>();
        head = animator.GetBoneTransform(HumanBodyBones.Head);
    }

    private void OnTriggerEnter(Collider other)
    {
        // ILookAtTargetを持つトリガーと接触した時、targetsに追加する。
        if (!other.isTrigger && other.TryGetComponent(out ILookAtTarget target))
        {
            targets.TryAdd(other.gameObject, target);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // targetsにあるトリガーと接触した場合、targetsから取り除く。
        if (!other.isTrigger)
        {
            targets.Remove(other.gameObject);
        }
    }

    /// <summary>
    /// 視対象の候補のうち、見える一番近いものを検索し、そちらの方を向く。
    /// </summary>
    /// <returns>
    /// 対象となったオブジェクトに付された<c>ILookAtTarget</c>と、その対象までの距離の2要素からなるタプル
    /// 見つからなかった場合は<c>(null, 0)</c>と
    /// </returns>
    public (ILookAtTarget target, float distance) LookAtFirstTarget()
    {
        float distance = 0;

        (GameObject _, ILookAtTarget target) =
            targets
            .OrderBy((target) => Vector3.Distance(head.position, target.Value.TargetPoint.position))
            .FirstOrDefault((target) =>
            {
                // 動作主の頭部から対象への方向を表すベクトル
                Vector3 direction = target.Value.TargetPoint.position - head.position;

                // 動作主の前方向に対する、対象の方向の角度
                float angle = Vector3.Angle(transform.forward, direction);

                // 対象の方向の角度が、視野角の外ならば、見えない。
                if (angle > viewingAngle) return false;

                // 動作主の頭から、対象に向けてRaycastを行う。
                if (!Physics.Raycast(head.position, direction, out RaycastHit hit, direction.magnitude)) return false;

                // 動作主の頭と対象の間に対象以外の物体がある場合は、見えない。
                if (hit.collider.gameObject != target.Key) return false;

                distance = hit.distance;
                return true;
            });

        PrepareAnimation(target);

        return (target, distance);
    }

    // OnAnimatorIKで利用するための値を用意するメソッド。
    private void PrepareAnimation(ILookAtTarget target)
    {
        // IKの影響度をこのフレームでどれぐらい増減するか
        float ikWeightReaction = reactionSpeed * Time.deltaTime;

        if (!suspended && target != null)
        {
            // 対象が存在して、保留中ではない場合、次にみる位置を計算し、IKの影響度を増やす。
            CalculateNextPosition(target.TargetPoint.position);
            ikLookAtWeight += ikWeightReaction;
        }
        else
        {
            // 対象が存在しないか、保留中の場合はIKの影響度を減らす。
            ikLookAtWeight -= ikWeightReaction;
        }

        ikLookAtWeight = Mathf.Clamp01(ikLookAtWeight);
    }

    private void CalculateNextPosition(Vector3 targetPosition)
    {
        // ikLookAtWeightが0の場合（＝何も見ていない場合）や、対象の位置が現在の見ている位置と同じ場合は、処理を飛ばす。
        if (ikLookAtWeight == 0 || lookAtPosition.position == targetPosition) return;

        // 頭の位置から対象までの方向
        Vector3 targetDirection = targetPosition - head.position;

        // 頭の位置から、現在見ている場所までの方向
        Vector3 currentDirection = lookAtPosition.position - head.position;

        // 次に見る場所は、現在見ている方向から、新しい対象までの方向に回転させたもの
        // 首を動かす角速度を超えては動かさないようにする。
        Vector3 nextDirection = Vector3.RotateTowards(currentDirection, targetDirection, rotationSpeed * Time.deltaTime, 0);

        // 現在の頭の位置から、上記で回転させた新しい方向に移動させた位置が、新しい対象の場所である。
        lookAtPosition.position = nextDirection + head.position;
    }

    private void OnAnimatorIK(int layerIndex)
    {
        animator.SetLookAtPosition(lookAtPosition.position);
        animator.SetLookAtWeight(ikLookAtWeight);
    }
}
}
