using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBaseState : MonoBehaviour
{
    public abstract void EnterState();

    public abstract void UpdateState();
}
