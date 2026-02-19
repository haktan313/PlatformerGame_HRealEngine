
using System;
using HRealEngine.BehaviorTree;

public class CanAttackParams : BTConditionParams
{
    [BTBlackboardKey(BTBlackboardKeyAttribute.KeyType.Bool, "Can Attack Key")]
    public string CanAttackKey = "CanAttack";
}

public class CanAttack : BTCondition
{
    public CanAttackParams canAttackParams;
    
    public CanAttack()
    {
        canAttackParams = new CanAttackParams();
        SetParameters(canAttackParams);
    }
    
    protected override void OnInitialize()
    {
        canAttackParams = parameters as CanAttackParams;
    }
    
    public override bool CheckCondition()
    {
        if (string.IsNullOrEmpty(canAttackParams.CanAttackKey))
            return false;

        bool canAttack = blackboard.GetBool(canAttackParams.CanAttackKey);
        Console.WriteLine($"Checking CanAttack condition: {canAttackParams.CanAttackKey}={canAttack}");
        return canAttack;
    }
}