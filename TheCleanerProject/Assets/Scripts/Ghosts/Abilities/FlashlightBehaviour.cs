using UnityEngine;

public class FlashlightBehaviour : MonoBehaviour
{
    public bool isBeingHeld;
    [SerializeField] private Transform attachPoint;
    
    Light spotLight;

    private void Start()
    {
        spotLight = GetComponentInChildren<Light>();
    }

    public Transform GetAttachPoint()
    {
        return attachPoint;
    }

    public float GetFOV()
    {
        return spotLight.spotAngle;
    }

    public void SetIsBeingHeld(bool value)
    {
        isBeingHeld = value;
    }
}