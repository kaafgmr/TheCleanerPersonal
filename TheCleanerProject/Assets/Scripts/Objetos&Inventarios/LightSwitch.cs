using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSwitch : MonoBehaviour
{
    [SerializeField] Light lightToSwitch;

    Quaternion offRotation;
    Quaternion onRotation;

    private void Start()
    {
        onRotation = transform.parent.rotation;
        offRotation = Quaternion.Euler(onRotation.eulerAngles.x, onRotation.eulerAngles.y, 180);
    }
    private void OnTriggerEnter(Collider other)
    {
        lightToSwitch.enabled = !lightToSwitch.enabled;

        if (lightToSwitch.enabled)
        {
            transform.parent.rotation = onRotation;
        }
        else
        {
            transform.parent.rotation = offRotation;
        }
    }
}