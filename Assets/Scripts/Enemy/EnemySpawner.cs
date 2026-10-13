using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private Transform player;
    [SerializeField] private EnemyHealth health;
    [SerializeField] private EnemyFollow speed;
    [SerializeField] private GameObject bossPrefab;
    [SerializeField] private BossHealthBarUI bossHealthBarUI;

    [Header("Spawn Settings")]
    [SerializeField] private float spawnRadius = 10f;
    [SerializeField] private float spawnInterval = 3f;
    [SerializeField] private int enemiesPerWave = 5;
    [SerializeField] private int enemiesIncreasePerWave = 2;
    [SerializeField] private int maxEnemies = 100;

    [Header("Wave Settings")]
    [SerializeField] private float waveDuration = 20f;
    [SerializeField] public KeyCode VagueBoss = KeyCode.M;

    [Header("Big Groups")]
    [SerializeField] private float groupRadius = 2f;
    [SerializeField] private float bigGroupChance = 0.25f;
    [SerializeField] private int bigGroupMin = 10;
    [SerializeField] private int bigGroupMax = 15;

    private Movements movements;

    private float spawnTimer;
    private float waveTimer;

    public int currentWave = 1;
    private bool finDesVagues = false;

    private void Start()
    {
        GameObject controller = GameObject.Find("Controller");

        if (controller != null)
        {
            movements = controller.GetComponent<Movements>();
        }

        if (player == null)
        {
            GameObject playerObject =
                GameObject.FindGameObjectWithTag("Player");

            if (playerObject != null)
                player = playerObject.transform;
        }

        health = enemyPrefab.GetComponent<EnemyHealth>();
        speed = enemyPrefab.GetComponent<EnemyFollow>();

        health.maxHealth = 3;
    }

    private void Update()
    {
        if (player == null || enemyPrefab == null)
            return;

        if (movements != null && movements.enpause)
            return;

        if (finDesVagues)
            return;

        // Faire progresser les vagues avec le temps
        waveTimer += Time.deltaTime;

        if (waveTimer >= waveDuration)
        {
            waveTimer = 0f;

            if (currentWave >= 15)
            {
                TerminerVagues();
                return;
            }

            currentWave++;

            health.maxHealth++;

            speed.moveSpeed += 2f;
        }

        // Faire apparaître les ennemis régulièrement
        spawnTimer += Time.deltaTime;

        if (spawnTimer >= spawnInterval)
        {
            spawnTimer = 0f;
            SpawnEnemies();
        }

        if (Input.GetKeyDown(VagueBoss))
            TerminerVagues();
    }

    private void SpawnEnemies()
    {
        int currentEnemies =
            GameObject.FindGameObjectsWithTag("Enemy").Length;

        int availableSlots = maxEnemies - currentEnemies;

        if (availableSlots <= 0)
            return;

        // Le nombre normal augmente avec chaque vague
        int count = enemiesPerWave
            + (currentWave - 1) * enemiesIncreasePerWave;

        // Parfois, faire apparaître un gros groupe
        if (Random.value < bigGroupChance)
        {
            count = Random.Range(bigGroupMin, bigGroupMax + 1);
        }

        count = Mathf.Min(count, availableSlots);

        // Choisir une direction autour du joueur
        Vector2 direction = Random.insideUnitCircle.normalized;

        if (direction.sqrMagnitude < 0.01f)
            direction = Vector2.up;

        Vector3 center = player.position + new Vector3(
            direction.x * spawnRadius,
            0f,
            direction.y * spawnRadius
        );

        // Regrouper les ennemis autour du point choisi
        for (int i = 0; i < count; i++)
        {
            Vector2 offset =
                Random.insideUnitCircle * groupRadius;

            Vector3 position = center + new Vector3(
                offset.x,
                0f,
                offset.y
            );

            position.y = 1;

            GameObject enemy = Instantiate(
                enemyPrefab,
                position,
                Quaternion.identity
            );

            enemy.tag = "Enemy";
        }
    }

    private void TerminerVagues()
    {
        finDesVagues = true;

        Debug.Log("Fin de la vague 15 !");

        EnemyHealth[] ennemis =
            FindObjectsOfType<EnemyHealth>();

        foreach (EnemyHealth ennemi in ennemis)
        {
            if (ennemi != null)
            {
                ennemi.Die();
            }
        }

        if (bossPrefab != null && player != null)
        {
            Vector3 positionBoss = player.position + player.forward * 5f;
            Quaternion rotation45X = Quaternion.Euler(45f, 0f, 0f);

            GameObject boss = Instantiate(bossPrefab,new Vector3(100, 1, 100),rotation45X);

            boss.tag = "Boss";

            EnemyHealth bossHealth = boss.GetComponent<EnemyHealth>();

            if (bossHealth != null && bossHealthBarUI != null)
            {
                bossHealthBarUI.AfficherBoss(bossHealth);
                bossHealth.bossHealthBarUI = bossHealthBarUI;
            }

            Debug.Log("Le boss est apparu !");
        }
        else
        {
            Debug.LogError(
                "BossPrefab ou Player non assigné dans EnemySpawner !"
            );
        }
    }
}