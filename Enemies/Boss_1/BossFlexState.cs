using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFlexState : EnemyBaseState
{
    private BossStateManager stateManager;

    public override void EnterState()
    {
        StartCoroutine(Timer());
    }

    public override void UpdateState()
    {
        if (stateManager.EnemyHealth <= 0) stateManager.SwitchState(stateManager.DeathState);
    }

    public void SetStateManager(BossStateManager state)
    {
        stateManager = state;
    }

    private IEnumerator Timer()
    {
        yield return new WaitForSeconds(2f);
        int rand = Random.Range(0, 3);

        if (rand == 0) stateManager.HealNearby();
        if (rand == 1) stateManager.SpeedNearby();
        if (rand == 2) stateManager.SpawnEnemies();

        stateManager.SwitchState(stateManager.MoveState);
    }
}
