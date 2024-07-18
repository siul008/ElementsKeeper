using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class ItemSlot : MonoBehaviour, IDropHandler
{
    [SerializeField] private int index; 
    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
            eventData.pointerDrag.GetComponent<DragItems>().SetEndPos(GetComponent<RectTransform>().anchoredPosition, index);
    }
}
