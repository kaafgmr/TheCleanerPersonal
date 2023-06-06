using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Brogos : Ghost
{
    [Header("IA")]
    public List<Transform> possibleTpPoints;
    public Vector3 recentTp;
    [SerializeField] float distanceToUpdate = 0.5f;
    NavMeshAgent agent;
    FieldOfView FOV;
    bool MovingTowardsPlayer = false;
    public bool timeToHide;
    int recentTpnum = 0;
    Coroutine _coroutine = null;

    [Header("Anims")]
    GhostAnimController gac;
    [SerializeField] private string walkAnimName;
    [SerializeField] private string idleAnimName;
    [SerializeField] private string screamerAnimName;
    bool ResumeMovementOnce = false;
    bool StopMovementOnce = false;
    

    private void Start()
    {
        Init();
        CalculateBounds.instance.OnInit.AddListener(ResumeMovement);
    }

    private void Init()
    {
        agent = GetComponent<NavMeshAgent>();
        resetRandPos();
        FOV = GetComponentInChildren<FieldOfView>();
        FOV.OnViewedByMe.AddListener(StartChanneling);
        FOV.ImBeingViewed.AddListener(GhostCounter);
        FOV.OnNothingHappening.AddListener(StartInspection);
        gac = GetComponent<GhostAnimController>();
    }

    private void Update()
    {
        if (screaming)
        
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
            if (_coroutine == null)
            {
                _coroutine = StartCoroutine(Inspection());
            }
        }
    }

    public void StartInspection()
    {
        if (_coroutine == null)
            _coroutine = StartCoroutine(Inspection());
    }

    public override void GhostCounter()
    {
        StopCoroutine(Stalk(Vector3.zero));
        MoveToRecentTp();
    }

    public override void Scream()
    {
        base.Scream();
        agent.enabled = false;
        gac.PlayAnimation(screamerAnimName);
    }

    public void StartChanneling(Vector3 playerPos)
    {
        StartCoroutine(Stalk(playerPos));
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
        ResumeMovement();
        agent.destination = playerPos;
        agent.isStopped = false;
    }
    public void MoveToRandomPoint()
    {
        if (screaming)
            return;

        ResumeMovement();
        if (agent.remainingDistance > distanceToUpdate) return;

        Vector3 pointInsideBounds = CalculateBounds.CalculatePointInsideBounds(CalculateBounds.bounds);
        NavMesh.SamplePosition(pointInsideBounds, out NavMeshHit hit, 100, NavMesh.AllAreas);

        agent.destination = hit.position;
    }

    void MoveToRecentTp()
    {
        agent.destination = recentTp;
        timeToHide = true;
    }

    public void SetValueTimeToHide(bool value)
    {
        timeToHide = value;
    }

    public bool CheckIfItsTimeToHide()
    {
        return timeToHide;
    }

    void UpdateVision()
    {
        if (agent.remainingDistance > distanceToUpdate) return;
        MovingTowardsPlayer = false;
    }

    public void resetRandPos()
    {
        int num = Random.Range(0, possibleTpPoints.Count-1);
        if (recentTpnum == num)
        {
            resetRandPos();
        }
        else 
        {
            agent.Warp(possibleTpPoints[num].position);
            SetValueTimeToHide(false);
            recentTp = possibleTpPoints[num].position;
            recentTpnum = num;
        }
    }

    IEnumerator Stalk(Vector3 PlayerPos)
    {
        if (timeToHide) { StopCoroutine(Stalk(PlayerPos)); }
        StopMovement();
        yield return new WaitForSeconds(2);
        ResumeMovement();
        MoveToPlayer(PlayerPos);
    }
    
    IEnumerator Inspection()
    {
        Debug.Log("entro");
        MoveToRandomPoint();
        yield return new WaitForSeconds(4);
        Debug.Log("entro despues de los 4");
        MoveToRecentTp();
        _coroutine = null;
    }    
}
