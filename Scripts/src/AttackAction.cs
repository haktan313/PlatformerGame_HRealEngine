
using System;
using HRealEngine;
using HRealEngine.BehaviorTree;

public class AttackActionParams : BTActionParams
{
    [BTParameter("Bullet Mesh Path")]
    public string bulletMeshPath = "Meshes/BulletMesh.hmesh";
    [BTBlackboardKey(BTBlackboardKeyAttribute.KeyType.String, "PlayerTagKey")]
    public string PlayerTagKey = "Player";
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
        Vector3 spawnPosition = owner.Position + new Vector3(0, 1, 0); // Spawn below the enemy,
        Vector3 spawnRotation = Vector3.Zero; // No rotation
        Vector3 spawnScale = new Vector3(1, 1, 1); // Default scale
        Console.WriteLine("AttackAction: Starting attack action. Spawn position: " + spawnPosition);
        
        Entity player = FindEntityByName(attackParams.PlayerTagKey);
        if (player == null)        
        {
            Console.WriteLine("AttackAction: Player entity not found with tag: " + attackParams.PlayerTagKey);
            return;
        }
        Vector3 toPlayer = player.Position - owner.Position;
        if (toPlayer.LengthSquared() > 0.0001f)
            toPlayer = toPlayer.Normalized();

        float bulletSpeed = 15.0f;
        Vector3 impulse = toPlayer * bulletSpeed;
        
        Entity bullet = SpawnEntity("BulletPrefab", "Bullet", spawnPosition, spawnRotation, spawnScale);
        Console.WriteLine("Bullet spawned with entity ID: " + bullet.EntityID);
        bullet.Position = spawnPosition;
                
        bullet.AddRigidbody3DComponent(RigidBodyType.Dynamic, false, 0.5f, 0.1f, 0.01f);
        Rigidbody3DComponent rb = bullet.GetComponent<Rigidbody3DComponent>();
        if (rb != null)                
        {
            rb.ApplyLinearImpulse(impulse);
            Console.WriteLine("Bullet linear impulse applied: " + impulse);
        }
        bullet.AddBoxCollider3DComponent(true, new Vector3(.5f, .75f, 0.5f), new Vector3(0, 0.75f, 0));
        bullet.AddMeshRendererComponent(attackParams.bulletMeshPath);
    }
    
    public override NodeStatus Update()
    {
        return NodeStatus.Success;
    }
}