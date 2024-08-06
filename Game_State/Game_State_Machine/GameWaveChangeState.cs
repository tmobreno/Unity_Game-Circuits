using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameWaveChangeState : GameBaseState
{
    public override void EnterState()
    {
        StartCoroutine(ChangeTimer());
    }

    public override void UpdateState()
    {
    }

    private IEnumerator ChangeTimer()
    {
        GameStateManager state = GameStateManager.Instance;
        yield return new WaitForSeconds(10f);
        if (GameData.Instance.CurrentWave != GameData.Instance.MaxWave) state.SwitchState(state.SpawningState);
    }
}
