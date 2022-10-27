using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndCheckpoint : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Controller>() == null)
            return;

        QuestSystem.Instance.CompleteWaitingQuests();
        QuestSystem.Instance.Save();

        GameSystem.Instance.StopTimer();
        GameSystem.Instance.FinishRun();
        Destroy(gameObject);
    }
}
