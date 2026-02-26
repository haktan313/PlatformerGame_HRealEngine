
using HRealEngine.BehaviorTree;

public class PlatformerAIBlackboard : BTBlackboard
{
    public PlatformerAIBlackboard()
    {
        CreateString("PlayerTag", "Player");
        CreateUlong("PlayerEntityID", 0);
    }
}