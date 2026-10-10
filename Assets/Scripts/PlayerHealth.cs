using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] private int maxHealth = 10;

    [Header("Invulnerability")]
    [SerializeField] private float invulnerabilityDuration = 1f;

    private int currentHealth;
    private float nextDamageTime;
    private bool isDead;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        if (isDead || Time.time < nextDamageTime)
            return;

        currentHealth = Mathf.Max(0, currentHealth - damage);
        nextDamageTime = Time.time + invulnerabilityDuration;

        Debug.Log(
            $"Joueur : {currentHealth}/{maxHealth} PV"
        );

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        isDead = true;
        Debug.Log("Le joueur est mort !");
    }
}