using Oculus.Interaction.HandGrab;
using UnityEngine;

public class BedInteraction : MonoBehaviour
{
    BlinkMaterial blinkMaterial;

    private void Start()
    {
        blinkMaterial = GetComponent<BlinkMaterial>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Plushy plushy))
        {
            blinkMaterial.StopBlink();
            plushy.GetComponent<HandGrabInteractable>().enabled = false;
            plushy.CompleteTask();
        }
    }
}