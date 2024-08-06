using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpreadTower : TowerPiece
{
    [field: Header("Bullet Properties")]
    [field: SerializeField] public Bullet Bullet { get; private set; }
    [field: SerializeField] public float BulletSpeed { get; private set; }
    [field: SerializeField] public int BulletAmount { get; private set; }

    public override void Attack() {
        for (int i = 0; i < BulletAmount; i++)
        {
            FindRandomEnemy();
            if (target == null) return;
            Vector3 t = this.transform.position;
            Bullet b = Instantiate(Bullet, new Vector3(t.x, t.y + 3, t.z), this.transform.rotation);
            b.SetParameters(target, this, BulletSpeed);

            if (connection == null || !connection.GetComponent<ComponentPiece>()) continue;
            b.SetEffect(connection.GetComponent<ComponentPiece>().effect);
        }
    }

}
