using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public List<TowerObjects> inv = new List<TowerObjects>();
    [SerializeField] TowerObjects[] voidFragments = new TowerObjects[4];
    [SerializeField] private Transform images;
    public TowerObjects test;
    private int currentFragments = 0;
    [SerializeField] private int minFragments = 10;
    int selected = 0;
    public static InventoryManager Instance { get; private set; }

    private void Awake() 
    { 
        // If there is an instance, and it's not me, delete myself.
    
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
    }
    
    void Start()
    {
        AddTower(test);
        AddTower(test);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            selected++;
            if (selected >= 6)
                selected = 0;
            UpdateInventoryUI();
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            selected--;
            if (selected < 0)
                selected = inv.Count - 1;
            UpdateInventoryUI();
        }
    }

    void UpdateInventoryUI()
    {
        int i;
        for (i = 0; i < 6; i++)
        {
            Image child = images.GetChild(i).GetComponent<Image>();
            if (i == selected)
                child.color = new Color(child.color.r, child.color.g, child.color.b, 0.9f);
            else
                child.color = new Color(child.color.r, child.color.g, child.color.b, 0.2f);
            if (inv[i] == null)
            {
                images.GetChild(i + 6).gameObject.SetActive(false);
                continue;
            }
            Image tower = images.GetChild(i + 6).GetComponent<Image>();
            //child.sprite = inv[i].sprite;
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
        Debug.Log("Merging BRO : " + index1 + " + " + index2);
        TowerObjects newTower = MergeManager.Instance.Merge(inv[index1], inv[index2]);
        if (newTower == null)
            return false;
        inv[index1] = null;
        inv[index2] = newTower;
        UpdateInventoryUI();
        return true;
    }

    public bool Swap(int index1, int index2)
    {
        Debug.Log("Want to swap " + index1 + " with " + index2);
        if (inv[index1] != null && inv[index2] != null)
            return (Merge(index1, index2));
        else
            (inv[index1], inv[index2]) = (inv[index2], inv[index1]);
        return true;
    }

    public void AddRandomFragment()
    {
        currentFragments++;
        if (currentFragments < minFragments)
            return;
        int rand = Random.Range(0, voidFragments.Length);
        for (int i = 0; i < 6; i++)
        {
            if (inv[i] == null)
            {
                inv[i] = voidFragments[rand];
                UpdateInventoryUI();
                currentFragments -= minFragments;
                return;
            }
        }
    }
}
