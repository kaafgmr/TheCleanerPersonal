using System.Collections.Generic;

public class VacuumTask : TaskInfo
{
    public List<DustInteraction> listOfDusts;
    int numberOfDustsVacuumed;

    public override void internalStart()
    {
        base.internalStart();
        numberOfDustsVacuumed = 0; 
    }

    public override void UpdateTask()
    {
        numberOfDustsVacuumed = 0;
        for (int i = 0; i < listOfDusts.Count; i++)
        {
            if (listOfDusts[i].hasBeenVacuumed == true) { numberOfDustsVacuumed++; }
        }
        if (numberOfDustsVacuumed == listOfDusts.Count)
        {
            taskFinished = true;
        }
    }

    public bool CheckIfItsDone()
    {
        return taskFinished;
    }
    private void Update()
    {
        if (CheckIfItsDone()) { base.FinishTask(); }
        else { UpdateTask(); }        
    }
}
