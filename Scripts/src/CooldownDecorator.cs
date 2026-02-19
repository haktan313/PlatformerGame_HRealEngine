// Scripts/BT/CooldownDecorator.cs
using HRealEngine;
using HRealEngine.BehaviorTree;

public class CooldownParams : BTDecoratorParams
{
    [BTParameter("Cooldown Time")]
    public float CooldownTime = 5.0f;

    [BTParameter("Reset On Fail")]
    public bool ResetOnFail = false;
}

public class CooldownDecorator : BTDecorator
{
    private CooldownParams cooldownParams;
    private float lastExecutionTime = -999f;

    public CooldownDecorator()
    {
        cooldownParams = new CooldownParams();
        SetParameters(cooldownParams);
    }

    protected override void OnInitialize()
    {
        cooldownParams = parameters as CooldownParams;
    }

    public override bool CanExecute()
    {
        // TODO: You'll need to expose Time.GetTime() via InternalCalls
        float currentTime = 0f; // Replace with actual time
        float elapsed = currentTime - lastExecutionTime;
        
        return elapsed >= cooldownParams.CooldownTime;
    }

    public override void OnFinishedResult(ref NodeStatus status)
    {
        if (status == NodeStatus.Success || 
            (status == NodeStatus.Failure && !cooldownParams.ResetOnFail))
        {
            lastExecutionTime = 0f; // Replace with actual time
        }
    }
}