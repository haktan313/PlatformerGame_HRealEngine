// Scripts/BT/CooldownDecorator.cs

using System;
using HRealEngine;
using HRealEngine.BehaviorTree;

public class CooldownParams : BTDecoratorParams
{
    [BTParameter("Cooldown Time")]
    public float CooldownTime = 5.0f;
}

public class CooldownDecorator : BTDecorator {
    private CooldownParams cooldownParams;
    private float elapsedTime = 0f;
    private bool onCooldown = false;

    public CooldownDecorator()
    {
        cooldownParams = new CooldownParams();
        SetParameters(cooldownParams);
    }

    protected override void OnInitialize()
    {
        cooldownParams = parameters as CooldownParams;
    }
    
    public override void SetParameters(BTDecoratorParams param)
    {
        base.SetParameters(param);
        cooldownParams = param as CooldownParams;
    }

    public override bool CanExecute()
    {
        if (!onCooldown) return true;

        elapsedTime += GetDeltaTime();

        if (elapsedTime >= cooldownParams.CooldownTime)
        {
            onCooldown = false;
            elapsedTime = 0f;
            Console.WriteLine("CooldownDecorator: Cooldown finished, can execute.");
            return true;
        }
        return false;
    }

    public override void OnFinishedResult(ref NodeStatus status)
    {
        if (status == NodeStatus.Success || status == NodeStatus.Failure)
        {
            onCooldown = true;
            elapsedTime = 0f;
            Console.WriteLine($"CooldownDecorator: Cooldown started for {cooldownParams.CooldownTime}s");
        }
    }
}