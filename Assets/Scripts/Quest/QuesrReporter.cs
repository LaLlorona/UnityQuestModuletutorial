using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class QuesrReporter : MonoBehaviour
{
    [SerializeField]
    private Category category;

    [SerializeField]
    private TaskTarget target;

    [SerializeField]
    private int successCount;

    [SerializeField]
    private string[] colliderTags;
    private void OnTriggerEnter(Collider other)
    {
        ReportIfPassCondition(other);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ReportIfPassCondition(collision);
    }

    public void Report()
    {
        QuestSystem.Instance.ReceiveReport(category, target, successCount);
    }

    private void ReportIfPassCondition(Component other)
    {
        if (colliderTags.Any(x => other.CompareTag(x)))
        {
            Report();
        }
    }


}
