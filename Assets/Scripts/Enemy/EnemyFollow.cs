using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2f;

    private Transform player;
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();

        // Empêche le monstre de tomber ou de sauter
        rb.useGravity = false;
        rb.constraints =
            RigidbodyConstraints.FreezePositionY |
            RigidbodyConstraints.FreezeRotation;
    }

    private void Start()
    {
        GameObject target = GameObject.FindGameObjectWithTag("Player");

        if (target != null)
            player = target.transform;
    }

    private void FixedUpdate()
    {
        if (player == null)
            return;

        // Direction uniquement sur le plan XZ
        Vector3 direction = player.position - rb.position;
        direction.y = 0f;
        direction.Normalize();

        rb.linearVelocity = direction * moveSpeed;
    }
}