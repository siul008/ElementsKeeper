using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class ElementSlot : MonoBehaviour, IDropHandler, IPointerDownHandler
{
    [SerializeField] private int index;
    public void OnDrop(PointerEventData eventData)
    {
        
        if (eventData.pointerDrag != null && eventData.pointerDrag.GetComponent<DragElements>())
        {
            eventData.pointerDrag.GetComponent<DragElements>()
                .SetEndPos(GetComponent<RectTransform>().anchoredPosition, false);
        }
    }
    
    public void OnPointerDown(PointerEventData eventData)
    {
        InventoryManager.Instance.SetSelected(index);
    }
}
