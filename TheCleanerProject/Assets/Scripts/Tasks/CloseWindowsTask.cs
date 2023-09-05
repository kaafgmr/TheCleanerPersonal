using System.Collections.Generic;

public class CloseWindowsTask : TaskInfo
{
    public static CloseWindowsTask instance;
    public List<CloseWindowInteraction> windowsToClose;
    int closedWindows;

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
    }

    public override void internalStart()
    {
        base.internalStart();

        closedWindows = 0;
    }

    public override void UpdateTask()
    {
        closedWindows++;

        if (closedWindows >= windowsToClose.Count)
        {
            base.FinishTask();
            taskFinished = true;
        }
    }
}
