using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTowerSelectionState : GameBaseState
{
    public override void EnterState()
    {
        UITowerSelect.Instance.PullUpTowerSelectMenu(2, 1, 1, 2);
        UITowerSelect.Instance.GenerateRandomStartPiece();
        GameStateManager.Instance.SwitchState(GameStateManager.Instance.WaveChangeState);
    }

    public override void UpdateState()
    {
    }
}
