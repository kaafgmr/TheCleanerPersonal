using System.Collections.Generic;

public class TidyUpRoomTask : TaskInfo
{
    public List<ObjectsToOrder> objesctsList;

    public static TidyUpRoomTask instance;

    int objectsInPlace;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
        objectsInPlace = 0;
    }

    public override void internalStart()
    {
        base.internalStart();
    }
    public override void UpdateTask()
    {
        objectsInPlace++;

        if(objectsInPlace >= objesctsList.Count)
        {
            base.FinishTask();
        }
    }
    public bool CheckIfItsDone()
    {
        return taskFinished;
    }
}