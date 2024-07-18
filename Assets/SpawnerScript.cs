using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerScript : MonoBehaviour
{
    [SerializeField] GameObject enemy;
    [SerializeField] Transform[] spawns;
    [SerializeField] float spawnRate = 1f;
    float cooldown = 1f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        cooldown -= Time.deltaTime;
        if(cooldown <= 0)
        {
            cooldown = spawnRate;
            spawnRate *= 0.99f;
            Spawn();
        }
    }

    void Spawn()
    {
        int spawnPoint = Random.Range(0, spawns.Length);
        Debug.Log("Spawning at: " + spawnPoint);
        Instantiate(enemy, spawns[spawnPoint].position, Quaternion.identity);
    }

}
