using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
using Random = UnityEngine.Random;

public class InventoryManager : MonoBehaviour
{
    public List<TowerObjects> inv = new List<TowerObjects>();
    public List<Transform> images = new List<Transform>();
    [SerializeField] TowerObjects[] voidFragments = new TowerObjects[4];
    [SerializeField] private Transform imagesParent;
    public TowerObjects test;
    private int currentFragments = 0;
    [SerializeField] private int minFragments = 10;
    [SerializeField] private TextMeshProUGUI fragmentText;
    int selected = 0;
    public static InventoryManager Instance { get; private set; }

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

        for (int i = 0; i < 6; i++)
        {
            inv.Add(null);
        }
        for (int i = 0; i < 6; i++)
        {
            images.Add(imagesParent.GetChild(6 + i));
        }
    }
    
    void Start()
    {
        inv[0] = test;
        UpdateInventoryUI();

    }
    void Update()
    {            
        UpdateInventoryUI();
        if(Input.GetKeyDown(KeyCode.Y))
            AddFragment();
        if (Input.GetKeyDown(KeyCode.U))
            GenerateTower();
        if (Input.GetKeyDown(KeyCode.E))
        {
            selected++;
            if (selected >= 6)
                selected = 0;
            UpdateInventoryUI();
        }
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            selected--;
            if (selected < 0)
                selected = inv.Count - 1;
            UpdateInventoryUI();
        }
    }

    void UpdateInventoryUI()
    {
        fragmentText.text = GetFragments().ToString();
        int i;
        for (i = 0; i < 6; i++)
        {
            Image child = imagesParent.GetChild(i).GetComponent<Image>();
            if (i == selected)
                child.color = new Color(child.color.r, child.color.g, child.color.b, 0.9f);
            else
                child.color = new Color(child.color.r, child.color.g, child.color.b, 0.2f);
            if (inv[i] == null)
            {
                images[i].gameObject.SetActive(false);
                continue;
            }
            Image tower = images[i].GetComponent<Image>();
            tower.gameObject.SetActive(true);
            tower.sprite = inv[i].sprite;
        }
    }

    void AddTower(TowerObjects obj)
    {
        for (int i = 0; i < 6; i++)
        {
            if (inv[i] == null)
            {
                inv[i] = obj;
                break;
            }
        }
        UpdateInventoryUI();
    }

    public bool Merge(int index1, int index2)
    {
        try
        {
            TowerObjects newTower = MergeManager.Instance.Merge(inv[index1], inv[index2]);
            if (newTower == null)
                return false;
            Debug.Log("set " + index1 + "to null and " + index2 + " to " + newTower);
            inv[index1] = null;
            inv[index2] = newTower;
            UpdateInventoryUI();
            return true;
        }
        catch (Exception e)
        {
            return false;
        }
    }

    public bool Swap(int index1, int index2)
    {
        if (inv[index1] != null && inv[index2] != null)
            return (Merge(index1, index2));
        else
        {
            (inv[index1], inv[index2]) = (inv[index2], inv[index1]);
            (images[index1], images[index2]) = (images[index2], images[index1]);
        }
        return true;
    }

    public void GenerateTower()
    {
        if (currentFragments < minFragments)
            return;
        
        for (int i = 0; i < 6; i++)
        {
            if (inv[i] == null)
            {
                int rand = Random.Range(0, voidFragments.Length);
                Debug.Log(voidFragments[rand]);
                inv[i] = voidFragments[rand];
                currentFragments -= minFragments;
                UpdateInventoryUI();
                return;
            }
        }
    }

    public TowerObjects GetSelectedTower()
    {
        TowerObjects t = inv[selected];
        if (t != null)
        {
            inv[selected] = null;
            UpdateInventoryUI();
        }
        return t;
    }

    public bool GetTower(TowerObjects tower)
    {
        for (int i = 0; i < 6; i++)
        {
            if (inv[i] == null)
            {
                inv[i] = tower;
                UpdateInventoryUI();
                return true;
            }
        }
        return false;
    }
    
    public void AddFragment()
    {
        currentFragments++;
        fragmentText.text = GetFragments().ToString();
    }

    public int GetFragments()
    {
        return currentFragments;
    }

    public int GetFragmentsCost()
    {
        return minFragments;
    }
}
