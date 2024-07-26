using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class ElementSlot : MonoBehaviour, IDropHandler, IPointerDownHandler
{
    [SerializeField] private int index;
    [SerializeField] private Transform canvas;
    private GameObject currentElement;
    public void OnDrop(PointerEventData eventData)
    {
        
        if (eventData.pointerDrag != null && eventData.pointerDrag.GetComponent<DragElements>() && CraftingManager.Instance.GetElementAmount(eventData.pointerDrag.GetComponent<DragElements>().GetElement()) > 0)
        {
            currentElement = Instantiate(eventData.pointerDrag, canvas);
            Debug.Log("Remove Element");
            CraftingManager.Instance.RemoveElement(currentElement.GetComponent<DragElements>().GetElement());
            currentElement.GetComponent<DragElements>().Duplicate();
            currentElement.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
        }
    }

    public GameObject GetCurrentElement()
    {
        return currentElement;
    }

    public void ResetSlot()
    {
        Destroy(currentElement);
    }
    
    public void OnPointerDown(PointerEventData eventData)
    {
        InventoryManager.Instance.SetSelected(index);
    }
}
