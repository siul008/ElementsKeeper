using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
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
    private Dictionary<Elements, bool> elements = new Dictionary<Elements, bool>();
    public static CraftingManager Instance { get; private set; }
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
    [Header("Unlock Elements")] 
    [SerializeField] private int increasePrice = 5;
    private int currentPrice = 0;
    private Dictionary<Elements, bool> unlockedElements = new Dictionary<Elements, bool>();
    [SerializeField] private List<DragElements> dragElements = new List<DragElements>();
    [SerializeField] private List<Image> elementsImg = new List<Image>();
    [SerializeField] private Button transmuteBtn;
    [Header("Slider")] 
    [SerializeField] private Image slider1;
    [SerializeField] private Image slider2;
    [SerializeField] private Animator canvasAnim;
    [SerializeField] private Button mergeBtn;

    private TowerObjects currentMerge;
    private static readonly int Merge1 = Animator.StringToHash("Merge");


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
        if (dragElements.Count != 4)
            Debug.LogError("There isn't 4 DragElements");
        if (elementsImg.Count != 4)
            Debug.LogError("There isn't 4 Elements Img");
        elements.Add(Elements.Fire, false);
        elements.Add(Elements.Water, false);
        elements.Add(Elements.Wind, false);
        elements.Add(Elements.Earth, false);
        unlockedElements.Add(Elements.Fire, true);
        unlockedElements.Add(Elements.Water, false);
        unlockedElements.Add(Elements.Wind, false);
        unlockedElements.Add(Elements.Earth, false);
        UpdateCraftingUI();
    }

    public bool GetElementUnlocked(Elements el)
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
        //voidTxt.text = InventoryManager.Instance.GetFragments().ToString();
    }
    
    public void AddSelected()
    {
        if (elements[(Elements)selected])
            return;
        if (unlockedElements[(Elements)selected] && InventoryManager.Instance.GetFragments() >= currentPrice)
        {
            InventoryManager.Instance.RemoveFragment(currentPrice);
            currentPrice += increasePrice;
            dragElements[selected].Unlocked();
            elements[(Elements)selected] = true;
            UpdateCraftingUI();
        }
    }

    public void UnlockElement(Elements el)
    {
        unlockedElements[el] = true;
    }

    public void UpdateSlider(int i, Color c)
    {
        if (i == 1)
            slider1.color = c;
        else if (i == 2)
            slider2.color = c;
    }

    public void AddElement(Elements el)
    {
        if (InventoryManager.Instance.GetFragments() > 0)
        {
           // elements[el] += 1;
           //InventoryManager.Instance.RemoveFragment();
        }
        UpdateCraftingUI();
    }

    public void ForceAddElement(Elements el)
    {
        //elements[el] += 1;
        UpdateCraftingUI();
    }
    
    public void RemoveElement(Elements el)
    {
        //elements[el] -= 1;
        UpdateCraftingUI();
    }

    public void UpdateCraftingUI()
    {
        if (currentPrice == 0)
        {
            voidTxt.text = "Free";
        }
        else
        {
            voidTxt.text = currentPrice.ToString();
        }
        if (!elements[(Elements)selected] && unlockedElements[(Elements)selected])
            transmuteBtn.interactable = true;
        else
            transmuteBtn.interactable = false;
        
        for (int i = 0; i < 4; i++)
        {
            if (elements[(Elements)i])
                elementsImg[i].color = new Color(255, 255, 255, 0.6f);
            else if (unlockedElements[(Elements)i])
                elementsImg[i].color = new Color(255, 255, 255, 0.35f);
            else
                elementsImg[i].color = new Color(255, 255, 255, 0f);
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
            towerPrice.color = Color.white;
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
        Debug.Log("slot 1 = " + tmp);
        Elements el1 = tmp.GetComponent<DragElements>().GetElement();
        tmp = slot2.GetCurrentElement();
        if (!tmp)
            return null;
        Debug.Log("slot 2 = " + tmp);
        Elements el2 = tmp.GetComponent<DragElements>().GetElement();
        TowerObjects tower = MergeManager.Instance.Merge(el1, el2);
        if (!tower)
            return null;
        return tower;
    }

    public void Merge()
    {
        SoundManager.Instance.PlayUISound();
        InventoryManager.Instance.AddTower(currentMerge);
        InventoryManager.Instance.RemoveFragment(currentMerge.price);
        UpdateFragmentText();
        slot1.ResetSlot();
        slot2.ResetSlot();
        mergeBtn.interactable = true;
    }
    
    public void TriggerMerge()
    {
        currentMerge = GetCurrentMerge();
        if (currentMerge && currentMerge.price <= InventoryManager.Instance.GetFragments() && !InventoryManager.Instance.isFull())
        {
            mergeBtn.interactable = false;
            canvasAnim.SetTrigger("Merge");
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
        if (elements[element.GetComponent<DragElements>().GetElement()] == false)
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
