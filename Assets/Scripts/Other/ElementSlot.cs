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
        
        if (eventData.pointerDrag != null && eventData.pointerDrag.GetComponent<DragElements>() && CraftingManager.Instance.GetElementUnlocked(eventData.pointerDrag.GetComponent<DragElements>().GetElement()))
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

    public void SetCurrentElement(GameObject newElement)
    {
        currentElement = newElement;
    }

    public void ResetSlot()
    {
        Destroy(currentElement);
        CraftingManager.Instance.ResetCurrentTower();
    }

    public bool AutoAddElement(GameObject el)
    {
        if (!currentElement)
        {
            currentElement = Instantiate(el, canvas);
            CraftingManager.Instance.RemoveElement(currentElement.GetComponent<DragElements>().GetElement());
            CraftingManager.Instance.SetCurrentTower();
            currentElement.GetComponent<DragElements>().Duplicate();
            currentElement.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
            SoundManager.Instance.PlayUISound();
            return true;
        }
        return false;
    }
}
