using UnityEngine;
using Oculus.Interaction.HandGrab;

public class ObjectsToOrder : MonoBehaviour
{
    void Start()
    {
        TidyUpRoomTask.instance.objesctsList.Add(this);
    }

    public void SetIsOnRealPosition()
    {
        GetComponent<HandGrabInteractable>().enabled = false;
        GetComponent<Rigidbody>().isKinematic = true;
        TidyUpRoomTask.instance.UpdateTask();
    }
}