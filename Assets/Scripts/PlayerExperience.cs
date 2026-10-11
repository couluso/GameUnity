using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerExperience : MonoBehaviour
{
    public int level = 1;
    public int currentXP = 0;
    public LevelUp levelUpMenu;

    [SerializeField] private int baseXP = 0;
    [SerializeField] private float xpMultiplier = 1.2f;

    private int xpToNextLevel;

    private void Start()
    {
        UpdateXPNeeded();
    }

    public void GainXP(int amount)
    {
        currentXP += amount;

        Debug.Log("XP : " + currentXP + " / " + xpToNextLevel);

        while (currentXP >= xpToNextLevel)
        {
            currentXP -= xpToNextLevel;
            level++;
            levelUpMenu.OuvrirMenu();

            UpdateXPNeeded();

            Debug.Log("Niveau supérieur ! Niveau : " + level);
        }
    }

    private void UpdateXPNeeded()
    {
        xpToNextLevel = Mathf.RoundToInt(
            baseXP * Mathf.Pow(xpMultiplier, level - 1)
        );
    }
}

