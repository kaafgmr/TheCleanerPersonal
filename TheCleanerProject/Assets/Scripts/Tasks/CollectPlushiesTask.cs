using System.Collections.Generic;

public class CollectPlushiesTask : TaskInfo
{
    public static CollectPlushiesTask instance;

    public List<Plushy> listOfPlushies;

    int plushiesInRightPlace;

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
        plushiesInRightPlace = 0;
    }

    public override void internalStart()
    {
        base.internalStart();
    }

    public override void UpdateTask()
    {
        plushiesInRightPlace++;

        if (plushiesInRightPlace >= listOfPlushies.Count)
        {
            base.FinishTask();
        }
    }
}