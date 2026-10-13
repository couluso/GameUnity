using UnityEngine;

public class Door : MonoBehaviour
{
    public GameObject Player;
    public Movements playerKey;
    public GameObject ManagerOBJ;
    public SceneLoader SceneLoader;

    public GameObject TexteNON;

    private void Start()
    {
        Player = GameObject.Find("Controller");
        playerKey = Player.GetComponent<Movements>();
        ManagerOBJ = GameObject.Find("SceneManager");
        SceneLoader = ManagerOBJ.GetComponent<SceneLoader>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (playerKey.clé == true)
            {
                SceneLoader.LoadSceneByName("EcranVictoire");
            }
            else
            {
                TexteNON.SetActive(true);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        TexteNON.SetActive(false);
    }
}
