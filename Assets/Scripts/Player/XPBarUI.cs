
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class XPBarUI : MonoBehaviour
{
    [SerializeField] private PlayerExperience playerExperience;
    [SerializeField] private Slider xpSlider;

    private void Update()
    {
        if (playerExperience == null ||
            xpSlider == null)
        {
            return;
        }

        int xpNecessaire = playerExperience.XPToNextLevel;
        int xpActuelle = playerExperience.currentXP;

        if (xpNecessaire <= 0)
        {
            xpSlider.value = 0f;
            return;
        }

        xpSlider.value =
            (float)xpActuelle / xpNecessaire;
    }
}
