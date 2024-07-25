using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    private GameObject tower;
    [SerializeField] private GameObject particles;

    public void SetTower(TowerObjects t)
    {
        if (!tower)
        {
            StartCoroutine(SpawnTower(t));
        }
    }

    IEnumerator SpawnTower(TowerObjects t)
    {
        Instantiate(particles, transform);
        yield return new WaitForSeconds(1f);
        tower = Instantiate(t.spawnableTower, transform);
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
