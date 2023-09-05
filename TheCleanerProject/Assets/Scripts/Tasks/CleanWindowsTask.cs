using System.Collections.Generic;

public class CleanWindowsTask : TaskInfo
{
    public static CleanWindowsTask instance;

    public List<WindowInteraction> windowsToClean;
    int windowsCleaned;

    public override void InternalAwake()
    {
        base.InternalAwake();
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
        windowsCleaned = 0;
    }

    public override void internalStart()
    {
        base.internalStart();
    }

    public override void UpdateTask()
    {
        windowsCleaned++;

        if (windowsCleaned >= windowsToClean.Count)
        {
            base.FinishTask();
            taskFinished = true;
        }
    }
}
