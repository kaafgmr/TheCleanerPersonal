using System.Collections;
using UnityEngine;

public class PointingMovement : MonoBehaviour
{
    [SerializeField] float baseSpeed;
    [SerializeField] float runningSpeedIncrease;
    [SerializeField] Transform trackedHand;
    [SerializeField] float yOffset;
    
    float _runningSpeed;
    bool ensureStartStop;
    Transform dirTransform;
    MovementBehaviour MB;

    private void Start()
    {
        MB = GetComponent<MovementBehaviour>();
        dirTransform = trackedHand;
    }

    public void StartedPointing()
    {
        ensureStartStop = true;
        StartCoroutine(UpdateMovement());
    }

    public void StoppedPointing()
    {
        ensureStartStop = false;
        StopCoroutine(UpdateMovement());
    }

    public void StartDoblePointing()
    {
        _runningSpeed = runningSpeedIncrease;
    }

    public void StopDoublePointing()
    {
        _runningSpeed = 0;
    }

    public Vector3 GetDirection()
    {
        dirTransform.rotation = Quaternion.Euler(0, trackedHand.rotation.eulerAngles.y + yOffset, 0);
        return dirTransform.forward;
    }

    IEnumerator UpdateMovement()
    {
        while (ensureStartStop)
        {
            MB.MoveRB3D(baseSpeed + _runningSpeed, -GetDirection());
            yield return new WaitForSeconds(Time.fixedDeltaTime);
        }
    }
}