using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyDeathState : EnemyBaseState
{
    private BasicEnemyStateManager basicState;

    private bool giveXP = true;

    public override void EnterState()
    {
        Instantiate(basicState.DeathFX, this.transform.position, this.transform.rotation);
        this.gameObject.SetActive(false);
        if(giveXP) GameData.Instance.AddXP(basicState.EnemyXP);
        Destroy(this.gameObject, 2f);
    }

    public override void UpdateState()
    {
    }

    public void SetStateManager(BasicEnemyStateManager state)
    {
        basicState = state;
    }

    public void DontGiveXP()
    {
        giveXP = false;
    }
}
