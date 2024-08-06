using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMoveState : EnemyBaseState
{
    private BossStateManager stateManager;

    public override void EnterState()
    {
        StartCoroutine(AbilityTimer());
    }

    public override void UpdateState()
    {
        if (stateManager.EnemyHealth <= 0) stateManager.SwitchState(stateManager.DeathState);

        var step = stateManager.EnemyMoveSpeed * Time.deltaTime;
        Vector3 next = stateManager.Waypoints[stateManager.NextWaypoint].position;
        transform.position = Vector3.MoveTowards(transform.position, next, step);

        // Reached End
        if (Vector3.Distance(transform.position, next) < 0.001f)
        {
            if (!stateManager.CheckNext())
            {
                GameData.Instance.TakeDamage(1);
                stateManager.DeathState.DontGiveXP();
                stateManager.SwitchState(stateManager.DeathState);
            }
            else stateManager.IncrementTarget();
        }
    }

    public void SetStateManager(BossStateManager state)
    {
        stateManager = state;
    }

    private IEnumerator AbilityTimer()
    {
        yield return new WaitForSeconds(stateManager.AbilityTimer);
        if (stateManager.BossType == BossStateManager.Type.Heal) stateManager.SwitchState(stateManager.HealState);
        if (stateManager.BossType == BossStateManager.Type.Speed) stateManager.SwitchState(stateManager.SpeedState);
        if (stateManager.BossType == BossStateManager.Type.Flex) stateManager.SwitchState(stateManager.FlexState);
    }
}
