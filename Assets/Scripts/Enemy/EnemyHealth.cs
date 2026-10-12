using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] public int maxHealth = 3;
    [SerializeField] private GameObject Orb;
    [SerializeField] private GameObject OrbPlus;
    [SerializeField] private int RNG;

    public int currentHealth;

    private void Awake()
    {
        if (transform.position.x < 0 || transform.position.z < 0 || transform.position.x > 200 || transform.position.z > 200)
        {
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
        if (RNG != 1)
        {
            Instantiate(Orb, transform.position, Quaternion.identity);
        }
        else if (RNG == 1)
        {
            Instantiate(OrbPlus, transform.position, Quaternion.identity);
        }

        Destroy(gameObject);
    }
}
