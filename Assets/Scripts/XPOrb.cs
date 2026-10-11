using UnityEngine;

public class XPOrb : MonoBehaviour
{
    [SerializeField] private int xpValue = 1;

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