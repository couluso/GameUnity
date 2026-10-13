
using UnityEngine;
using UnityEngine.UI;

public class BossHealthBarUI : MonoBehaviour
{
    [SerializeField] private GameObject bossHealthBar;
    [SerializeField] private Slider healthSlider;

    private EnemyHealth bossHealth;

    private void Start()
    {
        bossHealthBar.SetActive(false);
    }

    private void Update()
    {
        if (bossHealth == null)
            return;

        healthSlider.maxValue = bossHealth.maxHealth;
        healthSlider.value = bossHealth.currentHealth;
    }

    public void AfficherBoss(EnemyHealth boss)
    {
        bossHealth = boss;
        bossHealthBar.SetActive(true);

        healthSlider.maxValue = boss.maxHealth;
        healthSlider.value = boss.currentHealth;
    }

    public void MasquerBoss()
    {
        bossHealth = null;
        bossHealthBar.SetActive(false);
    }
}
