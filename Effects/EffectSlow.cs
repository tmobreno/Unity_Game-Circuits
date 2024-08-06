using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectSlow : Effect
{
    public override void OnHit(EnemyStateManager enemy, TowerPiece tower)
    {
        enemy.SpeedChange(Amount);
    }

    public override void OnHit(EnemyStateManager enemy, TowerPiece tower, Bullet bullet)
    {
        enemy.SpeedChange(Amount);
    }
}
