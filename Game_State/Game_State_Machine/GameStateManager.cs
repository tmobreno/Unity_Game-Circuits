using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;

public class GameStateManager : MonoBehaviour
{
    [field: SerializeField] public GameBaseState currentState;

    [HideInInspector] public GameWaveChangeState WaveChangeState;
    [HideInInspector] public GameSpawningState SpawningState;
    [HideInInspector] public GameTowerSelectionState TowerSelectState;

    [field: SerializeField] public List<Transform> Waypoints { get; private set; }

    public static GameStateManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        WaveChangeState = gameObject.AddComponent<GameWaveChangeState>();
        SpawningState = gameObject.AddComponent<GameSpawningState>();
        TowerSelectState = gameObject.AddComponent<GameTowerSelectionState>();

        currentState = TowerSelectState;
        currentState.EnterState();
    }

    // Update is called once per frame
    void Update()
    {
        currentState.UpdateState();
    }

    public void SwitchState(GameBaseState state)
    {
        currentState = state;
        currentState.EnterState();
    }

    public void PrependWaypoints(List<Transform> waypoints)
    {
        Waypoints.InsertRange(0, waypoints);
    }
}
