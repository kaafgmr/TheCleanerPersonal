using UnityEngine;

public class DebugColor : MonoBehaviour
{
    private Material material;

    private void Start()
    {
        material = GetComponent<MeshRenderer>().material;
    }

    private void OnTriggerEnter(Collider other)
    {
        material.color = Color.green;
    }

    private void OnTriggerExit(Collider other)
    {
        material.color = Color.white;
    }
}