using UnityEngine;

public class Key : MonoBehaviour
{
    public GameObject Player;
    public Movements playerKey;

    private void Awake()
    {
        Player = GameObject.Find("Controller");
        playerKey = Player.GetComponent<Movements>();
    }

    private void OnTriggerEnter(Collider other)
    {

        if (playerKey != null)
        {
            playerKey.clé = true;
            Destroy(gameObject);
        }
    }
}