using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITooltip : MonoBehaviour
{
    [field: SerializeField] public GameObject components { get; private set; }

    [field: SerializeField] public Text tooltipText { get; private set; }

    public static UITooltip Instance;

    private void Awake()
    {
        Instance = this;
        HideTooltip();
    }

    private void Start()
    {
        components.GetComponent<RectTransform>().pivot = new Vector2(0, 0);
    }

    public void ShowTooltip(string message, int offsetX, int offsetY)
    {
        tooltipText.text = message;
        components.SetActive(true);
        UpdateTooltipPosition(offsetX, offsetY);
    }

    public void HideTooltip()
    {
        components.SetActive(false);
    }

    public bool IsShowingTip()
    {
        if (components.activeSelf) return true;
        return false;
    }

    private void UpdateTooltipPosition(int offetX, int offsetY)
    {
        Vector2 mousePos = Input.mousePosition;
        mousePos.x += offetX;
        mousePos.y += offsetY;
        RectTransform tooltipRect = components.GetComponent<RectTransform>();
        tooltipRect.position = mousePos;
    }
}
