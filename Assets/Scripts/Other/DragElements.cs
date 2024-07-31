using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

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
    private bool unlocked = false;
    [SerializeField] private List<Elements> unlockedElements = new List<Elements>();
    [SerializeField] private Color color;
    private ElementSlot slot = null;

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

    public void Unlocked()
    {
        unlocked = true;
        GetComponent<Image>().color = Color.white;
        foreach (var el in unlockedElements)
        {
            CraftingManager.Instance.UnlockElement(el);
        }
    }

    public Color GetColor()
    {
        return color;
    }
    
    public void OnPointerDown(PointerEventData eventData)
    {
        pos = rect.anchoredPosition;
        CraftingManager.Instance.SetSelected((int) element);
        if (Input.GetKey(KeyCode.LeftShift))
        {
            CraftingManager.Instance.AutoAddElement(gameObject);
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!unlocked && isOriginal)
            return;
        rect.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void SetSlot(ElementSlot elementSlot)
    {
        slot = elementSlot;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!unlocked && isOriginal)
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
            if (slot)
                slot.ResetSlot();
            //CraftingManager.Instance.SetCurrentTower();
            //Destroy(gameObject);
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
        if (isOriginal)
            return;
        if (eventData.pointerDrag != null && eventData.pointerDrag.GetComponent<DragElements>() && CraftingManager.Instance.GetElementUnlocked(eventData.pointerDrag.GetComponent<DragElements>().GetElement()))
        {
            GameObject currentElement = Instantiate(eventData.pointerDrag, transform.parent);
            Debug.Log("Remove Element");
            CraftingManager.Instance.RemoveElement(currentElement.GetComponent<DragElements>().GetElement());
            CraftingManager.Instance.ForceAddElement(GetComponent<DragElements>().GetElement());
            CraftingManager.Instance.SetCurrentTower();
            currentElement.GetComponent<DragElements>().Duplicate();
            currentElement.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
            SoundManager.Instance.PlayUISound();
            CraftingManager.Instance.UpdateSlotElement(currentElement);
            Destroy(gameObject);
        }
    }
}
