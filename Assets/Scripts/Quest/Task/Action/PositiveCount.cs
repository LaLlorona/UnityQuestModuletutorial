using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Quest/Task/Action/PositiveCount", fileName = "Positive Count")]
public class PositiveCount : TaskAction
{
    public override int Run(Task task, int currentSuccess, int successCount)
    {
        Debug.Log($"In success count, current success is {currentSuccess}, and successCount is {successCount}");
        if (successCount > 0)
        {
            return currentSuccess + successCount;
        }
        else
        {
            return currentSuccess;
        }
       
    }
}
