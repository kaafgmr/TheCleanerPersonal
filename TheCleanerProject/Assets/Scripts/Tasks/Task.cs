using UnityEngine;
using UnityEngine.Events;

public abstract class Task : MonoBehaviour
{
    public enum TaskType { waterTask, electricityTask, windTask, cleanTask }
    float taskDuration;
    public bool taskFinished;
    public UnityEvent onTaskFinished;
    public abstract void UpdateTask();
    public virtual void DoTask() { }

    public virtual void FinishTask()
    {
        onTaskFinished.Invoke();
        TaskManager.instance.UpdateTasksCounter();
    }

    private void Awake()
    {
        InternalAwake();
    }

    private void Start()
    {
        internalStart();
    }

    public virtual void InternalAwake()
    {

    }

    public virtual void internalStart()
    {
        TaskManager.instance.tasksList.Add(this);
    }
}
