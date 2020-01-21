using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    [Header("Enemy Prefab reference")]
    public GameObject enemyPrefab;

    [Header("Player reference")]
    public GameObject player;

    [Header("Spawn Position variables")]
    public Vector2 xSpawnRange = new Vector2(-10, 10);
    public Vector2 ySpawnRange = new Vector2(-10,10); 
    public float minimumSpawnDistanceFromPlayer = 5f;

    [Header("Maximum number of enemies")]
    public int maxEnemies = 3;
    private int _numberOfEnemies;

    [Header("Enemy Spawn Rate")]
    public float enemySpawnRate = 1f;
    private float _timeLastSpawn;
    
    // Start is called before the first frame update
    private void Start()
    {
        _timeLastSpawn = Time.time - enemySpawnRate;
    }

    // Update is called once per frame
    private void Update()
    {
        if (_numberOfEnemies < maxEnemies && Time.time - _timeLastSpawn > enemySpawnRate)
        {
            SpawnEnemy();
        }
    }

    private void SpawnEnemy()
    {
        _timeLastSpawn = Time.time;
        var position = GetRandomPosition();
        var enemyInstance = Instantiate(enemyPrefab, position, Quaternion.identity);
        var enemyController = enemyInstance.GetComponent<EnemyController>();
        enemyController.player = player;
        enemyController.enemyGenerator = this;
    }

    private Vector2 GetRandomPosition()
    {
        Vector2 position;
        var playerPosition = player.transform.position;
        
        do
        {
            var randomX = Random.Range(xSpawnRange.x, xSpawnRange.y);
            var randomY = Random.Range(ySpawnRange.x, ySpawnRange.y);
            position = new Vector2(randomX, randomY);
        } while (Vector2.Distance(playerPosition, position) < minimumSpawnDistanceFromPlayer);

        return position;
    }

    public void EnemyIsDead()
    {
        // Little hack to postpone the spawning time and avoid having an instant spawn
        if (_numberOfEnemies == maxEnemies)
            _timeLastSpawn = Time.time;
        
        _numberOfEnemies--;
        if (_numberOfEnemies < 0)
            _numberOfEnemies = 0;
    }

    public void EnemyIsAlive()
    {
        _numberOfEnemies++;
    }
}
