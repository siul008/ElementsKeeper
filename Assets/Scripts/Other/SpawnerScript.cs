using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerScript : MonoBehaviour
{
    [SerializeField] Transform[] spawns;
    [SerializeField] List<Wave> waves = new List<Wave>();
    int index;
    float time;
    float timeToWait;

    enum WaveState
    {
        Spawning,
        WaitingToContinue,
    }

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

    private void Start()
    {
        index = 0;
        time = 0;
        timeToWait = waves[index].delay;
    }

    private void Update()
    {
        if (time >= timeToWait)
        {
            Spawn();
        }
        else
        {
            time += Time.deltaTime;
        }
    }



    void GetToNextSpawn()
    {
        time = 0;
        if (index + 1 < waves.Count)
        {
            index++;
        }
        timeToWait = waves[index].delay;
    }

    void Spawn()
    {
        Instantiate(waves[index].enemy, spawns[waves[index].lane].position, Quaternion.identity);
        GetToNextSpawn();
    }

    public void EnemyDied()
    {
        /*remaining--;
        if (remaining <= 0 && enemiesToSpawn <= 0)
        {
            StartCoroutine(NextWave());
        }*/
    }


    [System.Serializable]
    public class Wave 
    {
        public string name;
        public int lane;
        public GameObject enemy;
        public float delay;

    }
}
