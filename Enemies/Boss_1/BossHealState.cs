using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealState : EnemyBaseState
{
    private BossStateManager stateManager;

    public override void EnterState()
    {
        StartCoroutine(HealTimer());
    }

    public override void UpdateState()
    {
        if (stateManager.EnemyHealth <= 0) stateManager.SwitchState(stateManager.DeathState);
    }

    public void SetStateManager(BossStateManager state)
    {
        stateManager = state;
    }

    private IEnumerator HealTimer()
    {
        yield return new WaitForSeconds(2f);
        stateManager.HealNearby();
        stateManager.SwitchState(stateManager.MoveState);
    }
}
