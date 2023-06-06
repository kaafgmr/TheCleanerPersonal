using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class DoorHandle : MonoBehaviour
{
    [SerializeField] float maxDistance = 0.5f;
    
    XRGrabInteractable grabbedObj;

    private void Start()
    {
        grabbedObj = GetComponentInParent<XRGrabInteractable>();

        grabbedObj.selectEntered.AddListener(CheckDistance);
        grabbedObj.selectExited.AddListener(StopChecking);
    }

    private void CheckDistance(SelectEnterEventArgs arg0)
    {
        Transform whoInteracted = arg0.interactorObject.transform;
        StartCoroutine(DistanceChecker(whoInteracted));
    }

    IEnumerator DistanceChecker(Transform withWhom)
    {
        while (Vector3.Distance(transform.position, withWhom.position) < maxDistance)
        {
            yield return new WaitForSeconds(Time.fixedDeltaTime);
        }

        grabbedObj.trackPosition = false;
        grabbedObj.trackRotation = false;
    }

    private void StopChecking(SelectExitEventArgs arg0)
    {
        StopCoroutine(DistanceChecker(null));

        grabbedObj.trackPosition = true;
        grabbedObj.trackRotation = true;
    }
}