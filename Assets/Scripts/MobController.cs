using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class MobController : MonoBehaviour
{
    [SerializeField]
    private Transform[] navigationPoints;

    [SerializeField, Min(0)]
    private float stopDuration;

    [SerializeField]
    private bool selectRandomly;

    private Animator animator;
    private NavMeshAgent navMeshAgent;
    private int navigationIndex = 0;
    private bool isStopping = false;
    private LookAtHandler lookAtHandler;

    void Start()
    {
        animator = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        lookAtHandler = GetComponent<LookAtHandler>();

        animator.SetFloat(Constants.MotionSpeedAnimatorParameter, 1);
    }

    void Update()
    {
        if (!isStopping && navigationPoints.Length != 0)
        {
            StartCoroutine(SetDestination());
        }

        float speed = navMeshAgent.velocity.magnitude;
        animator.SetFloat(Constants.SpeedAnimatorParameter, speed);

        lookAtHandler.LookAtFirstTarget();
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

    public void OnFootstep(AnimationEvent animationEvent)
    {

    }

    public void OnLand(AnimationEvent animationEvent)
    {

    }
}
