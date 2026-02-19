
using System;
using HRealEngine.BehaviorTree;

public class AttackActionParams : BTActionParams
{
    [BTParameter("Damage")]
    public int Damage = 10;
    [BTBlackboardKey(BTBlackboardKeyAttribute.KeyType.Float, "Stamina Key")]
    public string StaminaKey = "Stamina";
}

public class AttackAction : BTActionNode
{
    private AttackActionParams attackParams;
    
    public AttackAction()
    {
        attackParams = new AttackActionParams();
        SetParameters(attackParams);
    }
    
    public override void SetParameters(BTActionParams param)
    {
        base.SetParameters(param);
        attackParams = param as AttackActionParams;
    }
    
    protected override void OnInitialize()
    {
        attackParams = parameters as AttackActionParams;
    }
    
    public override void OnStart()
    {
        Console.WriteLine($"AttackAction started. Damage: {attackParams.Damage}");
        float stamina = blackboard.GetFloat(attackParams.StaminaKey);
        Console.WriteLine($"Current stamina: {stamina}");
        
        if (stamina >= 10)
        {
            blackboard.SetFloat(attackParams.StaminaKey, stamina - 10);
            Console.WriteLine($"Attacked player for {attackParams.Damage} damage. Remaining stamina: {blackboard.GetFloat(attackParams.StaminaKey)}");
        }
        else
        {
            Console.WriteLine("Not enough stamina to attack.");
        }
    }
    
    public override NodeStatus Update()
    {
        return NodeStatus.Success;
    }
}