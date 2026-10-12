using UnityEngine;

public class Door : MonoBehaviour
{
    public GameObject Player;
    public Movements playerKey;
    public GameObject ManagerOBJ;
    public SceneLoader SceneLoader;

    private void Start()
    {
        Player = GameObject.Find("Controller");
        playerKey = Player.GetComponent<Movements>();
        ManagerOBJ = GameObject.Find("SceneManager");
        SceneLoader = ManagerOBJ.GetComponent<SceneLoader>();
    }

    private void OnTriggerEnter()
    {
        if (playerKey.clé == true)
        {
            SceneLoader.LoadSceneByName("EcranVictoire");
        }
    }
}
