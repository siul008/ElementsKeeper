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
    int enemiesToSpawn;
    int remaining;

    [SerializeField] int firstElementsStart;


    [SerializeField]
    List<Wave> waves = new List<Wave>();
    private int totalPercent = 0;

    public static SpawnerScript Instance { get; private set; }

    private void Awake()
    {

        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    void Start()
    {
        index = 0;
        enemiesToSpawn = waves[index].enemiesNbr;
        spawnTime = 0;
        Grid.Instance.HighlightLanes(waves[index].lanes);
        for (int i = 0; i < firstElementsStart; i++)
        {
            InventoryManager.Instance.GenerateFirstTowers();
        }
    }

    void Update()
    {
        if (spawnTime >= waves[index].spawnRate && enemiesToSpawn > 0)
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
        if (index + 1 < waves.Count)
        {
            index++;
        }

        //Get one element for each endReward of the wave
        for (int i = 0; i < waves[index].endReward; i++)
        {
            InventoryManager.Instance.GenerateTower();
        }
        Grid.Instance.HighlightLanes(waves[index].lanes);

        totalPercent = 0;
        foreach (var e in waves[index].enemies)
        {
            totalPercent += e.percent;
        }

        if (totalPercent <= 0)
        {
            Debug.LogError("total percent is lower than 0%");
        }

        //Wait the end of wave delay
        yield return new WaitForSeconds(endOfWaveDelay);
        enemiesToSpawn = waves[index].enemiesNbr;
        spawnTime = 0;
    }

    public void EnemyDied()
    {
        remaining--;
        if (remaining <= 0 && enemiesToSpawn <= 0)
        {
            StartCoroutine(NextWave());
        }
    }
    void Spawn()
    {
        spawnTime = 0;
        int lane = waves[index].lanes[Random.Range(0, waves[index].lanes.Length)];
        Vector3 spawnPoint = spawns[lane].position;
        spawnPoint.y = Random.Range(spawnPoint.y - 0.10f, spawnPoint.y + 0.11f);
        Instantiate(ChooseEnemy(), spawnPoint, Quaternion.identity);
        remaining++;
        enemiesToSpawn--;
    }

    GameObject ChooseEnemy()
    {
        int currentPercent = 0;
        int i = Random.Range(0, totalPercent);
        Debug.Log("total percent = " + totalPercent);
        Debug.Log("random nb = " + i);
        foreach (var e in waves[index].enemies)
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
        public string name;
        public int enemiesNbr;
        public float spawnRate;
        public int endReward;
        public int[] lanes;
        public List<SpawnableEnemy> enemies;
    }
    
    [System.Serializable]
    public class SpawnableEnemy
    {
        public string name;
        public int percent;
        public GameObject obj;
    }
}
