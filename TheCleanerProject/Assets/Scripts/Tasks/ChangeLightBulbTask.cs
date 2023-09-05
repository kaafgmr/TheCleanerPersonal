using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeLightBulbTask : TaskInfo //script que va en el padre de la bombilla rota
{
    public override void internalStart() { base.internalStart(); }
    public override void UpdateTask() { }
    public bool CheckIfItsDone() { return taskFinished; }
}
