using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2f;

    private Transform player;
    private GameObject controller;
    public Movements script;
    private Rigidbody rb;

    private void Start()
    {
        GameObject target = GameObject.FindGameObjectWithTag("Player");
        controller = GameObject.Find("Controller");
        script = controller.GetComponent<Movements>();
        player = target.transform;
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        // Empêche le monstre de tomber ou de sauter
        rb.useGravity = false;
        rb.constraints =
            RigidbodyConstraints.FreezePositionY |
            RigidbodyConstraints.FreezeRotation;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {

            if (script.invincibilite == false)
            {
                script.HP -= 4;
                script.invincibilite = true;
                Destroy(gameObject);
            }
            else
                Destroy(gameObject);
        }

    }


    private void FixedUpdate()
    {
        if (script.enpause == true)
            moveSpeed = 0f;
        else
            moveSpeed = 2f;
        
        
        if (player == null)
            return;

            // Direction uniquement sur le plan XZ
        Vector3 direction = player.position - rb.position;
        direction.y = 0f;
        direction.Normalize();

        rb.linearVelocity = direction * moveSpeed;

        
        
    }
}