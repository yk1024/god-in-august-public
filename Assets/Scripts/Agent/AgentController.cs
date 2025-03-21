using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using GodInAugust.System;

namespace GodInAugust.Agent
{
/// <summary>
/// 乗り物などの動くものを操作するためのコンポーネント
/// </summary>
[AddComponentMenu("God In August/Agent/Agent Controller")]
public class AgentController : MonoBehaviour
{
    [SerializeField, Tooltip("徘徊ポイント")]
    private Transform[] navigationPoints;

    [SerializeField, Min(0), Tooltip("ポイント到達時に待機する時間（秒）")]
    private float stopDuration;

    [SerializeField, Tooltip("徘徊ポイントからランダムで次のポイントを選択する")]
    private bool selectRandomly;

    /// <summary>
    /// 同じオブジェクトに付与されているアニメーター
    /// Start時に取得される。
    /// </summary>
    protected Animator animator;

    /// <summary>
    /// 同じオブジェクトに付与されているナビメッシュエージェント
    /// Start時に取得される。
    /// </summary>
    protected NavMeshAgent navMeshAgent;

    // 現在何番目の徘徊ポイントを目指しているか
    private int navigationIndex = 0;

    // 現在徘徊ポイントに到達して待機しているかどうか。
    private bool isStopping = false;

    /// <summary>
    /// 現在のナビメッシュエージェントの速度
    /// </summary>
    protected float speed;

    // 速度のアニメーションパラメータ名
    private const string SpeedAnimatorParameter = "Speed";

    protected virtual void Start()
    {
        animator = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    protected virtual void Update()
    {
        // 待機中であるか、徘徊ポイントが0個の時は処理を飛ばす。
        if (!isStopping && navigationPoints.Length != 0)
        {
            StartCoroutine(SetDestination());
        }

        speed = navMeshAgent.velocity.magnitude;
        animator.SetFloat(SpeedAnimatorParameter, speed);
    }

    // 次のポイントを決めるメソッド
    // 待機する場合にはWaitForSecondsをyield returnする。
    private IEnumerator SetDestination()
    {
        if (!navMeshAgent.pathPending && navMeshAgent.remainingDistance < 0.5f)
        {
            if (stopDuration != 0)
            {
                isStopping = true;
                yield return new WaitForSeconds(stopDuration);
                isStopping = false;
            }

            if (selectRandomly)
            {
                navigationIndex = Random.Range(0, navigationPoints.Length);
            }
            else
            {
                navigationIndex = navigationIndex < navigationPoints.Length - 1 ? navigationIndex + 1 : 0;
            }

            Transform nextPoint = navigationPoints[navigationIndex];
            navMeshAgent.SetDestination(nextPoint.position);
        }
    }
}
}
