using NUnit.Framework.Internal;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    private GameObject controller;
    private GameObject options;
    private Movements movements;

    private void Start()
    {
        controller = GameObject.Find("Controller");
        movements = controller.GetComponent<Movements>();
        options = GameObject.Find("Options");
    }

    public void LoadSceneByName(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    // Nouvelle méthode pour fermer le jeu
    public void QuitterJeu()
    {
        Application.Quit(); // Ferme l'application (jeu buildé)

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // Arrête le mode Play dans l'éditeur Unity
#endif
    }

    public void Resume()
    {
        options.SetActive(false);
        movements.enpause = false;
    }
}
