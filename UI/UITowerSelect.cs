using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UITowerSelect : MonoBehaviour
{
    [field: SerializeField] public GameObject components { get; private set; }

    [SerializeField] private Button towerButton;

    [SerializeField] private Text descriptionText;

    private List<Button> currentButtons = new();

    public static UITowerSelect Instance;

    public int selectionNum { get; private set; }

    private void Awake()
    {
        selectionNum = 0;
        Instance = this;
    }

    public void Play()
    {
        if (Time.timeScale != 0) return;
        Time.timeScale = 1;
        components.transform.DOLocalMoveX(600, 0.8f).OnComplete(() => {
            selectionNum = 0;
            ResetButtons();
            components.SetActive(false);
            UIGameOverlay.Instance.SetUpOverlay();
        });
    }

    public void InitializePosition()
    {
        components.transform.DOLocalMoveX(0, 0.6f).OnComplete(() =>
        {
            Time.timeScale = 0;
        });
    }

    public void ResetButtons()
    {
        foreach(Button b in currentButtons)
        {
            if (b != null) Destroy(b.gameObject);
        }
        currentButtons = new();
    }

    private List<int> GenerateRandomInts(int max, int count)
    {
        List<int> randomValues = new();

        for (int i = 0; i < max; i++)
        {
            int numToAdd = Random.Range(0, count);
            while (randomValues.Contains(numToAdd))
            {
                numToAdd = Random.Range(0, count);
            }
            randomValues.Add(numToAdd);
        }

        return randomValues;
    }


    // Tower Select


    public void PullUpTowerSelectMenu(int towers, int comp, int utilities, int numOfSelections)
    {
        components.SetActive(true);
        InitializePosition();
        UpdateSelection(towers, comp, utilities, numOfSelections);
    }

    public void UpdateSelection(int towers, int components, int utilities, int numOfSelections)
    {
        int yVal = 0;
        yVal = LoopSpawnButtons(GenerateRandomInts(towers, TowerFactory.Instance.Towers.Count), TowerFactory.Instance.Towers, yVal, numOfSelections);
        yVal = LoopSpawnButtons(GenerateRandomInts(components, TowerFactory.Instance.Components.Count), TowerFactory.Instance.Components, yVal, numOfSelections);
        LoopSpawnButtons(GenerateRandomInts(utilities, TowerFactory.Instance.Utilities.Count), TowerFactory.Instance.Utilities, yVal, numOfSelections);
    }

    private int LoopSpawnButtons(List<int> indexes, List<PlaceableObject> objects, int yVal, int numOfSelections)
    {
        foreach (int i in indexes)
        {
            SpawnButton(objects[i], yVal, numOfSelections);
            yVal -= 125;
        }
        return yVal;
    }

    private void SpawnButton(PlaceableObject t, int yVal, int numOfSelections)
    {
        Button b = Instantiate(towerButton, components.transform);
        b.transform.localPosition = new Vector2(b.transform.position.x + 650, b.transform.position.y + yVal);

        b.GetComponentInChildren<Text>().text = t.Name;

        b.onClick.AddListener(delegate {
            TowerFactory.Instance.Inventory.Add(t);
            selectionNum++;
            Destroy(b.gameObject);
            if (selectionNum == numOfSelections)
            {
                ResetButtons();
                return;
            }
        });

        b.GetComponent<PointableUIObject>().SetDescription(descriptionText, t.Description);

        currentButtons.Add(b);
    }


    // Map Select

    public void GenerateRandomStartPiece()
    {
        int rand = Random.Range(0, 3);
        List<GameObject> pieces = MapFactory.Instance.MapPiece1;
        if (rand == 1) pieces = MapFactory.Instance.MapPiece2;
        if (rand == 2) pieces = MapFactory.Instance.MapPiece3;
        MapFactory.Instance.CreateMapPiece(pieces[Random.Range(0, pieces.Count)], true);
    }

    public void PullUpMapSelectMenu(int amount, int numOfSelections)
    {
        components.SetActive(true);
        InitializePosition();
        UpdateMapSelection(amount, numOfSelections);
    }

    public void UpdateMapSelection(int amount, int numOfSelections)
    {
        int yVal = 0;
        List<GameObject> pieces = MapFactory.Instance.GetNextPieces();
        LoopSpawnMapButtons(GenerateRandomInts(amount, pieces.Count), pieces, yVal, numOfSelections);
    }

    private int LoopSpawnMapButtons(List<int> indexes, List<GameObject> objects, int yVal, int numOfSelections)
    {
        foreach (int i in indexes)
        {
            SpawnMapButton(objects[i], yVal, numOfSelections);
            yVal -= 125;
        }
        return yVal;
    }

    private void SpawnMapButton(GameObject g, int yVal, int numOfSelections)
    {
        Button b = Instantiate(towerButton, components.transform);
        b.transform.localPosition = new Vector2(b.transform.position.x + 650, b.transform.position.y + yVal);

        b.GetComponentInChildren<Text>().text = g.name;

        b.onClick.AddListener(delegate {
            MapFactory.Instance.CreateMapPiece(g, true);
            b.GetComponent<PointableUIObject>().DestroyGameObjectPreview();
            b.GetComponent<PointableUIObject>().enabled = false;

            selectionNum++;
            Destroy(b.gameObject);
            if (selectionNum == numOfSelections)
            {
                ResetButtons();
                return;
            }
        });

        b.GetComponent<PointableUIObject>().SetDescription(descriptionText, "");
        b.GetComponent<PointableUIObject>().SetGameObjectPreview(g);

        currentButtons.Add(b);
    }
}
