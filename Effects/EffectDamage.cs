using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectDamage : Effect
{

    public override void OnHit(EnemyStateManager enemy, TowerPiece tower)
    {
        enemy.TakeDamage(Amount);
    }

    public override void OnHit(EnemyStateManager enemy, TowerPiece tower, Bullet bullet)
    {
        enemy.TakeDamage(Amount);
    }
}
