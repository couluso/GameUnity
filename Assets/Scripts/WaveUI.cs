
using UnityEngine;
using TMPro;

public class WaveUI : MonoBehaviour
{
    [SerializeField] private EnemySpawner enemySpawner;
    [SerializeField] private TMP_Text waveText;

    private void Update()
    {
        if (enemySpawner != null && waveText != null)
        {
            waveText.text = "Vague "
                + enemySpawner.currentWave
                + " / 15";
        }
    }
}
