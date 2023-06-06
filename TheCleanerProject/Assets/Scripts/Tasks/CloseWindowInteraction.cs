using UnityEngine;

public class CloseWindowInteraction : MonoBehaviour
{
    private void Start()
    {
        CloseWindowsTask.instance.windowsToClose.Add(this);
    }

    public void FinishTask()
    {
        CloseWindowsTask.instance.UpdateTask();
    }
}