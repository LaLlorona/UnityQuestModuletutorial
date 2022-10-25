using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public enum TaskState
{
    Inactive,
    Running,
    Complete
}

[CreateAssetMenu(menuName ="Quest/Task/Task", fileName = "Task_")]
public class Task : ScriptableObject
{
    #region Events
    public delegate void StateChangeHandler(Task task, TaskState currentState, TaskState prevState);
    public delegate void SuccessChangeHandler(Task task, int currentSuccess, int prevSuccess);
    #endregion

    [SerializeField]
    private Category category;

    [Header("Text")]
    [SerializeField]
    private string codeName;
    [SerializeField]
    private string description;

    [Header("Action")]
    [SerializeField]
    private TaskAction action;

    [Header("Setting")]
    [SerializeField]
    private InitialSuccessValue initialSuccessValue;

    [Header("Target")]
    [SerializeField]
    private TaskTarget[] targets;

    [SerializeField]
    private int neededSuccessToComplete;

    [SerializeField]
    private bool canReceiveReportsDuringCompletion;

    private TaskState state;
    private int currentSuccess;

    public event StateChangeHandler onStateChanged;
    public event SuccessChangeHandler onSuccessChanged;

    public int CurrentSuccess
    {
        get => currentSuccess;
        set
        {
            Debug.Log($"Debug from Task.cs, current neededSuccessTocomplete is {neededSuccessToComplete}");
            int prevSuccess = currentSuccess;
            int updateValue = Mathf.Clamp(value, 0, neededSuccessToComplete);
    
            currentSuccess = Mathf.Clamp(value, 0, neededSuccessToComplete);
            Debug.Log($"Debug from Task.cs, currentSucess after update is is {currentSuccess}, value is {value}");
            if (currentSuccess != prevSuccess)
            {
                State = currentSuccess == neededSuccessToComplete ? TaskState.Complete : TaskState.Running;
                onSuccessChanged?.Invoke(this, currentSuccess, prevSuccess);
            }
           
        }
    }

    public int NeededSuccessToComplete => neededSuccessToComplete;

    public TaskState State
    {
        get => state;
        set
        {
            var prevState = state;
            state = value;
            onStateChanged?.Invoke(this, state, prevState);
        }
    }

    public string CodeName => codeName;
    public string Description => description;

    public Category Category => category;

    public void ReceiveReport(int successCount)
    {
        Debug.Log($"In receive report of Task.cs, successCount is {successCount}");
        int updateValue = action.Run(this, CurrentSuccess, successCount);
        CurrentSuccess = updateValue;
        Debug.Log($"In receive report of Task.cs, successCount is {successCount} and CurrentSuccess is ${CurrentSuccess}");
    }

    public bool IsComplete => State == TaskState.Complete;

    public Quest Owner { get; private set; }

    public void Setup(Quest owner)
    {
        Owner = owner;
    }

    public void Start()
    {
        State = TaskState.Running;
        if (initialSuccessValue)
        {
            currentSuccess = initialSuccessValue.GetValue(this);
        }
    }

    public void End()
    {
        onStateChanged = null;
        onSuccessChanged = null;
    }

    public void Complete()
    {
        CurrentSuccess = neededSuccessToComplete;
    }

    public bool IsTarget(string category, object target)
    {
        return (Category == category && targets.Any(x => x.IsEqual(target)) && 
            (!IsComplete || (IsComplete && canReceiveReportsDuringCompletion )));
    }

    public bool ContainsTarget(object target) => targets.Any(x => x.IsEqual(target));


}
