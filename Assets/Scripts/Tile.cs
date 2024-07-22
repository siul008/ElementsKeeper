using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    private GameObject tower;

    public void SetTower(TowerObjects t)
    {
        if (!tower)
        {
            tower = Instantiate(t.spawnableTower, transform);
        }
    }

    public void RemoveTower()
    {
        if (tower)
            Destroy(tower);
        tower = null;
    }
    
    public GameObject GetTower()
    {
        return tower;
    }
}
