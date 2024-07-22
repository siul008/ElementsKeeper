using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceDefenceScript : MonoBehaviour
{
    //[SerializeField] GameObject tower;
    // array of towers
    Dictionary<Vector2Int, GameObject> dictionary = new Dictionary<Vector2Int, GameObject>();

    public GameObject cursor;
    public GameObject cursorGO;

    public GameObject carriedTower;
    public GameObject carriedTowerGO;


 
    // Start is called before the first frame update
    void Start()
    {
        cursorGO = Instantiate(cursor, transform.position, Quaternion.identity);
        carriedTowerGO = Instantiate(carriedTower, transform.position, Quaternion.identity);
        carriedTowerGO.GetComponent<TowerScript>().SetGhostTower(Color.white);

        Debug.Log(cursorGO == null);
    }

    // Update is called once per frame
    void Update()
    {

        Vector2Int pos = new Vector2Int(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y));
        cursorGO.transform.position = new Vector3(pos.x, pos.y, 1);
        carriedTowerGO.transform.position = new Vector3(pos.x, pos.y, 1);
        // if space is pressed spawn a tower
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Vector2Int pos(transform.position.x, transform.position.y);
            if (dictionary.ContainsKey(pos) &&
                InventoryManager.Instance.GetTower(dictionary[pos].GetComponent<TowerScript>().GetScriptable()))
            {
                Destroy(dictionary[pos]);
                dictionary.Remove(pos);
            }
            else
            {
                TowerObjects tower = InventoryManager.Instance.GetSelectedTower();
                if (tower != null)
                    dictionary[pos] = Instantiate(tower.spawnableTower, new Vector3(pos.x, pos.y, 1), Quaternion.identity);
            }
            // if(towers[posx][posy])
            // {
            //     Destroy(towers[posx][posy]);
            // }
            // else
            // {
            //     towers[posx][posy] = Instantiate(tower, new Vector3(posx, posy), Quaternion.identity);
            // }
            // Instantiate(tower, transform.position, Quaternion.identity);
        }
    }
}
