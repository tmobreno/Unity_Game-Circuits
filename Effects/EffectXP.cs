using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectXP : Effect
{
    public override void OnHit(EnemyStateManager enemy, TowerPiece tower)
    {
        GameData.Instance.AddXP(Amount);
    }

    public override void OnHit(EnemyStateManager enemy, TowerPiece tower, Bullet bullet)
    {
        GameData.Instance.AddXP(Amount);
    }
}
