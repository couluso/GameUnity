using UnityEngine;

public class XPOrb : MonoBehaviour
{
    [SerializeField] private int xpValue = 1;

    private void Awake()
    {
        if (transform.position.x < 0 || transform.position.z < 0 || transform.position.x > 200 || transform.position.z > 200)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerExperience player =
            other.GetComponentInParent<PlayerExperience>();

        if (player != null)
        {
            player.GainXP(xpValue);
            Destroy(gameObject);
        }
    }
}