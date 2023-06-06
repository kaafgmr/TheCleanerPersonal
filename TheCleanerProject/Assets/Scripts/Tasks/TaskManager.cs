using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TaskManager : MonoBehaviour
{
    public static TaskManager instance;
    
    public List<Task> tasksList;
    public UnityEvent OnAllTasksCompleted;
    int numberTasksDone;
    
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }

        numberTasksDone = 0;
    }

    public void UpdateTasksCounter()
    {
        numberTasksDone++;

        if (numberTasksDone >= tasksList.Count)
        {
            OnAllTasksCompleted.Invoke();
        }
    }
}
