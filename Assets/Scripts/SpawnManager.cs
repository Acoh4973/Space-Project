using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager instance;

    int currentWave;
    float waveDelayTime;
    int EnemyCount;
    public bool aliveEnemies;
    float aliveEnemiesTime;
    bool EnemyCapped;
    float enemyCheck;

    [SerializeField] Transform[] spawnPositions;
    [SerializeField] GameObject[] LowEnemies;
    [SerializeField] GameObject[] MedEnemies;
    [SerializeField] GameObject[] HighEnemies;
    [SerializeField] GameObject Boss1;
    [SerializeField] GameObject Boss2;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (!EnemyCapped) waveDelayTime -= Time.deltaTime;
        if (waveDelayTime <= 0)
        {
            currentWave++;
            EnemyCount = 3 + (2 * currentWave);
            waveCheck();
        }
        if (!aliveEnemies)
        {
            waveDelayTime -= Time.deltaTime * 15;
        }
        capEnemies();
        EnemyCheck();
    }

    void waveCheck()
    {
        if (currentWave > 0 && currentWave <= 5)
        {
            waveDelayTime = 60;
            while(EnemyCount > 0) lowSpawn();
        }
        if (currentWave > 5 && currentWave <= 10)
        {
            waveDelayTime = 60;
            while (EnemyCount > 0)
            {
                int threat = Random.Range(0, 2);
                if (threat == 0) lowSpawn();
                else if (threat == 1) medSpawn();
            }
        }
        if (currentWave > 10 && currentWave <= 14)
        {
            waveDelayTime = 60;
            while (EnemyCount > 0) medSpawn();
        }
        if (currentWave == 15)
        {
            spawnFirstBoss();
        }
        if (currentWave > 15 && currentWave <= 20)
        {
            waveDelayTime = 60;
            while (EnemyCount > 0) medSpawn();
        }
        if (currentWave > 20 && currentWave <= 25)
        {
            waveDelayTime = 60;
            while (EnemyCount > 0)
            {
                int threat = Random.Range(0, 2);
                if (threat == 0) medSpawn();
                else if (threat == 1) HighSpawn();
            }
        }
        if (currentWave > 25 && currentWave <= 29)
        {
            waveDelayTime = 60;
            while (EnemyCount > 0) HighSpawn();
        }
        if (currentWave == 30)
        {
            spawnSecondBoss();
        }
        if (currentWave > 30)
        {
            GameManager.instance.endGame();
        }
    }
    void lowSpawn()
    {
        int arrayPos = Random.Range(0, LowEnemies.Length);
        int arraySpawnPos = Random.Range(0, spawnPositions.Length);
            Instantiate(LowEnemies[arrayPos],
                spawnPositions[arraySpawnPos].position,
                spawnPositions[arraySpawnPos].rotation);
            EnemyCount--;
    }
    void medSpawn()
    {
        int arrayPos = Random.Range(0, MedEnemies.Length);
        int arraySpawnPos = Random.Range(0, spawnPositions.Length);
            Instantiate(MedEnemies[arrayPos],
                spawnPositions[arraySpawnPos].position,
                spawnPositions[arraySpawnPos].rotation);
            EnemyCount--;
    }
    void HighSpawn()
    {
        int arrayPos = Random.Range(0, HighEnemies.Length);
        int arraySpawnPos = Random.Range(0, spawnPositions.Length);
            Instantiate(HighEnemies[arrayPos],
                spawnPositions[arraySpawnPos].position,
                spawnPositions[arraySpawnPos].rotation);
            EnemyCount--;
    }

    void spawnFirstBoss()
    {
        EnemyCount = 1;
        waveDelayTime += 120;
        int arrayPos = Random.Range(0, HighEnemies.Length);
        int arraySpawnPos = Random.Range(0, spawnPositions.Length);
        Instantiate(Boss1,
            spawnPositions[arraySpawnPos].position,
            spawnPositions[arraySpawnPos].rotation);
        EnemyCount--;
    }

    void spawnSecondBoss()
    {
        EnemyCount = 1;
        waveDelayTime += 180;
        int arrayPos = Random.Range(0, HighEnemies.Length);
        int arraySpawnPos = Random.Range(0, spawnPositions.Length);
        Instantiate(Boss2,
            spawnPositions[arraySpawnPos].position,
            spawnPositions[arraySpawnPos].rotation);
        EnemyCount--;
    }

    void EnemyCheck()
    {
        enemyCheck += Time.deltaTime;
        if (enemyCheck > 3)
        {
            aliveEnemies = false;
            enemyCheck = 0;
        }
    }

    void capEnemies()
    {
        if (aliveEnemies)
        {
            aliveEnemiesTime += Time.deltaTime;
            if (aliveEnemiesTime > 90) EnemyCapped = true;
        }
        else
        {
            aliveEnemiesTime = 0;
        }
    }
}
