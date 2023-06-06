using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class SukSuk : Ghost
{
    [SerializeField] private float speed = 3.5f;
    [SerializeField] private Transform restingPos;

    FlashlightBehaviour flashLight;
    NavMeshAgent agent;
    FieldOfView FOV;
    float flashlightFOVAngle;



    public TextMeshProUGUI debugText;



    private void Start()
    {

        debugText = GameObject.Find("DebugText").GetComponent<TextMeshProUGUI>();
        debugText.text = "";



        Init();

        CalculateBounds.instance.OnInit.AddListener(ResumeMovement);
    }

    private void Init()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = speed;
        StopMovement();
        FOV = GetComponentInChildren<FieldOfView>();
        FOV.OnNothingHappening.AddListener(ReturnToSpawn);
        FOV.OnViewedByMe.AddListener(GhostAction);
        flashLight = GameManager.instance.flashlight;
        flashlightFOVAngle = flashLight.GetFOV();
    }

    public override void GhostAction(Vector3 otherPos)
    {
        debugText.text ="inside: " + FOV.IsInsideTheFOVOf(flashLight.GetAttachPoint(), flashlightFOVAngle, transform).ToString();
        if (flashLight.isBeingHeld && FOV.IsInsideTheFOVOf(flashLight.GetAttachPoint(), flashlightFOVAngle, transform))
        {
            ResumeMovement();
            agent.destination = otherPos;
        }
    }

    public override void GhostCounter()
    {

    }

    public override void Scream()
    {
        throw new System.NotImplementedException();
    }
    
    void ReturnToSpawn()
    {
        if (agent.isStopped)
        {
            ResumeMovement();
        }
        agent.destination = restingPos.position;
    }

    void ResumeMovement()
    {
        if (agent.isStopped)
        {
            agent.isStopped = false;
        }
    }

    public void StopMovement()
    {
        if (!agent.isStopped)
        {
            agent.isStopped = true;
        } 
    }
}