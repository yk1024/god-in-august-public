using UnityEngine;
using UnityEngine.AI;

public class MobController : MonoBehaviour
{
    [SerializeField]
    private Transform[] navigationPoints;

    private Animator animator;
    private NavMeshAgent navMeshAgent;
    private int navigationIndex = 0;
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
        if (navigationPoints.Length != 0)
        {
            SetDestination();
        }

        float speed = navMeshAgent.velocity.magnitude;
        animator.SetFloat(Constants.SpeedAnimatorParameter, speed);

        lookAtHandler.LookAtFirstTarget();
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
