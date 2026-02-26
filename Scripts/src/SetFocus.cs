using System;
using HRealEngine;
using HRealEngine.BehaviorTree;

public class SetFocusParams : BTActionParams
{
    [BTBlackboardKey(BTBlackboardKeyAttribute.KeyType.ULong, "TargetEntityIDKey")]
    public string TargetEntityIDKey = "TargetEntityID";
}
public class SetFocus : BTActionNode
{
    private SetFocusParams focusParams;
    private Entity focusedEntity;
        
    public SetFocus()
    {
        focusParams = new SetFocusParams();
        SetParameters(focusParams);
    }
    public override void SetParameters(BTActionParams param)
    {
        base.SetParameters(param);
        focusParams = param as SetFocusParams;
    }
    protected override void OnInitialize()
    {
        focusParams = parameters as SetFocusParams;
    }

    public override void OnStart()
    {
        ulong targetEntityID = blackboard.GetUlong(focusParams.TargetEntityIDKey);
        if (targetEntityID != 0)
        {
            focusedEntity = FromID(targetEntityID);
        }
    }

    public override NodeStatus Update()
    {
        if (focusedEntity != null)
        {
            Enemy enemy = owner as Enemy;
            if (enemy != null)
            {
                enemy.SetFocusToTarget(focusedEntity);
                return NodeStatus.Running;
            }
        }
        return NodeStatus.Failure;
    }
}