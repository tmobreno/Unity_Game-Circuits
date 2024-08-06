using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerModifiers : MonoBehaviour
{
    [field: SerializeField] public float DamageMod { get; private set; }
    [field: SerializeField] public float RangeMod { get; private set; }
    [field: SerializeField] public float SpeedMod { get; private set; }

    public static TowerModifiers Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        DamageMod = 0;
        RangeMod = 0;
        SpeedMod = 1;
    }

    public void SetDamageMod(float set)
    {
        DamageMod += set;
    }
    public void SetRangeMod(float set)
    {
        RangeMod += set;
    }
    public void SetSpeedMod(float set)
    {
        SpeedMod += set;
    }

}
