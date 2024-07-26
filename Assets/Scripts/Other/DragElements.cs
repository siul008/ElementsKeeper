using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragElements : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler, IDropHandler
{
    private RectTransform rect;
    private CanvasGroup group;
    private Vector2 pos;
    private Vector2 startPos;
    public bool atStart = true;
    private bool reset = true;
    private bool called = false;
    private bool isOriginal = true;
    [SerializeField] private Canvas canvas;
    [SerializeField] private Elements element;

    private void Awake()
    {
        rect = GetComponent<RectTransform>();
        group = GetComponent<CanvasGroup>();
        startPos = rect.anchoredPosition;
    }

    public Elements GetElement()
    {
        return element;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        pos = rect.anchoredPosition;
        CraftingManager.Instance.SetSelected((int) element);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (CraftingManager.Instance.GetElementAmount(element) <= 0 && isOriginal)
            return;
        rect.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (CraftingManager.Instance.GetElementAmount(element) <= 0 && isOriginal)
            return;
        group.alpha = 0.6f;
        group.blocksRaycasts = false;
    }

    void EndMove()
    {
        if (!isOriginal)
        {
            Debug.Log("Add Element");
            CraftingManager.Instance.ForceAddElement(element);
            Destroy(gameObject);
        }
        group.alpha = 1f;
        group.blocksRaycasts = true;
        rect.anchoredPosition = pos;
    }

    public void Duplicate()
    {
        isOriginal = false;
        group.alpha = 1f;
        group.blocksRaycasts = true;
    }
    
    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("EndDrag");
        EndMove();
    }
    
    public void OnDrop(PointerEventData eventData)
    {

    }
}
