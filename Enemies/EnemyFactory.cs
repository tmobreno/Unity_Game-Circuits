using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFactory : MonoBehaviour
{
    public static EnemyFactory Instance { get; private set; }

    [field: SerializeField] public List<Wave> EasyWaves { get; private set; }

    [field: SerializeField] public List<Wave> MediumWaves { get; private set; }

    [field: SerializeField] public List<Wave> HardWaves { get; private set; }

    [field: SerializeField] public List<Wave> BossWaves { get; private set; }

    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
    }

    public Wave GetBossWave(int ind)
    {
        return BossWaves[ind];
    }

    public Wave GetRandomEasyWave()
    {
        return GetWave(EasyWaves);
    }

    public Wave GetRandomMediumWave()
    {
        return GetWave(MediumWaves);
    }

    public Wave GetRandomHardWave()
    {
        return GetWave(HardWaves);
    }

    private Wave GetWave(List<Wave> wave)
    {
        int i = Random.Range(0, wave.Count);
        return wave[i];
    }
}
