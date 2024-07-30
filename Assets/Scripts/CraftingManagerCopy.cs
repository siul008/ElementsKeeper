using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum ElementsCopy
{
    Fire,
    Water,
    Wind,
    Earth
}

public class CraftingManagerCopy : MonoBehaviour
{
    private Dictionary<Elements, int> elements = new Dictionary<Elements, int>();
    public static CraftingManagerCopy Instance { get; private set; }
    [SerializeField] List<TextMeshProUGUI> texts = new List<TextMeshProUGUI>();
    [SerializeField] private TextMeshProUGUI voidTxt;
    [SerializeField] private ElementSlot slot1, slot2;
    [SerializeField] private TextMeshProUGUI mergeButtonText;
    [SerializeField] private TextMeshProUGUI transmuteButtonText;
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
    [SerializeField]
    [ItemCanBeNull]
    private List<GameObject> selector;
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
        if (selector.Count != 4)
            Debug.LogError("There isn't 4 selectors");
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
        {
            selector[i]?.SetActive(false);
            selected = i;
        }

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
            SoundManager.Instance.PlayUISound();
        }
        else
        {
            SoundManager.Instance.PlayUISoundDisabled();
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
            texts[i].color = Color.black;
            if (i == selected)
            {
                selector[i]?.SetActive(true);
            }
            else
            {
                selector[i]?.SetActive(false);
            }
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
        Color mergeColor = mergeButtonText.color; ;
        if (currentTower.price > InventoryManager.Instance.GetFragments())
        {
            mergeColor.a = 0.6f;
            towerPrice.color = Color.red;
        }
        else
        {
            mergeColor.a = 1f;
            towerPrice.color = Color.black;
        }
        mergeButtonText.color = mergeColor;
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
        if (tower && tower.price <= InventoryManager.Instance.GetFragments())
        {
            SoundManager.Instance.PlayUISound();
            InventoryManager.Instance.AddTower(tower);
            for (int i = 0; i < tower.price; i++)
            {
                InventoryManager.Instance.RemoveFragment();
            }
            UpdateFragmentText();
            slot1.ResetSlot();
            slot2.ResetSlot();
        }
        else
        {
            SoundManager.Instance.PlayUISoundDisabled();
        }
        return;
    }

    public void UpdateSlotElement(GameObject el)
    {
        if (el.transform.position == slot1.transform.position)
        {
            slot1.SetCurrentElement(el);
            SetCurrentTower();
        }
        else if (el.transform.position == slot2.transform.position)
        {
            slot2.SetCurrentElement(el);
            SetCurrentTower();
        }
    }

    public void AutoAddElement(GameObject element)
    {
        if (elements[element.GetComponent<DragElements>().GetElement()] <= 0)
            return;
        if (slot2.AutoAddElement(element))
            return;
        slot1.AutoAddElement(element);
    }

    // Start is called before the first frame update
    void Start()
    {
        if (texts.Count != 4)
            Debug.LogError("Not Enough TXT");
    }

}
