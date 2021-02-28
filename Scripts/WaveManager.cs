using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public Vector3 spawnZoneLeftUp;
    public Vector3 spawnZoneRightDown;
    public GameObject bossPrefab;
    public List<GameObject> enemiesPrefabs;
    public List<GameObject> enemies;

    public bool waveOver;
    public GameObject waveOverText;
    public GameObject textBox;
    public bool textBoxShowed;
    public bool shopShowed;

    public int curWave;
    public int baseNrofEnemiesPerWave;
    private float nrOfEnemiesPerWave;
    public float difficulty; //smaller value = harder gameplay
    public float distBetweenSpawns;

    public float bossSpawnRate;
    public float timeUntilNextBossWaveSpawn;

    private void Update() {
        bool delete = false;
        List<int> enemiesToDelete = new List<int>();
        foreach(GameObject enemy in enemies) {
            if(enemy == null) {
                delete = true;
                enemiesToDelete.Add(enemies.IndexOf(enemy));
            }
        }
        if(delete){
            foreach(int enemyNr in enemiesToDelete) {
                enemies.Remove(enemies[enemyNr]);
            } 
        }

        if(enemies.Count == 0) WaveOver();

        if(waveOver && Input.GetKeyDown(KeyCode.Return)) {
            this.GetComponent<ShopManager>().shop.SetActive(false);
            shopShowed = true;
            SpawnEnemies();
            waveOverText.SetActive(false);
            textBox.SetActive(false);
        }

        foreach (var enemy in enemies)
        {
            if(enemy.tag == "Boss") {
                if(timeUntilNextBossWaveSpawn <= Time.time) {
                    StartCoroutine (SpawnBossWaveEnemiesIenum());
                    timeUntilNextBossWaveSpawn = bossSpawnRate + Time.time;
                }
            }
        }
    }

    IEnumerator SpawnBossWaveEnemiesIenum() {
        yield return new WaitForSeconds(bossSpawnRate);
        foreach (var enemy in enemies)
        {
            if(enemy.tag == "Boss") {
                SpawnBossWaveEnemies();
            }
        }
    }

    public void SpawnEnemies() {
        nrOfEnemiesPerWave = baseNrofEnemiesPerWave + Mathf.Ceil(curWave / difficulty);
        waveOver = false;
        for(int i = 0; i < nrOfEnemiesPerWave; i++) {
            GameObject chosenEntity = enemiesPrefabs[Random.Range(0, enemiesPrefabs.Count)];
            Vector3 spawnLoc = CreatRandPos();
            while(spawnLoc == new Vector3(0,50,0)) spawnLoc = CreatRandPos();
            GameObject spawnedEntity = Instantiate(chosenEntity, spawnLoc, Quaternion.identity);
            enemies.Add(spawnedEntity);
        }
        if((curWave + 1) % 5 == 0 && curWave != 0) {
            GameObject spawnedEntity = Instantiate(bossPrefab, new Vector3(0,20,0), Quaternion.identity);
            enemies.Add(spawnedEntity);
        }
        curWave += 1;
    }

    public void SpawnBossWaveEnemies() {
        nrOfEnemiesPerWave = baseNrofEnemiesPerWave;
        waveOver = false;
        for(int i = 0; i < nrOfEnemiesPerWave; i++) {
            GameObject chosenEntity = enemiesPrefabs[Random.Range(0, enemiesPrefabs.Count)];
            Vector3 spawnLoc = CreatRandPos();
            while(spawnLoc == new Vector3(0,50,0)) spawnLoc = CreatRandPos();
            GameObject spawnedEntity = Instantiate(chosenEntity, spawnLoc, Quaternion.identity);
            enemies.Add(spawnedEntity);
        }
    }

    private Vector3 CreatRandPos() {
        Vector3 spawnLoc = new Vector3(Random.Range(spawnZoneLeftUp.x, spawnZoneRightDown.x), Random.Range(spawnZoneLeftUp.y, spawnZoneRightDown.y), 0);
            for (int i = 0; i < enemies.Count; i++)
            {
                if (Vector3.Distance(enemies[i].transform.position, spawnLoc)  < distBetweenSpawns)
                {
                    return new Vector3(0,50,0);
                }        
            }
        return spawnLoc;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.GetComponentInChildren<EnemyWeapon_Shooting>()) {
            EnableEnemy(other.gameObject);
        }
        if(other.GetComponentInChildren<BossEnemy_Shooting>()) {
            EnableEnemy(other.gameObject);
        }
    }

    void EnableEnemy(GameObject entity) {
        if(entity.GetComponentInChildren<EnemyWeapon_Shooting>()) {
            entity.GetComponentInChildren<EnemyWeapon_Shooting>().enabled = true;
        }
        if(entity.GetComponentInChildren<EnemyBase>().boss) {
            entity.GetComponentInChildren<BossEnemy_Shooting>().enabled = true;
            entity.GetComponentInChildren<EnemyBase>().Stop();
        }
    }

    public void WaveOver() {
        waveOver = true;
        waveOverText.SetActive(true);
        if(!textBoxShowed)    {
            textBox.SetActive(true);
            textBoxShowed = true;
        }
        if(shopShowed)    this.GetComponent<ShopManager>().shop.SetActive(true);
    }

    public float CalculateDifficultySpeed(float baseSpeed) {
        var val = baseSpeed + curWave / difficulty;
        return val;
    }

    public float CalculateDifficultyHealth(float baseHealth) {
        var val = baseHealth + Mathf.Round(curWave / difficulty / 2);
        return val;
    }

    public float CalculateDifficultyFireRate(float baseFireRate) {
        var val = baseFireRate - Mathf.Round(curWave / difficulty / 2);
        if(val <= 0.5) return baseFireRate;
        return val;
    }
}
