using HRealEngine;
using HRealEngine.BehaviorTree;

public class ClearFocusParams : BTActionParams
{

}
public class ClearFocus : BTActionNode
{
    private ClearFocusParams clearFocusParams;

    public ClearFocus()
    {
        clearFocusParams = new ClearFocusParams();
        SetParameters(clearFocusParams);
    }
    public override void SetParameters(BTActionParams param)
    {
        base.SetParameters(param);
        clearFocusParams = param as ClearFocusParams;
    }
    protected override void OnInitialize()
    {
        clearFocusParams = parameters as ClearFocusParams;
    }
    public override NodeStatus Update()
    {
        Enemy enemy = owner as Enemy;
        if (enemy != null)
        {
            enemy.ClearFocus();
            return NodeStatus.Running;
        }
        return NodeStatus.Failure;
    }
        
}