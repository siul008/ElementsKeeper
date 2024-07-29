using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class ElementSlot : MonoBehaviour, IDropHandler
{
    [SerializeField] private Transform canvas;
    private GameObject currentElement;
    public void OnDrop(PointerEventData eventData)
    {
        
        if (eventData.pointerDrag != null && eventData.pointerDrag.GetComponent<DragElements>() && CraftingManager.Instance.GetElementAmount(eventData.pointerDrag.GetComponent<DragElements>().GetElement()) > 0)
        {
            currentElement = Instantiate(eventData.pointerDrag, canvas);
            Debug.Log("Remove Element");
            CraftingManager.Instance.RemoveElement(currentElement.GetComponent<DragElements>().GetElement());
            CraftingManager.Instance.SetCurrentTower();
            currentElement.GetComponent<DragElements>().Duplicate();
            currentElement.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
            SoundManager.Instance.PlayUISound();
        }
    }

    public GameObject GetCurrentElement()
    {
        return currentElement;
    }

    public void ResetSlot()
    {
        Destroy(currentElement);
        CraftingManager.Instance.ResetCurrentTower();
    }
}
