using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceDefenceScript : MonoBehaviour
{
    //[SerializeField] GameObject tower;
    // array of towers
    Dictionary<Vector2Int, GameObject> dictionary = new Dictionary<Vector2Int, GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // if space is pressed spawn a tower
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Vector2Int pos(transform.position.x, transform.position.y);
            Vector2Int pos = new Vector2Int((int)(transform.position.x + 0.5), (int)(transform.position.y + 0.5));
            if(dictionary.ContainsKey(pos))
            {
                Destroy(dictionary[pos]);
                dictionary.Remove(pos);
            }
            else
            {
                TowerObjects tower = InventoryManager.Instance.GetSelectedTower();
                if (tower != null)
                    dictionary[pos] = Instantiate(tower.spawnableTower, new Vector3(pos.x, pos.y), Quaternion.identity);
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
