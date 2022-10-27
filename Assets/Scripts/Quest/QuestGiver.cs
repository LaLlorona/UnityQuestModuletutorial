using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestGiver : MonoBehaviour
{
    [SerializeField]
    private Quest[] quests;

    private void Start()
    {
        print($"length of completed quest is {QuestSystem.Instance.completedQuests.Count}");
        foreach (var quest in QuestSystem.Instance.completedQuests)
        {
            print(quest.CodeName);
        }
    
        foreach (var quest in quests)
        {
            
            if (quest.IsAcceptable && !QuestSystem.Instance.ContainsInCompleteQuests(quest))
            {
                QuestSystem.Instance.Register(quest);
            }
        }
    }

    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.F))
    //    {
    //        foreach (var quest in quests)
    //        {
    //            if (quest.IsAcceptable && !QuestSystem.Instance.ContainsInCompleteQuests(quest))
    //            {
    //                QuestSystem.Instance.Register(quest);
    //            }
    //        }
    //    }
    //}
}
