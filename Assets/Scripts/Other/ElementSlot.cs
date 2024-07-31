using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class ElementSlot : MonoBehaviour, IDropHandler
{
    [SerializeField] private Transform canvas;
    [SerializeField] private int index = 1;
    private GameObject currentElement;
    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null && eventData.pointerDrag.GetComponent<DragElements>() && CraftingManager.Instance.GetElementUnlocked(eventData.pointerDrag.GetComponent<DragElements>().GetElement()))
        {
            Debug.Log("Start Coroutine");
            GameObject tmp = Instantiate(eventData.pointerDrag, canvas);
            StartCoroutine(DropElements(tmp));
        }
    }

    IEnumerator DropElements(GameObject el)
    {
        yield return new WaitForSecondsRealtime(0.01f);
        Debug.Log("Remove Element");
        currentElement = el;
        currentElement.GetComponent<DragElements>().SetSlot(this);
        CraftingManager.Instance.RemoveElement(currentElement.GetComponent<DragElements>().GetElement());
        CraftingManager.Instance.UpdateSlider(index, currentElement.GetComponent<DragElements>().GetColor());
        currentElement.GetComponent<DragElements>().Duplicate();
        currentElement.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
        SoundManager.Instance.PlayUISound();
        CraftingManager.Instance.SetCurrentTower();
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
        Debug.Log("before merge");
        CraftingManager.Instance.ResetCurrentTower();
    }

    public bool AutoAddElement(GameObject el)
    {
        if (!currentElement)
        {
            currentElement = Instantiate(el, canvas);
            currentElement.GetComponent<DragElements>().SetSlot(this);
            CraftingManager.Instance.RemoveElement(currentElement.GetComponent<DragElements>().GetElement());
            CraftingManager.Instance.UpdateSlider(index, currentElement.GetComponent<DragElements>().GetColor());
            currentElement.GetComponent<DragElements>().Duplicate();
            currentElement.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
            SoundManager.Instance.PlayUISound();
            CraftingManager.Instance.SetCurrentTower();
            return true;
        }
        return false;
    }
}
