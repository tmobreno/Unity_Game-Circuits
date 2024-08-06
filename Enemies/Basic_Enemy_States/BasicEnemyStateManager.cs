using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyStateManager : EnemyStateManager
{
    [HideInInspector] public BasicEnemyMoveState MoveState;
    [HideInInspector] public BasicEnemyDeathState DeathState;

    // Start is called before the first frame update
    void Start()
    {
        EnemyHealth = ((EnemyHealth / 6f) * GameData.Instance.CurrentWave) + EnemyHealth;
        EnemyXP = ((EnemyXP / 5f) * (GameData.Instance.CurrentWave * 2f)) + EnemyXP;
        EnemyMoveSpeed = (EnemyMoveSpeed + (GameData.Instance.CurrentWave * 0.01f));

        MoveState = gameObject.AddComponent<BasicEnemyMoveState>();
        DeathState = gameObject.AddComponent<BasicEnemyDeathState>();

        MoveState.SetStateManager(this);
        DeathState.SetStateManager(this);

        currentState = MoveState;
        currentState.EnterState();
    }
}
