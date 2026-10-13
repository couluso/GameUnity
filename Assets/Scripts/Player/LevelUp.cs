using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelUp : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    [SerializeField] private Button[] boutons;
    [SerializeField] private TMP_Text[] textes;

    private Movements joueur;
    private WeaponAuto arme;

    private enum Amelioration
    {
        Cadence,
        Degats,
        Vitesse,
        Soin
    }

    private void Start()
    {
        joueur = GameObject.Find("Controller")
            .GetComponent<Movements>();

        arme = FindFirstObjectByType<WeaponAuto>();

        panel.SetActive(false);
    }

    public void OuvrirMenu()
    {
        Time.timeScale = 0f;
        joueur.enpause = true;

        List<Amelioration> choix =
            new List<Amelioration>
            {
                Amelioration.Cadence,
                Amelioration.Degats,
                Amelioration.Vitesse,
                Amelioration.Soin
            };

        for (int i = 0; i < 3; i++)
        {
            int index = Random.Range(0, choix.Count);
            Amelioration amelioration = choix[index];
            choix.RemoveAt(index);

            textes[i].text = NomAmelioration(amelioration);

            boutons[i].onClick.RemoveAllListeners();

            boutons[i].onClick.AddListener(
                () => ChoisirAmelioration(amelioration)
            );
        }

        panel.SetActive(true);
    }

    private string NomAmelioration(Amelioration a)
    {
        switch (a)
        {
            case Amelioration.Cadence:
                return "Fire rate up!";

            case Amelioration.Degats:
                return "Damage up!";

            case Amelioration.Vitesse:
                return "Speed up!";

            case Amelioration.Soin:
                return "Health up!";

            default:
                return "";
        }
    }

    private void ChoisirAmelioration(Amelioration a)
    {
        switch (a)
        {
            case Amelioration.Cadence:
                arme.AugmenterCadence();
                break;

            case Amelioration.Degats:
                arme.AugmenterDegats();
                break;

            case Amelioration.Vitesse:
                joueur.PlayerSpeed += 1.5f;
                break;

            case Amelioration.Soin:
                joueur.HP = Mathf.Min(
                    joueur.MaxHP,
                    joueur.HP + 25
                );
                break;
        }

        panel.SetActive(false);
        joueur.enpause = false;
        Time.timeScale = 1f;
    }
}
