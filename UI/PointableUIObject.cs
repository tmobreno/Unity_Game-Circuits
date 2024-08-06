using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PointableUIObject : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Text descriptionText;

    private string descriptionString;

    private bool mouse_over = false;

    private GameObject storedObject;

    private GameObject instantiatedObject;

    void Update()
    {
        if (mouse_over)
        {
            if (!descriptionText.gameObject.activeSelf && storedObject != null)
            {
                instantiatedObject = MapFactory.Instance.CreateMapPiece(storedObject, false);
            }
            if (!descriptionText.gameObject.activeSelf)
            {
                descriptionText.text = descriptionString;
                descriptionText.gameObject.SetActive(true);
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        mouse_over = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        mouse_over = false;
        if (descriptionText.gameObject.activeSelf && storedObject != null) Destroy(instantiatedObject.gameObject);
        if (descriptionText.gameObject.activeSelf) descriptionText.gameObject.SetActive(false);
    }

    public void SetDescription(Text descriptionSet, string stringSet)
    {
        descriptionText = descriptionSet;
        descriptionString = stringSet;
        descriptionText.gameObject.SetActive(false);
    }

    public void SetGameObjectPreview(GameObject set)
    {
        storedObject = set;
    }

    public void DestroyGameObjectPreview()
    {
        if (instantiatedObject != null && descriptionText.gameObject.activeSelf && storedObject != null) Destroy(instantiatedObject.gameObject);
    }
}
