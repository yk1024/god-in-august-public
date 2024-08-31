using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MobController : MonoBehaviour
{
    [SerializeField]
    private Transform[] navigationPoints;

    private Animator animator;
    private NavMeshAgent navMeshAgent;
    private int navigationIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();

        animator.SetFloat("MotionSpeed", 1);
    }

    // Update is called once per frame
    void Update()
    {
        if (navigationPoints.Length != 0)
        {
            SetDestination();
        }

        float speed = navMeshAgent.velocity.magnitude;
        animator.SetFloat("Speed", speed);
    }

    private void SetDestination()
    {
        if (!navMeshAgent.pathPending && navMeshAgent.remainingDistance < 0.5f)
        {
            Transform nextPoint = navigationPoints[navigationIndex];
            navMeshAgent.SetDestination(nextPoint.position);
            navigationIndex = navigationIndex < navigationPoints.Length - 1 ? navigationIndex + 1 : 0;
        }
    }

    public void OnFootstep(AnimationEvent animationEvent)
    {

    }

    public void OnLand(AnimationEvent animationEvent)
    {

    }
}
