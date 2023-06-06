using UnityEngine;

public class Plushy : MonoBehaviour
{
    void Start()
    {
        CollectPlushiesTask.instance.listOfPlushies.Add(this);
    }
    
    public void CompleteTask()
    {
        CollectPlushiesTask.instance.UpdateTask();
    }
}