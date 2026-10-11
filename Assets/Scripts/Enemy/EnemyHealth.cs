using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] public int maxHealth = 3;
    [SerializeField] private GameObject Orb;
    [SerializeField] private GameObject OrbPlus;
    [SerializeField] private int x;

    public int currentHealth;

    private void Awake()
    {
        x = Random.Range(0, 4);
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

    private void Die()
    {
        if (x != 1)
        {
            Instantiate(Orb, transform.position, Quaternion.identity);
        }
        else if (x == 1)
        {
            Instantiate(OrbPlus, transform.position, Quaternion.identity);
        }

        Destroy(gameObject);
    }
}
