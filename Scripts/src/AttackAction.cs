
using System;
using HRealEngine;
using HRealEngine.BehaviorTree;

public class AttackActionParams : BTActionParams
{
    [BTParameter("Damage")]
    public int Damage = 10;
    [BTParameter("Bullet Mesh Path")]
    public string bulletMeshPath = "Meshes/BulletMesh.hmesh";
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
        Vector3 spawnPosition = owner.Translation + new Vector3(0, -1, 0); // Spawn below the enemy,
        Vector3 spawnRotation = Vector3.Zero; // No rotation
        Vector3 spawnScale = new Vector3(1, 1, 1); // Default scale
                
        Entity bullet = SpawnEntity("BulletPrefab", "Bullet", spawnPosition, spawnRotation, spawnScale);
        Console.WriteLine("Bullet spawned with entity ID: " + bullet.EntityID);
        bullet.Translation = spawnPosition;
                
        bullet.AddRigidbody3DComponent(RigidBodyType.Dynamic, false, 0.5f, 0.1f, 0.01f);
        Rigidbody3DComponent rb = bullet.GetComponent<Rigidbody3DComponent>();
        if (rb != null)                
        {
            rb.ApplyLinearImpulse(new Vector3(-10, 5, 10));
            Console.WriteLine("Bullet linear impulse applied: " + new Vector3(-10, 5, 10));
        }
        bullet.AddBoxCollider3DComponent(true, new Vector3(.5f, .75f, 0.5f), new Vector3(0, 0.75f, 0));
        bullet.AddMeshRendererComponent(attackParams.bulletMeshPath);
    }
    
    public override NodeStatus Update()
    {
        return NodeStatus.Success;
    }
}