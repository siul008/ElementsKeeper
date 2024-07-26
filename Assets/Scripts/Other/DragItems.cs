using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragItems : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler, IDropHandler
{
    private RectTransform rect;
    private CanvasGroup group;
    private Vector2 startPos;
    private Vector2 endPos;
    private bool reset = true;
    [SerializeField] private Canvas canvas;
    public int moveFrom;

    private void Awake()
    {
        rect = GetComponent<RectTransform>();
        group = GetComponent<CanvasGroup>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        startPos = GetComponent<RectTransform>().anchoredPosition;
        InventoryManager.Instance.SetSelected(moveFrom);
    }

    public void OnDrag(PointerEventData eventData)
    {
        rect.anchoredPosition += eventData.delta / (canvas.scaleFactor * transform.parent.GetComponent<RectTransform>().localScale);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        group.alpha = 0.6f;
        group.blocksRaycasts = false;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        EndMove();
    }

    private void EndMove()
    {
        group.alpha = 1f;
        group.blocksRaycasts = true;
        if (reset)
            GetComponent<RectTransform>().anchoredPosition = startPos;
        else
            GetComponent<RectTransform>().anchoredPosition = endPos;
        reset = true;
    }

    public void TrashTower()
    {
        InventoryManager.Instance.RemoveTower(moveFrom);
        EndMove();
    }

    public void SetEndPos(Vector2 pos, int moveTo)
    {
        if (moveFrom == moveTo)
            return;
        Debug.Log("set end move");
        reset = false;
        endPos = pos;
        //Debug.Log(transform.parent.GetChild(6 + moveTo).GetComponent<RectTransform>().anchoredPosition + "should move to :" + startPos);
        InventoryManager.Instance.images[moveTo].GetComponent<RectTransform>().anchoredPosition = startPos;
        //Debug.Log(transform.parent.GetChild(6 + moveTo).GetComponent<RectTransform>().anchoredPosition);
        InventoryManager.Instance.images[moveTo].GetComponent<DragItems>().moveFrom = moveFrom;
        InventoryManager.Instance.Swap(moveFrom, moveTo);
        moveFrom = moveTo;
    }

    int GetMoveFrom()
    {
        return moveFrom;
    }
    
    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            if (InventoryManager.Instance.Merge(eventData.pointerDrag.GetComponent<DragItems>().GetMoveFrom(), moveFrom))
            {
                eventData.pointerDrag.GetComponent<DragItems>().EndMove();
                eventData.pointerDrag.SetActive(false);
            }
        }
        //eventData.pointerDrag.GetComponent<DragItems>().SetEndPos(GetComponent<RectTransform>().anchoredPosition, moveFrom);
    }
}
