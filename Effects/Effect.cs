using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Effect : MonoBehaviour
{
    protected float Amount;

    // Works for non-bullet based towers
    public abstract void OnHit(EnemyStateManager enemy, TowerPiece tower);

    // Works for bullet based towers
    public abstract void OnHit(EnemyStateManager enemy, TowerPiece tower, Bullet bullet);

    public virtual void SetAmount(float set)
    {
        Amount = set;
    }
}
