using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    public int currentLevel = 1;
    private MeshTileMap meshTileMap;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Use this for initialization
    void Start () {

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
            meshTileMap = GameObject.FindGameObjectWithTag("MeshTileMap").GetComponent<MeshTileMap>();
            meshTileMap.SetMap("level" + this.currentLevel + ".tmx");
            Debug.Log("load level " + this.currentLevel);
            meshTileMap.GenerateMesh();
            this.currentLevel += 1;
        }
    }
}
