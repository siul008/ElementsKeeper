using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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
    private TowerObjects currentTower;
    [SerializeField] private Image towerImg;
    [Header("Tower Informations ")]
    [SerializeField] private GameObject towerInfos;
    [SerializeField] private TextMeshProUGUI towerDes;
    [SerializeField] private TextMeshProUGUI towerName;
    [SerializeField] private TextMeshProUGUI towerDmgVal;
    [SerializeField] private TextMeshProUGUI towerASVal;
    [SerializeField] private TextMeshProUGUI towerTypeVal;
    [SerializeField] private TextMeshProUGUI towerPrice;
    [Header("Tower Selected")]
    [SerializeField] private GameObject fireSelector;
    [SerializeField] private GameObject waterSelector;
    [SerializeField] private GameObject windSelector;
    [SerializeField] private GameObject earthSelector;
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

    public void UpdateFragmentText()
    {
        voidTxt.text = InventoryManager.Instance.GetFragments().ToString();
    }
    
    public void AddSelected()
    {
        if (InventoryManager.Instance.GetFragments() >= 1)
        {
            elements[(Elements)selected] += 1;
            AddElement((Elements)selected);

        }
    }

    public void AddElement(Elements el)
    {
        if (InventoryManager.Instance.GetFragments() > 0)
        {
           // elements[el] += 1;
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
        UpdateFragmentText();
    }

    public TowerObjects GetCurrentTower()
    {
        return currentTower;
    }

    public void ResetCurrentTower()
    {
        currentTower = null;
        towerImg.color = new Color(255, 255, 255, 0);
        towerInfos.SetActive(false);
    }

    void GetTowerInfosText()
    {
        towerDes.text = currentTower.description;
        towerPrice.text = currentTower.price.ToString();
        towerName.text = currentTower.towerName.ToUpper();
        towerDmgVal.text = currentTower.damage.ToString();
        towerASVal.text = currentTower.attackRate > 0 ? currentTower.attackRate.ToString() : "None";
        switch (currentTower.dmgType)
        {
            case TowerObjects.DmgType.Single:
                {
                    towerTypeVal.text = "Single Target";
                    break;
                }
            case TowerObjects.DmgType.AreaOfEffect:
                {
                    towerTypeVal.text = "Area of effect";
                    break;
                }
            case TowerObjects.DmgType.None:
                {
                    towerTypeVal.text = "None";
                    break;
                }
        }
        towerInfos.gameObject.SetActive(true);
    }

    public void SetCurrentTower()
    {
        ResetCurrentTower();
        currentTower = GetCurrentMerge();
        if (!currentTower)
            return;
        towerImg.sprite = currentTower.sprite;
        towerImg.color = new Color(255, 255, 255, 0.8f);
        GetTowerInfosText();
    }

    TowerObjects GetCurrentMerge()
    {
        GameObject tmp = slot1.GetCurrentElement();
        if (!tmp)
            return null;
        Debug.Log("slot 1 OK");
        Elements el1 = tmp.GetComponent<DragElements>().GetElement();
        tmp = slot2.GetCurrentElement();
        if (!tmp)
            return null;
        Debug.Log("slot 2 OK");
        Elements el2 = tmp.GetComponent<DragElements>().GetElement();
        TowerObjects tower = MergeManager.Instance.Merge(el1, el2);
        if (!tower)
            return null;
        return tower;
    }
    
    public void Merge()
    {
        TowerObjects tower = GetCurrentMerge();
        Debug.Log("ADD Tower");
        if (tower.price <= InventoryManager.Instance.GetFragments())
        {
            InventoryManager.Instance.AddTower(tower);
            for (int i = 0; i < tower.price; i++)
            {
                InventoryManager.Instance.RemoveFragment();
            }
            UpdateFragmentText();
            slot1.ResetSlot();
            slot2.ResetSlot();
        }
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
