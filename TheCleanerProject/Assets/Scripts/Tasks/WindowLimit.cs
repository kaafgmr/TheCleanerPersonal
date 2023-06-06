using Oculus.Interaction.HandGrab;
using UnityEngine;

public class WindowLimit : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out CloseWindowInteraction cwi))
        {
            cwi.FinishTask();
            cwi.GetComponent<HandGrabInteractable>().enabled = false;
        }
    }
}