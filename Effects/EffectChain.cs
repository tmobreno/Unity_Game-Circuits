using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

public class EffectChain : Effect
{
    public override void OnHit(EnemyStateManager enemy, TowerPiece tower)
    {
    }

    public override void OnHit(EnemyStateManager enemy, TowerPiece tower, Bullet bullet)
    {
        EnemyStateManager nextTarget = FindNearestEnemy(enemy, bullet);
        if (nextTarget == null) return;
        Bullet b = Instantiate(bullet, bullet.transform.position, bullet.transform.rotation);
        b.SetParameters(nextTarget, tower, bullet.BulletSpeed);
        b.SetEffect(null);
    }

    private EnemyStateManager FindNearestEnemy(EnemyStateManager enemy, Bullet bullet)
    {
        Collider[] hitColliders = Physics.OverlapSphere(bullet.transform.position, 3.5f);
        EnemyStateManager nearestEnemy = null;
        float minDistance = float.MaxValue;
        foreach (var hitCollider in hitColliders)
        {
            var enemyComponent = hitCollider.GetComponent<EnemyStateManager>();
            if (enemyComponent == null || enemyComponent == enemy) continue;
            float distance = Vector3.Distance(bullet.transform.position, hitCollider.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                nearestEnemy = hitCollider.gameObject.GetComponent<EnemyStateManager>();
            }
        }
        return nearestEnemy;
    }
}
