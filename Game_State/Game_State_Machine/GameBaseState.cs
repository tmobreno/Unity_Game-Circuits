using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameBaseState : MonoBehaviour
{
    public abstract void EnterState();

    public abstract void UpdateState();
}
