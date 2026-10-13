using JetBrains.Annotations;
using System.Collections;
using System.Data;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    private GameObject player;
    private GameObject slider;
    
    private Movements PV;
    private Slider valeur;

    private void Start()
    {
        player = GameObject.Find("Controller");
        slider = GameObject.Find("Slider");

        PV = player.GetComponent<Movements>();
        valeur = slider.GetComponent<Slider>();

        valeur.maxValue = PV.HP;
    }


    private void Update()
    {
        valeur.value = PV.HP;
    }
}