using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BodySocketInventory : MonoBehaviour
{
    public Transform HMD;
    [Range(0.01f, 1f)]
    public float heightRatio = 0.6f;

    void Update()
    {
        UpdateBeltTransform();
    }

    private void UpdateBeltTransform()
    {
        transform.localPosition = new Vector3(transform.localPosition.x, HMD.localPosition.y * heightRatio, transform.localPosition.z);
        transform.localRotation = new Quaternion(transform.localRotation.x, HMD.localRotation.y, transform.localRotation.z, transform.localRotation.w);
    }
}