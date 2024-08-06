using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperTower : TowerPiece
{
    [field: Header("Bullet Properties")]
    [field: SerializeField] public Bullet bullet { get; private set; }
    [field: SerializeField] public float BulletSpeed { get; private set; }

    public override void Attack()
    {
        FindStrongestEnemy();
        if (target == null) return;
        Vector3 t = this.transform.position;
        Bullet b = Instantiate(bullet, new Vector3(t.x, t.y + 4, t.z), this.transform.rotation);
        b.SetParameters(target, this, BulletSpeed);

        if (connection == null || !connection.GetComponent<ComponentPiece>()) return;
        b.SetEffect(connection.GetComponent<ComponentPiece>().effect);
    }
}
