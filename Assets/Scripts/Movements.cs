using JetBrains.Annotations;
using System.Collections;
using System.Data;
using Unity.VisualScripting;
using UnityEngine;

public class Movements : MonoBehaviour
{

    private CharacterController controller;
    private GameObject player;
    private GameObject options;
    private Vector3 movement = Vector3.zero;

    public bool enpause = false;

    //Mouvements
    [SerializeField] KeyCode forward = KeyCode.W;
    [SerializeField] KeyCode left = KeyCode.A;
    [SerializeField] KeyCode back = KeyCode.S;
    [SerializeField] KeyCode right = KeyCode.D;
    [SerializeField] KeyCode menu = KeyCode.Escape;

    public float PlayerSpeed = 12f;

    public int HP = 10;


    private void Start()
    {
        player = GameObject.Find("Player");
        controller = player.GetComponent<CharacterController>();
        options = GameObject.Find("Options");
    }

    // Update is called once per frame
    void Update()
    {
        if (enpause == false)
        
            options.SetActive(false);
        
        else
            options.SetActive(true);

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
    }
}
