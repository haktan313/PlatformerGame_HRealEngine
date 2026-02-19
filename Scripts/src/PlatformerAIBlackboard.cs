
using HRealEngine.BehaviorTree;

public class PlatformerAIBlackboard : BTBlackboard
{
    public PlatformerAIBlackboard()
    {
        CreateString("PlayerTag", "Player");
        CreateFloat("AttackRange", 1.0f);
    }
}