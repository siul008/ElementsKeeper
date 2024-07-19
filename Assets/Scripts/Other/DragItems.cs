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
    [SerializeField] private int moveFrom;

    private void Awake()
    {
        rect = GetComponent<RectTransform>();
        group = GetComponent<CanvasGroup>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        startPos = GetComponent<RectTransform>().anchoredPosition;
    }

    public void OnDrag(PointerEventData eventData)
    {
        rect.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        group.alpha = 0.6f;
        group.blocksRaycasts = false;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        group.alpha = 1f;
        group.blocksRaycasts = true;
        EndMove();
    }

    private void EndMove()
    {
        Debug.Log(endPos);
        if (reset)
            GetComponent<RectTransform>().anchoredPosition = startPos;
        else
            GetComponent<RectTransform>().anchoredPosition = endPos;
        reset = true;
    }

    public void SetEndPos(Vector2 pos, int moveTo)
    {
        endPos = pos;
        reset = false;
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
            if (InventoryManager.Instance.Swap(eventData.pointerDrag.GetComponent<DragItems>().GetMoveFrom(), moveFrom))
            {
                eventData.pointerDrag.SetActive(false);
            }
        }
        //eventData.pointerDrag.GetComponent<DragItems>().SetEndPos(GetComponent<RectTransform>().anchoredPosition, moveFrom);
    }
}
