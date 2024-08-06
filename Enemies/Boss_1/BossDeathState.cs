using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDeathState : EnemyBaseState
{
    private BossStateManager stateManager;
    private bool giveXP = true;

    public override void EnterState()
    {
        Instantiate(stateManager.DeathFX, this.transform.position, this.transform.rotation);
        this.gameObject.SetActive(false);
        if (giveXP) GameData.Instance.AddXP(stateManager.EnemyXP);
        Destroy(this.gameObject, 2f);
    }

    public override void UpdateState()
    {
    }

    public void SetStateManager(BossStateManager state)
    {
        stateManager = state;
    }

    public void DontGiveXP()
    {
        giveXP = false;
    }
}
