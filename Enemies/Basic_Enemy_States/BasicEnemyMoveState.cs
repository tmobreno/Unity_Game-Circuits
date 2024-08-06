using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class BasicEnemyMoveState : EnemyBaseState
{
    private BasicEnemyStateManager basicState;

    public override void EnterState()
    {
    }

    public override void UpdateState()
    {
        if(basicState.EnemyHealth <= 0) basicState.SwitchState(basicState.DeathState);

        var step = basicState.EnemyMoveSpeed * Time.deltaTime;
        Vector3 next = basicState.Waypoints[basicState.NextWaypoint].position;
        transform.position = Vector3.MoveTowards(transform.position, next, step);

        // Reached End
        if (Vector3.Distance(transform.position, next) < 0.001f)
        {
            if (!basicState.CheckNext())
            {
                GameData.Instance.TakeDamage(1);
                basicState.DeathState.DontGiveXP();
                basicState.SwitchState(basicState.DeathState);
            }
            else basicState.IncrementTarget();
        }
    }

    public void SetStateManager(BasicEnemyStateManager state)
    {
        basicState = state;
    }
}
