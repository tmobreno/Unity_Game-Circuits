using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectExplosive : Effect
{
    public override void OnHit(EnemyStateManager enemy, TowerPiece tower)
    {
    }

    public override void OnHit(EnemyStateManager enemy, TowerPiece tower, Bullet bullet)
    {
        List<EnemyStateManager> nearbyTargets = FindNearestEnemy(enemy, bullet);
        if (nearbyTargets == null) return;
        foreach (EnemyStateManager target in nearbyTargets)
        {
            target.TakeDamage(tower.GetDamage() / 2);
        }
    }

    private List<EnemyStateManager> FindNearestEnemy(EnemyStateManager enemy, Bullet bullet)
    {
        Collider[] hitColliders = Physics.OverlapSphere(bullet.transform.position, 5f);
        List<EnemyStateManager> enemiesInRange = new();
        foreach (var hitCollider in hitColliders)
        {
            var enemyComponent = hitCollider.GetComponent<EnemyStateManager>();
            if (enemyComponent == null || enemyComponent == enemy) continue;
            enemiesInRange.Add(hitCollider.gameObject.GetComponent<EnemyStateManager>());
        }
        return enemiesInRange;
    }
}
