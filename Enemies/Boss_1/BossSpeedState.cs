using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpeedState : EnemyBaseState
{
    private BossStateManager stateManager;

    public override void EnterState()
    {
        StartCoroutine(SpeedTimer());
    }

    public override void UpdateState()
    {
        if (stateManager.EnemyHealth <= 0) stateManager.SwitchState(stateManager.DeathState);
    }

    public void SetStateManager(BossStateManager state)
    {
        stateManager = state;
    }

    private IEnumerator SpeedTimer()
    {
        yield return new WaitForSeconds(2f);
        stateManager.SpeedNearby();
        stateManager.SwitchState(stateManager.MoveState);
    }
}
