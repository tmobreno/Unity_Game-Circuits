using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStateManager : EnemyStateManager
{
    public enum Type { Heal, Speed, Flex }

    [field: Header("Boss Type Specification")]
    [field: SerializeField] public Type BossType { get; private set; }

    [HideInInspector] public BossMoveState MoveState;

    [HideInInspector] public BossHealState HealState;
    [HideInInspector] public BossSpeedState SpeedState;
    [HideInInspector] public BossFlexState FlexState;

    [HideInInspector] public BossDeathState DeathState;

    [field: Header("Boss Type Properties")]
    [field: SerializeField] public float HealAmount { get; private set; }
    [field: SerializeField] public float SpeedAmount { get; private set; }
    [field: SerializeField] public float AbilityTimer { get; private set; }
    [field: SerializeField] public EnemyStateManager EnemyToSpawn { get; private set; }

    [field: Header("Boss Type FX")]
    [field: SerializeField] public GameObject HealFX { get; private set; }
    [field: SerializeField] public GameObject SpeedFX { get; private set; }
    [field: SerializeField] public GameObject SpawnFX { get; private set; }


    // Start is called before the first frame update
    void Start()
    {
        MoveState = gameObject.AddComponent<BossMoveState>();
        HealState = gameObject.AddComponent<BossHealState>();
        SpeedState = gameObject.AddComponent<BossSpeedState>();
        FlexState = gameObject.AddComponent<BossFlexState>();
        DeathState = gameObject.AddComponent<BossDeathState>();

        MoveState.SetStateManager(this);
        HealState.SetStateManager(this);
        SpeedState.SetStateManager(this);
        FlexState.SetStateManager(this);
        DeathState.SetStateManager(this);

        currentState = MoveState;
        currentState.EnterState();
    }

    public void HealNearby()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 4f);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.GetComponent<EnemyStateManager>() == null) continue;
            if (hitCollider.name == this.name) continue;
            EnemyStateManager e = hitCollider.GetComponent<EnemyStateManager>();
            e.HealthChange(HealAmount);
        }
        Vector3 t = this.transform.position;
        Instantiate(HealFX, new Vector3(t.x, t.y + 1, t.z), this.transform.rotation);
    }

    public void SpeedNearby()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 4f);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.GetComponent<EnemyStateManager>() == null) continue;
            if (hitCollider.name == this.name) continue;
            EnemyStateManager e = hitCollider.GetComponent<EnemyStateManager>();
            e.SpeedChange(SpeedAmount);
        }
        Vector3 t = this.transform.position;
        Instantiate(SpeedFX, new Vector3(t.x, t.y + 1, t.z), this.transform.rotation);
    }

    public void SpawnEnemies()
    {
        EnemyStateManager e = Instantiate(EnemyToSpawn, this.transform.position, this.transform.rotation);
        e.SetWaypoints(new List<Transform>(Waypoints));
        e.SetIndex(EnemyIndex);
        e.SetNext(NextWaypoint);
        Vector3 t = this.transform.position;
        Instantiate(SpawnFX, new Vector3(t.x, t.y + 1, t.z), this.transform.rotation);
    }

    public override void SpeedChange(float amount)
    {
    }
}
