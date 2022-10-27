using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Quest/Condition/IsQuestComplete", fileName = "IsquestComplete_")]
public class IsQuestcomplete : Condition
{
    [SerializeField]
    private Quest target;
    public override bool IsPass(Quest quest)
    {
        return QuestSystem.Instance.ContainsInCompleteQuests(target);
    }
}
