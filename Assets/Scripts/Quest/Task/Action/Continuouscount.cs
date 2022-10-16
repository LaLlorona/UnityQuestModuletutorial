using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Quest/Task/Action/ContinuousCount", fileName = "Continuous Count")]
public class Continuouscount : TaskAction
{
    public override int Run(Task task, int currentSuccess, int successCount)
    {
        Debug.Log($"in continuous count, currentSuccess is {currentSuccess} and successCount is {successCount}");
        return successCount > 0 ? currentSuccess + successCount : 0;
    }
}
