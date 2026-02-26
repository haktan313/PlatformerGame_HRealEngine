

using System;
using HRealEngine;
using HRealEngine.BehaviorTree;

public class HasTargetParams : BTConditionParams
{
    [BTBlackboardKey(BTBlackboardKeyAttribute.KeyType.ULong, "TargetEntityIDKey")]
    public string TargetEntityIDKey = "TargetEntityID";
}

public class HasTarget : BTCondition
{
    private HasTargetParams hasTargetParams;
    
    public HasTarget()
    {
        hasTargetParams = new HasTargetParams();
        SetParameters(hasTargetParams);
    }
    public override void SetParameters(BTConditionParams param)
    {
        base.SetParameters(param);
        hasTargetParams = param as HasTargetParams;
    }
    protected override void OnInitialize()
    {
        hasTargetParams = parameters as HasTargetParams;
    }

    public override void OnStart()
    {
        Console.WriteLine("HasTarget: Checking for target entity in blackboard with key: " + hasTargetParams.TargetEntityIDKey);
    }

    public override bool CheckCondition()
    {
        ulong targetEntityID = blackboard.GetUlong(hasTargetParams.TargetEntityIDKey);
        if (targetEntityID != 0)
        {
            Entity targetEntity = FromID(targetEntityID);
            if (targetEntity != null)
            {
                return true;
            }
        }
        return false;
    }

    public override void OnFinished()
    {
    }
}