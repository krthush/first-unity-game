using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private float restartDelay = 1f;
    [SerializeField] private TimeManager timeManager;
    [SerializeField] private GameObject completeLevelUI;
    private bool gameEnded = false;

    public void EndGame()
    {
        if (!gameEnded)
        {
            gameEnded = true;
            Invoke("Restart", restartDelay);
        }
    }

    public void CompleteLevel ()
    {
        completeLevelUI.SetActive(true);
    }

    private void Restart ()
    {
        timeManager.StopSlowMotion();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void Update()
    {
        if (Input.GetKeyDown("r"))
        {
            Restart();
        }
    }
}
