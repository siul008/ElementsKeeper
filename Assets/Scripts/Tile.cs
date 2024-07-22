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

    public void SetTower(GameObject t)
    {
        if (!tower)
        {
            tower = t;
            tower.transform.parent = this.transform;
            tower.transform.position = this.transform.position;
        }
    }

    public void RemoveTower(bool destroy)
    {
        if (tower && destroy)
        {
            Destroy(tower);
        }
        tower = null;
    }

    public GameObject GetTower()
    {
        return tower;
    }
}
