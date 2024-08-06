using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class BulletTower : TowerPiece
{
    [field: Header("Bullet Properties")]
    [field: SerializeField] public Bullet bullet { get; private set; }
    [field: SerializeField] public float BulletSpeed { get; private set; }

    public override void Attack() {
        FindFirstEnemy();
        if (target == null) return;
        Vector3 t = this.transform.position;
        Bullet b = Instantiate(bullet, new Vector3(t.x, t.y + 2, t.z), this.transform.rotation);
        b.SetParameters(target, this, BulletSpeed);

        if (connection == null || !connection.GetComponent<ComponentPiece>()) return;
        b.SetEffect(connection.GetComponent<ComponentPiece>().effect);
    }
}
