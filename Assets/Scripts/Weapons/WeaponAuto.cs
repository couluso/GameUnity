using UnityEngine;

public class WeaponAuto : MonoBehaviour
{
    [Header("Weapon")]
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float attackInterval = 1f;
    [SerializeField] private float range = 8f;

    private float timer;


    private GameObject controller;
    public Movements script;


    private void Start()
    {
        controller = GameObject.Find("Controller");
        script = controller.GetComponent<Movements>();
    }

    private void Update()
    {
        if (script.enpause == false)
        {
            timer += Time.deltaTime;

            if (timer < attackInterval)
                return;

            EnemyHealth target = FindNearestEnemy();

            if (target == null)
                return;

            timer = 0f;

            Vector3 direction =
                target.transform.position - firePoint.position;

            direction.y = 0f;

            GameObject shot = Instantiate(
                projectilePrefab,
                firePoint.position,
                Quaternion.identity
            );

            Projectile projectile = shot.GetComponent<Projectile>();

            if (projectile != null)
            {
                projectile.Launch(direction);
            }
            else
            {
                Destroy(shot);
                Debug.LogError(
                    "Le prefab du projectile ne possède pas le script Projectile."
                );
            }

        }
        
    }

    private EnemyHealth FindNearestEnemy()
    {
        EnemyHealth[] enemies =
            FindObjectsByType<EnemyHealth>(
                FindObjectsSortMode.None
            );

        EnemyHealth nearest = null;
        float bestDistance = range * range;

        foreach (EnemyHealth enemy in enemies)
        {
            if (enemy == null)
                continue;

            Vector3 offset =
                enemy.transform.position - transform.position;

            // Distance sur le plan XZ uniquement
            offset.y = 0f;

            float distanceSquared = offset.sqrMagnitude;

            if (distanceSquared < bestDistance)
            {
                bestDistance = distanceSquared;
                nearest = enemy;
            }
        }

        return nearest;
    }
}
