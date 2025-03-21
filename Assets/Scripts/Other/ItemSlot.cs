using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class ItemSlot : MonoBehaviour, IDropHandler, IPointerDownHandler
{
    [SerializeField] private int index;
    [SerializeField] private bool isTrash;
    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            if (isTrash)
            {
                eventData.pointerDrag.GetComponent<DragItems>().TrashTower();
            }
            else
            {
                eventData.pointerDrag.GetComponent<DragItems>()
                    .SetEndPos(GetComponent<RectTransform>().anchoredPosition, index);
                InventoryManager.Instance.SetSelected(index);
            }
        }
    }
    
    public void OnPointerDown(PointerEventData eventData)
    {
        InventoryManager.Instance.SetSelected(index);
    }
}
