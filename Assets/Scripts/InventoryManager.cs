using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public TowerObjects[] inv = new TowerObjects[6];
    private int nbTower = 0;
    [SerializeField] private Transform images;
    public TowerObjects test;
    
    // Start is called before the first frame update
    void Start()
    {
        AddTower(test);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void UpdateInventoryUI()
    {
        for (int i = 0; i < nbTower; i++)
        {
            images.GetChild(i).GetComponent<Image>().sprite = inv[i].sprite;
        }
    }

    void AddTower(TowerObjects obj)
    {
        inv[nbTower] = obj;
        nbTower++;
        UpdateInventoryUI();
    }
}
