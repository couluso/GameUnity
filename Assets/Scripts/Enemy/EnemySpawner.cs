using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private Transform player;

    [Header("Spawn Settings")]
    [SerializeField] private float spawnRadius = 10f;
    [SerializeField] private float spawnInterval = 3f;
    [SerializeField] private int enemiesPerWave = 5;
    [SerializeField] private int maxEnemies = 100;

    [Header("Group Settings")]
    [SerializeField] private float groupRadius = 2f;


    private GameObject controller;
    private Movements movements;

    private float timer;

    private void Start()
    {
        controller = GameObject.Find("Controller");
        movements = controller.GetComponent<Movements>();
    }


    private void Update()
    {

        if (movements.enpause == false)
        {
        if (player == null || enemyPrefab == null)
            return;

        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            timer = 0f;
            SpawnWave();
        }

        }

    }

    private void SpawnWave()
    {


        int currentEnemies =
            GameObject.FindGameObjectsWithTag("Enemy").Length;

        int availableSlots = maxEnemies - currentEnemies;
        int count = Mathf.Min(enemiesPerWave, availableSlots);

        if (count <= 0)
            return;

        // Choisir un point aléatoire autour du joueur sur le plan XZ
        Vector2 randomDirection = Random.insideUnitCircle.normalized;

        if (randomDirection.sqrMagnitude < 0.01f)
            randomDirection = Vector2.up;

        Vector3 center = player.position + new Vector3(
            randomDirection.x,
            0f,
            randomDirection.y
        ) * spawnRadius;

        // Faire apparaître le groupe autour de ce point
        for (int i = 0; i < count; i++)
        {
            Vector2 offset = Random.insideUnitCircle * groupRadius;

            Vector3 spawnPosition = center + new Vector3(
                offset.x,
                0f,
                offset.y
            );

            spawnPosition.y = player.position.y;

            GameObject enemy = Instantiate(
                enemyPrefab,
                spawnPosition,
                Quaternion.identity
            );

            enemy.tag = "Enemy";
        }
    }
}