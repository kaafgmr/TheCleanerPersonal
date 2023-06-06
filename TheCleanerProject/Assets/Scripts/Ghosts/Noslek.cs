using UnityEngine;
using UnityEngine.AI;

public class Noslek : Ghost
{
    [SerializeField] float distanceToUpdate = 0.5f;
    [SerializeField] private string walkAnimName;
    [SerializeField] private string idleAnimName;
    [SerializeField] private string screamerAnimName;


    NavMeshAgent agent;
    FieldOfView FOV;
    GhostAnimController gac;

    bool MovingTowardsPlayer = false;
    bool ResumeMovementOnce = false;
    bool StopMovementOnce = false;

    private void Start()
    {
        Init();
        CalculateBounds.instance.OnInit.AddListener(ResumeMovement);
    }

    private void Init()
    {
        gac = GetComponent<GhostAnimController>();
        agent = GetComponent<NavMeshAgent>();
        StopMovement();
        FOV = GetComponentInChildren<FieldOfView>();
        FOV.OnViewedByMe.AddListener(MoveToPlayer);
        FOV.ImBeingViewed.AddListener(GhostCounter);
        FOV.OnNothingHappening.AddListener(MoveToRandomPoint);
    }

    private void Update()
    {
        if (screaming) return;
        if (agent != null && agent.isStopped) return;

        GhostAction(Vector3.zero);
    }

    public override void GhostAction(Vector3 otherPos)
    {
        if (MovingTowardsPlayer)
        {
            UpdateVision();
        }
        else
        {
            MoveToRandomPoint();
        }
    }

    public override void GhostCounter()
    {
        if (!screaming)
        StopMovement();
    }

    public override void Scream()
    {
        base.Scream();
        agent.enabled = false;
        gac.PlayAnimation(screamerAnimName);
    }

    public void StopMovement()
    {
        if (!agent.isStopped)
        {
            agent.isStopped = true;

            if (!StopMovementOnce)
            {
                StopMovementOnce = true;
                ResumeMovementOnce = false;
                gac.PlayAnimation(idleAnimName);
            }
        }
    }

    void ResumeMovement()
    {
        if (agent.isStopped && !screaming)
        {
            agent.isStopped = false;
            
            if (!ResumeMovementOnce)
            {
                StopMovementOnce = false;
                ResumeMovementOnce = true;
                gac.PlayAnimation(walkAnimName);
            }
        }
    }

    public void MoveToPlayer(Vector3 playerPos)
    {
        if (screaming) return;
        ResumeMovement();
        agent.destination = playerPos;
        MovingTowardsPlayer = true;
        agent.isStopped = false;
    }
    public void MoveToRandomPoint()
    {
        if (screaming) return;
        ResumeMovement();
        if (agent.remainingDistance > distanceToUpdate) return;

        Vector3 pointInsideBounds = CalculateBounds.CalculatePointInsideBounds(CalculateBounds.bounds);
        NavMesh.SamplePosition(pointInsideBounds, out NavMeshHit hit, 100, NavMesh.AllAreas);
        
        agent.destination = hit.position;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        if(agent != null)
        {
            Gizmos.DrawWireSphere(agent.destination, 0.5f);
        }
    }

    void UpdateVision()
    {
        if (agent.remainingDistance > distanceToUpdate) return;
        MovingTowardsPlayer = false;
    }
}