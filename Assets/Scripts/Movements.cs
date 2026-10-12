using JetBrains.Annotations;
using System.Collections;
using System.Data;
using Unity.VisualScripting;
using UnityEngine;

public class Movements : MonoBehaviour
{
    [Header("Keybinds")]
    [SerializeField] KeyCode forward = KeyCode.W;
    [SerializeField] KeyCode left = KeyCode.A;
    [SerializeField] KeyCode back = KeyCode.S;
    [SerializeField] KeyCode right = KeyCode.D;
    [SerializeField] KeyCode menu = KeyCode.Escape;

    [Header("Références")]
    private CharacterController controller;
    private GameObject player;
    private GameObject options;
    private GameObject death;
    private GameObject scenemng;
    private Vector3 movement = Vector3.zero;

    [Header("Valeurs")]

    public float PlayerSpeed = 12f;

    public int HP = 100;

    public int MaxHP = 100;
    
    public bool enpause = false;

    public bool invincibilite = false;
    
    private bool attenteEnCours = false;


    private void Start()
    {
        player = GameObject.Find("Player");
        controller = player.GetComponent<CharacterController>();
        options = GameObject.Find("Options");
        death = GameObject.Find("Death");
    }

    // Update is called once per frame
    void Update()
    {
        if (enpause == false)
        
            options.SetActive(false);

        if (HP > 0)

            death.SetActive(false);

        // Déplacements perso
        float deltaTime = Time.deltaTime;
        float deltaMove = PlayerSpeed * deltaTime;

        movement = Vector3.down;

        if (enpause == false)
        {

            if (Input.GetKey(forward))
                movement += Vector3.forward;

            if (Input.GetKey(left))
                movement += Vector3.left;

            if (Input.GetKey(back))
                movement += Vector3.back;

            if (Input.GetKey(right))
                movement += Vector3.right;
        }


        if (Input.GetKeyDown(menu))
        {
            if (enpause == false)
            {
                options.SetActive(true);
                enpause = true;
            }
            else
            {
                options.SetActive(false);
                enpause = false;
            }

        }
        controller.Move(movement * deltaMove);
    
        if (invincibilite && !attenteEnCours)
        {
            StartCoroutine(Attendre3Secondes());
        }

        if (HP <= 0)
        {
            enpause = true;
            death.SetActive(true);
        }

    }

    IEnumerator Attendre3Secondes()
    {
        attenteEnCours = true;

        yield return new WaitForSeconds(1.5f);

        invincibilite = false;
        attenteEnCours = false;
    }
}
