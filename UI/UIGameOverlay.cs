using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIGameOverlay : MonoBehaviour
{
    [field: SerializeField] public GameObject components { get; private set; }

    [SerializeField] private Button towerButton;

    private List<Button> currentButtons = new();

    [SerializeField] private Text XPText, healthText, descriptionText, upcomingWaveText;

    public static UIGameOverlay Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void SetXPBar()
    {
        XPText.text = GameData.Instance.XP + " / " + GameData.Instance.NextXPLevel;
    }

    public void SetHealthText()
    {
        healthText.text = GameData.Instance.Health.ToString();
    }

    public void UpdateUpcomingWave(string set)
    {
        upcomingWaveText.text = set;
    }

    public void SetUpOverlay()
    {
        foreach(Button b in currentButtons)
        {
            if (b!=null) Destroy(b.gameObject);
        }

        currentButtons = new();
        int yVal = 200;

        foreach (PlaceableObject t in TowerFactory.Instance.Inventory)
        {
            Button b = Instantiate(towerButton, components.transform);
            b.transform.localPosition = new Vector2(b.transform.position.x - 800, b.transform.position.y + yVal);

            b.GetComponentInChildren<Text>().text = t.Name;

            b.onClick.AddListener(delegate { TowerFactory.Instance.SetSelectedObject(t.Name); });

            b.GetComponent<PointableUIObject>().SetDescription(descriptionText, t.Description);

            currentButtons.Add(b);
            yVal -= 125;
        }

        SetXPBar();
    }
}
