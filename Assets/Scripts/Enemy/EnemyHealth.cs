using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] public int maxHealth = 3;
    [SerializeField] private GameObject Orb;
    [SerializeField] private GameObject OrbPlus;
    [SerializeField] private GameObject keyPrefab;
    [SerializeField] private int RNG;

    public int currentHealth;
    public BossHealthBarUI bossHealthBarUI;

    private void Awake()
    {
        if (transform.position.x < 0 || transform.position.z < 0 || transform.position.x > 200 || transform.position.z > 200)
        {
            if (this.CompareTag("Enemy"))
                Destroy(gameObject);
        }

        RNG = Random.Range(0, 10);
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Die()
{
    if (CompareTag("Boss"))
    {
            Instantiate(keyPrefab,new Vector3(transform.position.x, 1, transform.position.z), Quaternion.identity);
    }
    else if (CompareTag("Enemy"))
    {
        if (RNG != 1)
        {
            Instantiate(Orb, transform.position, Quaternion.identity);
        }
        else
        {
            Instantiate(OrbPlus, transform.position, Quaternion.identity);
        }
    }

        if (CompareTag("Boss") && bossHealthBarUI != null)
        {
            bossHealthBarUI.MasquerBoss();
        }

        Destroy(gameObject);
}
}
