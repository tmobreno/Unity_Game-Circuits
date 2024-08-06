using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class GatlingTower : TowerPiece
{
    [field: Header("Bullet Properties")]
    [field: SerializeField] public Bullet bullet { get; private set; }
    [field: SerializeField] public float BulletSpeed { get; private set; }

    public override void Attack()
    {
        FindFirstEnemy();
        if (target == null) return;
        Vector3 t = this.transform.position;
        Bullet b = Instantiate(bullet, new Vector3(t.x, t.y + 3, t.z), this.transform.rotation);
        b.SetParameters(target, this, BulletSpeed);

        if (connection == null || !connection.GetComponent<ComponentPiece>()) return;
        b.SetEffect(connection.GetComponent<ComponentPiece>().effect);
    }
}
