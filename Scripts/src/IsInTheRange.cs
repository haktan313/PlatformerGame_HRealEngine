
using System;
using HRealEngine;
using HRealEngine.BehaviorTree;

public class IsInTheRangeParams : BTConditionParams
{
    [BTBlackboardKey(BTBlackboardKeyAttribute.KeyType.String, "PlayerTagKey")]
    public string PlayerTagKey = "Player"; 
    [BTParameter("Range")]
    public float MinRange = 3.0f;
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
    
    public override void SetParameters(BTConditionParams param)
    {
        base.SetParameters(param);
        rangeParams = param as IsInTheRangeParams;
    }
        
    public override bool CheckCondition()
    {
        if (string.IsNullOrEmpty(rangeParams.PlayerTagKey))
            return false;

        Entity player = FindEntityByName(rangeParams.PlayerTagKey);
        if (player == null)
            return false;
        TransformComponent playerTransform = player.GetComponent<TransformComponent>();
        TransformComponent ownerTransform = owner.GetComponent<TransformComponent>();
        if (playerTransform == null || ownerTransform == null)
            return false;
        var playerPos = playerTransform.Translation;
        var ownerPos = ownerTransform.Translation;

        float dx = playerPos.X - ownerPos.X;
        float dy = playerPos.Y - ownerPos.Y;
        float dz = playerPos.Z - ownerPos.Z;

        float distance = (float)Math.Sqrt(dx * dx + dy * dy + dz * dz);
        if (distance <= rangeParams.MinRange)
        {
            Console.WriteLine($"IsInTheRange: Player is within range. Distance: {distance}, Range: {rangeParams.MinRange}");
        }
        return distance <= rangeParams.MinRange;
    }
}