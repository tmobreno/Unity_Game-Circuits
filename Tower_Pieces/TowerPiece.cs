using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using static UnityEngine.GraphicsBuffer;
using UnityEditor;

public abstract class TowerPiece : PlaceableObject
{
    [field: Header("Tower Properties")]
    [field: SerializeField] public float Damage { get; private set; }
    [field: SerializeField] public float AttackSpeed { get; private set; }
    [field: SerializeField] public float Range { get; private set; }
    [field: SerializeField] public GameObject RangeIndicator { get; private set; }
    private GameObject StoredRangeIndicator;

    protected EnemyStateManager target;

    protected float timer;

    public abstract void Attack();

    public virtual void Start()
    {
        timer = Time.time + AttackSpeed;
        StoredRangeIndicator = Instantiate(RangeIndicator, transform);
        StoredRangeIndicator.SetActive(false);
        UpdateRangeIndicator(Range + TowerModifiers.Instance.RangeMod);
    }

    public virtual void Update()
    {
        if (Time.time > timer)
        {
            Attack();
            timer = Time.time + GetSpeed();
        }
    }

    private void UpdateRangeIndicator(float setRange)
    {
        StoredRangeIndicator.transform.position = transform.position;
        StoredRangeIndicator.transform.localScale = new Vector3(setRange, 0.01f, setRange);
    }

    // Enemy Search Algorithms

    private void FindTargetEnemy(System.Func<EnemyStateManager, float> selector)
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, GetRange());
        target = null;
        float bestValue = float.MaxValue;
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.GetComponent<EnemyStateManager>() == null) continue;
            EnemyStateManager e = hitCollider.GetComponent<EnemyStateManager>();
            float value = selector(e);
            if (value < bestValue)
            {
                bestValue = value;
                target = e;
            }
        }
    }

    protected void FindFirstEnemy()
    {
        FindTargetEnemy(e => e.EnemyIndex);
    }

    protected void FindStrongestEnemy()
    {
        FindTargetEnemy(e => e.EnemyHealth);
    }

    protected void FindRandomEnemy()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, GetRange());
        List<EnemyStateManager> enemies = new();
        target = null;
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.GetComponent<EnemyStateManager>() == null) continue;
            enemies.Add(hitCollider.GetComponent<EnemyStateManager>());
        }
        if (enemies.Count == 0) return;
        target = enemies[Random.Range(0, enemies.Count)];
    }

    // Property Getters
    public float GetDamage()
    {
        return Damage + TowerModifiers.Instance.DamageMod;
    }

    public float GetRange()
    {
        if (StoredRangeIndicator != null && StoredRangeIndicator.transform.localScale.x != Range + TowerModifiers.Instance.RangeMod)
            UpdateRangeIndicator(Range + TowerModifiers.Instance.RangeMod);
        return Range + TowerModifiers.Instance.RangeMod;
    }

    public float GetSpeed()
    {
        return (AttackSpeed / TowerModifiers.Instance.SpeedMod);
    }

    public override void ShowRange(bool set)
    {
        if(StoredRangeIndicator != null) StoredRangeIndicator.SetActive(set);
    }
}
