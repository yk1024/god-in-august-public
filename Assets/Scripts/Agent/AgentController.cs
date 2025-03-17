using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using GodInAugust.System;

namespace GodInAugust.Agent
{
public class AgentController : MonoBehaviour
{
    [SerializeField]
    private Transform[] navigationPoints;

    [SerializeField, Min(0)]
    private float stopDuration;

    [SerializeField]
    private bool selectRandomly;

    protected Animator animator;
    protected NavMeshAgent navMeshAgent;
    private int navigationIndex = 0;
    private bool isStopping = false;

    protected float speed;

    protected virtual void Start()
    {
        animator = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    protected virtual void Update()
    {
        if (!isStopping && navigationPoints.Length != 0)
        {
            StartCoroutine(SetDestination());
        }

        speed = navMeshAgent.velocity.magnitude;
        animator.SetFloat(Constants.SpeedAnimatorParameter, speed);
    }

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
