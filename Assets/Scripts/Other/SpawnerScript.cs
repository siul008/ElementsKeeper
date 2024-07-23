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

    [SerializeField] private List<SpawnableEnemy> enemies = new List<SpawnableEnemy>();
    private int totalPercent = 0;

    void Start()
    {
        index = 0;
        remaining = waves[index].enemiesNbr;
        spawnTime = 0;
        for (int i = 0; i < waves[index].lanes.Length; i++)
        {
            Debug.Log(waves[index].lanes[i]);
            Grid.Instance.HighlightLane(waves[index].lanes[i]);
        }
        
        foreach (var e in enemies)
        {
            totalPercent += e.percent;
        }

        if (totalPercent <= 0)
        {
            Debug.LogError("total percent is lower than 0%");
        }
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
        //Get one element for each endReward of the wave
        for (int i = 0; i < waves[index].endReward; i++)
        {
            InventoryManager.Instance.GenerateTower();
        }
        for (int i = 0; i < waves[index].lanes.Length; i++)
        {
            Debug.Log(waves[index].lanes[i]);
            Grid.Instance.DisableHightLight(waves[index].lanes[i]);
        }
        for (int i = 0; i < waves[index].lanes.Length; i++)
        {
            Debug.Log(waves[index + 1].lanes[i]);
            Grid.Instance.HighlightLane(waves[index + 1].lanes[i]);
        }
        //Wait the end of wave delay
        yield return new WaitForSeconds(endOfWaveDelay);
        index++;
        remaining = waves[index].enemiesNbr;
        spawnTime = 0;
    }

    void Spawn()
    {
        spawnTime = 0;
        int lane = waves[index].lanes[Random.Range(0, waves[index].lanes.Length)];
        Vector3 spawnPoint = spawns[lane].position;
        spawnPoint.y = Random.Range(spawnPoint.y - 0.10f, spawnPoint.y + 0.11f);
        Instantiate(ChooseEnemy(), spawnPoint, Quaternion.identity);
        remaining--;
        if (remaining <= 0)
        {
            StartCoroutine(NextWave());
        }
    }

    GameObject ChooseEnemy()
    {
        int currentPercent = 0;
        int i = Random.Range(0, totalPercent);
        Debug.Log("total percent = " + totalPercent);
        Debug.Log("random nb = " + i);
        foreach (var e in enemies)
        {
            currentPercent += e.percent;
            if (i < currentPercent)
                return e.obj;
        }
        return enemy;
    }

    [System.Serializable]
    public class Wave 
    {
        public int enemiesNbr;
        public float spawnRate;
        public int endReward;
        public int[] lanes;
    }
    
    [System.Serializable]
    public class SpawnableEnemy
    {
        public int percent;
        public GameObject obj;
    }
}
