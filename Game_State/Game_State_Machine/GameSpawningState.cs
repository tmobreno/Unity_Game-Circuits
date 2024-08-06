using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSpawningState : GameBaseState
{
    private int SpawnAmount;

    private int EnemyIndex;

    [SerializeField] private List<Wave> chosenWaves;

    [SerializeField] private List<EnemyStateManager> waveEnemies;

    private void Start()
    {
        chosenWaves = new();
        EnemyIndex = 0;
        ChooseAllWaves();
    }

    public override void EnterState()
    {
        waveEnemies = new();
        SpawnAmount = 0;
        InitializeWave();
        UpdateUpcomingWave();
        StartCoroutine(SpawnEnemy());
    }

    public override void UpdateState()
    {
    }

    private void ChooseAllWaves()
    {
        for (int i = 0; i < GameData.Instance.MaxWave; i++)
        {
            chosenWaves.Add(ChooseWave(i));
        }
    }

    private Wave ChooseWave(int ind)
    {
        if (ind == 9) return EnemyFactory.Instance.GetBossWave(0);
        if (ind == 19) return EnemyFactory.Instance.GetBossWave(1);
        if (ind == 29) return EnemyFactory.Instance.GetBossWave(2);
        if (ind < 10) return EnemyFactory.Instance.GetRandomEasyWave();
        if (ind < 20) return EnemyFactory.Instance.GetRandomMediumWave();
        return EnemyFactory.Instance.GetRandomHardWave();
    }

    private void InitializeWave()
    {
        foreach (var entry in chosenWaves[GameData.Instance.CurrentWave].enemies)
        {
            for (int i = 0; i < entry.count; i++)
            {
                waveEnemies.Add(entry.enemyType);
            }
        }
    }

    public IEnumerator SpawnEnemy()
    {
        GameStateManager state = GameStateManager.Instance;
        while (true)
        {
            yield return new WaitForSeconds(1f);

            if (SpawnAmount < waveEnemies.Count)
            {
                EnemyStateManager e = Instantiate(waveEnemies[SpawnAmount], state.Waypoints[0].transform);
                e.SetWaypoints(new List<Transform>(state.Waypoints));
                e.SetIndex(EnemyIndex);
                EnemyIndex++;
                SpawnAmount++;
            }
            else
            {
                state.SwitchState(state.WaveChangeState);
                GameData.Instance.IncrementWave(1);
                this.StopAllCoroutines();
                break;
            }
        }
    }

    private void UpdateUpcomingWave()
    {
        string set = "THIS WAVE: " + '\n';
        foreach (var entry in chosenWaves[GameData.Instance.CurrentWave].enemies)
        {
            set += entry.enemyType.name.ToString() + " : ";
            set += entry.count.ToString();
            set += '\n';
        }
        set += '\n';

        if (GameData.Instance.CurrentWave + 1 != GameData.Instance.MaxWave)
        {
            set += "UPCOMING WAVE: " + '\n';
            foreach (var entry in chosenWaves[GameData.Instance.CurrentWave + 1].enemies)
            {
                set += entry.enemyType.name.ToString() + " : ";
                set += entry.count.ToString();
                set += '\n';
            }
        }

        UIGameOverlay.Instance.UpdateUpcomingWave(set);
    }
}
