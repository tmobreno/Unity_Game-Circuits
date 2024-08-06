using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZapTower : TowerPiece
{
    [field: Header("Tower FX")]
    [SerializeField] private GameObject HitFX;

    public override void Attack() {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, GetRange());
        int count = 0;
        Vector3 t = this.transform.position;

        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.GetComponent<EnemyStateManager>() == null) continue;
            EnemyStateManager e = hitCollider.GetComponent<EnemyStateManager>();
            e.TakeDamage(GetDamage());
            count++;

            if (connection == null || !connection.GetComponent<ComponentPiece>()) continue;
            connection.GetComponent<ComponentPiece>().effect.OnHit(e, this);
        }

        if (count > 0) Instantiate(HitFX, new Vector3(t.x, t.y + 2.5f, t.z), this.transform.rotation);
    }
}
