
using HRealEngine.BehaviorTree;

public class PlatformerAIBlackboard : BTBlackboard
{
    public PlatformerAIBlackboard()
    {
        CreateBool("CanAttack", false);
        CreateFloat("DistanceToPlayer", 500.0f);
    }
}