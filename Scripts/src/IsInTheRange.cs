
using System;
using HRealEngine.BehaviorTree;

public class IsInTheRangeParams : BTConditionParams
{
    [BTBlackboardKey(BTBlackboardKeyAttribute.KeyType.Float, "Distance Key")]
    public string DistanceKey = "DistanceToPlayer"; 
    [BTParameter("Range")]
    public float MinRange = 250.0f;
}
public class IsInTheRange : BTCondition
{
    private IsInTheRangeParams rangeParams;
        
    public IsInTheRange()
    {
        rangeParams = new IsInTheRangeParams();
        SetParameters(rangeParams);
    }
        
    protected override void OnInitialize()
    {
        rangeParams = parameters as IsInTheRangeParams;
    }
        
    public override bool CheckCondition()
    {
        if (string.IsNullOrEmpty(rangeParams.DistanceKey))
            return false;

        float distance = blackboard.GetFloat(rangeParams.DistanceKey);
        Console.WriteLine($"Checking range condition: Distance={distance}, MinRange={rangeParams.MinRange}");
        return distance >= rangeParams.MinRange;
    }
}