using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed = 12f;
    [SerializeField] private float lifetime = 3f;
    [SerializeField] private int damage = 1;

    private Rigidbody rb;
    private bool launched;

    private GameObject controller;
    public Movements script;

    private void Start()
    {
        controller = GameObject.Find("Controller");
        script = controller.GetComponent<Movements>();
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        rb.isKinematic = false;
        rb.constraints =
            RigidbodyConstraints.FreezePositionY |
            RigidbodyConstraints.FreezeRotation;
    }

    private void Update()
    {

    }


    public void Launch(Vector3 direction)
    {

        direction.y = 0f;

        if (direction.sqrMagnitude < 0.001f)
        {
            Destroy(gameObject);
            return;
        }

        direction.Normalize();
        rb.linearVelocity = direction * speed;
        
        launched = true;
        Destroy(gameObject, lifetime);
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!launched)
            return;

        EnemyHealth enemy = other.GetComponentInParent<EnemyHealth>();

        if (enemy == null)
            return;

        enemy.TakeDamage(damage);
        Destroy(gameObject);
    }
}
