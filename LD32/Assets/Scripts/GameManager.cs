using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public int currentLevel = 0;
    private MeshTileMap meshTileMap;
    //private int ammoCount;
    private GameObject gameOverMenu;
    public TextAsset[] levels;

    void Awake()
    {
        if (GameObject.FindGameObjectsWithTag("GameManager").Length == 1)
        {
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        
    }

    // Use this for initialization
    void Start () {
        gameOverMenu = GameObject.FindGameObjectWithTag("GameOverMenu");
    }
    
    // Update is called once per frame
    void Update () {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            EndGame(true);
        }
    }

    public void LoadNextLevel()
    {
        Debug.Log("loadLevel");
        if (currentLevel < levels.Length)
        {
            Application.LoadLevel(1);
        } else
        {
            Debug.Log("Fin");
            EndGame("That's all, folks!\nThank you for playing!");
        }
    }

    void OnLevelWasLoaded(int level) {
        if (level == 1)
        {
            gameOverMenu = GameObject.FindGameObjectWithTag("GameOverMenu");

            meshTileMap = GameObject.FindGameObjectWithTag("MeshTileMap").GetComponent<MeshTileMap>();
            meshTileMap.SetMap(levels[currentLevel]);
            //meshTileMap.SetMap("level" + this.currentLevel);
                
            //Debug.Log("load level " + this.currentLevel);
            meshTileMap.GenerateMesh();
            this.currentLevel += 1;
        }
    }

    public void RestartLevel()
    {
        this.currentLevel -= 1;
        LoadNextLevel();
    }

    public void Continue()
    {

    }

    public void MainMenu()
    {
        Application.LoadLevel(0);
        //Destroy(gameObject);
    }

    public void EndGame(bool allowContinue)
    {
        //Debug.Log("Game over!");
        //Time.timeScale = 0f;
        gameOverMenu.SetActive(true);
        
        gameOverMenu.GetComponent<Animator>().enabled = true;
        GameOverMenu gom = gameOverMenu.GetComponent<GameOverMenu>();
        gom.SetMessage("Leaving so soon?");
        gom.ShowContinue();
    }

    public void EndGame(string message)
    {
        //Debug.Log("Game over!");
        //Time.timeScale = 0f;
        gameOverMenu.SetActive(true);

        gameOverMenu.GetComponent<Animator>().enabled = true;
        GameOverMenu gom = gameOverMenu.GetComponent<GameOverMenu>();
        gom.SetMessage(message);
        gom.HideContinue();
        
    }


    public void EndGame()
    {
        //Debug.Log("Game over!");
        //Time.timeScale = 0f;
        gameOverMenu.SetActive(true);

        GameOverMenu gom = gameOverMenu.GetComponent<GameOverMenu>();
        gom.HideContinue();
        gameOverMenu.GetComponent<Animator>().enabled = true;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    /*public void LoseAmmo()
    {
        this.ammoCount -= 1;
    }*/
}
