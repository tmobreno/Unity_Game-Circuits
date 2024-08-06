using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour
{
    public static GameData Instance { get; private set; }

    [field: Header("Player Stored Data")]
    [field: SerializeField] public int Health { get; private set; }
    [field: SerializeField] public float XP { get; private set; }
    [field: SerializeField] public float NextXPLevel { get; private set; }
    [field: SerializeField] public float Level { get; private set; }

    [field: Header("Game Stored Data")]
    [field: SerializeField] public int CurrentWave { get; private set; }
    [field: SerializeField] public int MaxWave { get; private set; }

    [field: Header("Controls")]
    [field: SerializeField] public KeyCode PlaceTower { get; private set; } = KeyCode.Mouse0;
    [field: SerializeField] public KeyCode RemoveTower { get; private set; } = KeyCode.Mouse1;
    [field: SerializeField] public KeyCode DragCamera { get; private set; } = KeyCode.Mouse1;
    [field: SerializeField] public KeyCode ResetCamera { get; private set; } = KeyCode.Mouse2;

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        Level = 1;
        XP = 0;
        NextXPLevel = 500;
        CurrentWave = 0;
        MaxWave = 30;
        Time.timeScale = 1;
        UIGameOverlay.Instance.SetHealthText();
    }

    public void IncrementWave(int set)
    {
        CurrentWave += set;
    }

    // Temporary game loss state, change later

    public void TakeDamage(int loss)
    {
        Health -= loss;
        if (Health <=0)
        {
            UIGameOver.Instance.SetVisible();
        }
        UIGameOverlay.Instance.SetHealthText();
    }

    public void GainHealth(int gain)
    {
        if (Health <= 0) return;
        Health += gain;
        if (Health > 15) Health = 15;
        UIGameOverlay.Instance.SetHealthText();
    }

    public void AddXP(float add)
    {
        XP += add;
        CheckNextXPLevel();
    }

    private void CheckNextXPLevel()
    {
        if (XP >= NextXPLevel)
        {
            XP = 0;
            NextXPLevel *= 1.75f;
            NextXPLevel = ((int)(NextXPLevel / 5)) * 5;
            Level++;

            if (Level % 3 == 0 && Level < 10)
            {
                UITowerSelect.Instance.PullUpMapSelectMenu(2, 1);
            }
            else if ((Level % 3) - 1 == 0)
            {
                RandomizeTwoTowerSelection();
            }
            else
            {
                RandomizeOneTowerSelection();
            }
        }
        UIGameOverlay.Instance.SetXPBar();
    }

    private void RandomizeOneTowerSelection()
    {
        int i = Random.Range(0, 5);
        if (i == 0) UITowerSelect.Instance.PullUpTowerSelectMenu(1, 1, 1, 1);
        if (i == 1) UITowerSelect.Instance.PullUpTowerSelectMenu(2, 1, 0, 1);
        if (i == 2) UITowerSelect.Instance.PullUpTowerSelectMenu(2, 0, 1, 1);
        if (i == 3) UITowerSelect.Instance.PullUpTowerSelectMenu(1, 2, 0, 1);
        if (i == 4) UITowerSelect.Instance.PullUpTowerSelectMenu(1, 0, 2, 1);

    }

    private void RandomizeTwoTowerSelection()
    {
        int i = Random.Range(0, 3);
        if (i == 0) UITowerSelect.Instance.PullUpTowerSelectMenu(2, 1, 1, 2);
        if (i == 1) UITowerSelect.Instance.PullUpTowerSelectMenu(2, 2, 0, 2);
        if (i == 2) UITowerSelect.Instance.PullUpTowerSelectMenu(2, 0, 2, 2);
    }
}
