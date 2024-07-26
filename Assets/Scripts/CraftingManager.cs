using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public enum Elements
{
    Fire,
    Water,
    Wind,
    Earth
}
public class CraftingManager : MonoBehaviour
{
    private Dictionary<Elements, int> elements = new Dictionary<Elements, int>();
    public static CraftingManager Instance { get; private set; }
    [SerializeField] List<TextMeshProUGUI> texts = new List<TextMeshProUGUI>();
    [SerializeField] private TextMeshProUGUI voidTxt;
    [SerializeField] private ElementSlot slot1, slot2;
    private int selected = 0;

    
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
        elements.Add(Elements.Fire, 0);
        elements.Add(Elements.Water, 0);
        elements.Add(Elements.Wind, 0);
        elements.Add(Elements.Earth, 0);
        UpdateCraftingUI();
    }

    public int GetElementAmount(Elements el)
    {
        return elements[el];
    }

    public void SetSelected(int i)
    {
        if (i <= 3 && i >= 0)
            selected = i;
        UpdateCraftingUI();
    }
    
    public void AddSelected()
    {
        elements[(Elements)selected] += 1;
        AddElement((Elements)selected);
    }

    public void AddElement(Elements el)
    {
        if (InventoryManager.Instance.GetFragments() > 0)
        {
            elements[el] += 1;
            InventoryManager.Instance.RemoveFragment();
        }
        UpdateCraftingUI();
    }

    public void ForceAddElement(Elements el)
    {
        elements[el] += 1;
        UpdateCraftingUI();
    }
    
    public void RemoveElement(Elements el)
    {
        elements[el] -= 1;
        UpdateCraftingUI();
    }

    void UpdateCraftingUI()
    {
        for (int i = 0; i < 4; i++)
        {
            if (i == selected)
                texts[i].color = Color.black;
            else
                texts[i].color = Color.white;
                
            texts[i].text = elements[(Elements)i].ToString();
        }
        voidTxt.text = InventoryManager.Instance.GetFragments().ToString();
    }

    public void Merge()
    {
        GameObject tmp = slot1.GetCurrentElement();
        if (!tmp)
            return;
        Debug.Log("slot 1 OK");
        Elements el1 = tmp.GetComponent<DragElements>().GetElement();
        tmp = slot2.GetCurrentElement();
        if (!tmp)
            return;
        Debug.Log("slot 2 OK");
        Elements el2 = tmp.GetComponent<DragElements>().GetElement();
        TowerObjects tower = MergeManager.Instance.Merge(el1, el2);
        if (!tower)
            return;
        Debug.Log("ADD Tower");
        InventoryManager.Instance.AddTower(tower);
        slot1.ResetSlot();
        slot2.ResetSlot();
        return;
    }

    // Start is called before the first frame update
    void Start()
    {
        if (texts.Count != 4)
            Debug.LogError("Not Enough TXT");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
