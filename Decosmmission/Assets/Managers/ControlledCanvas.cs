using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlledCanvas : MonoBehaviour
{
    public CanvasType type;

    public void SetActive(bool state)
    {
        gameObject.SetActive(state);
    }

    public void SetInteractable(bool state)
    {
        GetComponent<CanvasGroup>().interactable = state;
    }

    public void SetSortingOrder(int order)
    {
        GetComponent<Canvas>().sortingOrder = order;
    }

    public int GetSortingOrder() => GetComponent<Canvas>().sortingOrder;
}
