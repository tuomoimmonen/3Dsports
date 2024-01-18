using UnityEngine.InputSystem;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState { menu, running, jumping, javelin, end };
public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    GameState gameState;

    private void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        instance = this;
    }
    void Start()
    {

    }

    void Update()
    {
        DebugLevels();
    }

    private void DebugLevels()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        else if ((Input.GetKeyDown(KeyCode.T) || Keyboard.current.tKey.wasPressedThisFrame))
        {
            SceneManager.LoadScene((SceneManager.GetActiveScene().buildIndex + 1) % SceneManager.sceneCountInBuildSettings);
        }
        else if(Input.GetKeyDown(KeyCode.Y))
        {
            SceneManager.LoadScene((SceneManager.GetActiveScene().buildIndex - 1) % SceneManager.sceneCountInBuildSettings);
        }
    }




}
