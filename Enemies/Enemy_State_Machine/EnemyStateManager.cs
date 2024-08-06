using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyStateManager : MonoBehaviour
{
    [HideInInspector] public EnemyBaseState currentState;

    public virtual List<Transform> Waypoints { get; protected set; }

    public int NextWaypoint { get; protected set; }

    public int EnemyIndex { get; protected set; }

    [field: Header("Base Enemy Properties")]
    [field: SerializeField] public float EnemyHealth { get; protected set; }
    [field: SerializeField] public float EnemyMoveSpeed { get; protected set; }
    [field: SerializeField] public float EnemyXP { get; protected set; }

    [field: Header("Enemy FX")]
    [field: SerializeField] public GameObject HitFX { get; protected set; }
    [field: SerializeField] public GameObject DeathFX { get; protected set; }

    // Update is called once per frame
    public virtual void Update()
    {
        currentState.UpdateState();
    }

    public virtual void SwitchState(EnemyBaseState state)
    {
        currentState = state;
        currentState.EnterState();
    }

    public virtual void SetWaypoints(List<Transform> set)
    {
        Waypoints = set;
    }

    public virtual void SetIndex(int set)
    {
        EnemyIndex = set;
    }

    public virtual void IncrementTarget()
    {
        NextWaypoint++;
    }

    public virtual bool CheckNext()
    {
        if (Waypoints.Count == NextWaypoint + 1)
        {
            return false;
        }
        return true;
    }

    public virtual void SetNext(int next)
    {
        NextWaypoint = next;
    }

    public virtual void TakeDamage(float damage)
    {
        Instantiate(HitFX, this.transform.position, this.transform.rotation);
        EnemyHealth -= damage;
    }

    public virtual void HealthChange(float health)
    {
        EnemyHealth += health;
    }

    public virtual void SpeedChange(float amount)
    {
        EnemyMoveSpeed *= amount;
        if (EnemyMoveSpeed < 0.5) EnemyMoveSpeed = 0.5f;
    }
}
