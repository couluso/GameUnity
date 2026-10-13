using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    [SerializeField] public float moveSpeed; 
    public int degats = 4;
    public int augmentationDegatsParVague = 1;

    private Transform player;
    private GameObject controller;
    private GameObject Manager;
    public Movements script;
    public EnemySpawner spawner;
    private Rigidbody rb;

    private void Start()
    {
        GameObject target = GameObject.FindGameObjectWithTag("Player");
        controller = GameObject.Find("Controller");
        Manager = GameObject.Find("GameManager");
        script = controller.GetComponent<Movements>();
        spawner = Manager.GetComponent<EnemySpawner>();
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
        
        moveSpeed = 2f;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {

            if (script.invincibilite == false)
            {
                script.HP -= degats;
                script.invincibilite = true;
            }
            if (this.CompareTag("Enemy"))
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