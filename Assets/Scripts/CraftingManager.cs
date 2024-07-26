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
    [SerializeField] List<TextMeshProUGUI> texts = new List<TextMeshProUGUI>(4);
    [SerializeField] private TextMeshProUGUI voidTxt;
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

    public void AddElement(Elements el)
    {
        if (InventoryManager.Instance.GetFragments() > 0)
        {
            elements[el] += 1;
            InventoryManager.Instance.RemoveFragment();
        }
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
