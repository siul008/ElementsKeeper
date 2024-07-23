using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerScript : MonoBehaviour
{
    [SerializeField] GameObject enemy;
    [SerializeField] Transform[] spawns;
    float spawnTime;
    [SerializeField]
    float endOfWaveDelay;
    int index;
    public int remaining;

    [SerializeField]
    List<Wave> waves = new List<Wave>();

    void Start()
    {
        index = 0;
        remaining = waves[index].enemiesNbr;
        spawnTime = 0;
    }

    void Update()
    {
        if (spawnTime >= waves[index].spawnRate && remaining > 0)
        {
            Spawn();
        }
        else
        {
            spawnTime += Time.deltaTime;
        }
    }

    IEnumerator NextWave()
    {
        yield return new WaitForSeconds(endOfWaveDelay);
        index++;
        remaining = waves[index].enemiesNbr;
        spawnTime = 0;
    }

    void Spawn()
    {
        spawnTime = 0;
        Vector3 spawnPoint = spawns[Random.Range(0, spawns.Length)].position;
        spawnPoint.y = Random.Range(spawnPoint.y - 0.10f, spawnPoint.y + 0.11f);
        Instantiate(enemy, spawnPoint, Quaternion.identity);
        remaining--;
        if (remaining <= 0)
        {
            StartCoroutine(NextWave());
        }
    }

    [System.Serializable]
    public class Wave 
    {
        public int enemiesNbr;
        public float spawnRate;
    }
}
