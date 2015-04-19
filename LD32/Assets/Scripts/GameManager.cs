using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public int currentLevel = 1;
    private MeshTileMap meshTileMap;
    //private int ammoCount;
    private GameObject gameOverMenu;

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
    
    }

    public void LoadNextLevel()
    {
        Application.LoadLevel(1);
    }

    void OnLevelWasLoaded(int level) {
        if (level == 1)
        {
            Time.timeScale = 1f;
            meshTileMap = GameObject.FindGameObjectWithTag("MeshTileMap").GetComponent<MeshTileMap>();
            meshTileMap.SetMap("level" + this.currentLevel + ".tmx");
            gameOverMenu = GameObject.FindGameObjectWithTag("GameOverMenu");
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

    public void MainMenu()
    {
        Application.LoadLevel(0);
        //Destroy(gameObject);
    }

    public void EndGame()
    {
        Debug.Log("Game over!");
        //Time.timeScale = 0f;
        gameOverMenu.SetActive(true);
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
