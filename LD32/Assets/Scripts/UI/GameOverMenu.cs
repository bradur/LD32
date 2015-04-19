using UnityEngine;
using System.Collections;

public class GameOverMenu : MonoBehaviour {

    public float slowDownTime;
    public float speedUpTime;

    public AudioSource music;
    private GameManager gm;

    // Use this for initialization
    void Start () {
        music = GameObject.FindGameObjectWithTag("music").GetComponent<AudioSource>();
        gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }
    
    // Update is called once per frame
    void Update () {
    
    }

    public void SpeedUpTime()
    {
        Debug.Log("Time sped up");
        //Time.timeScale = speedUpTime;
        //music.pitch = speedUpTime;
    }

    public void SlowDownTime()
    {
        Debug.Log("Time slowed down");
        //Time.timeScale = slowDownTime;
        //music.pitch = slowDownTime;
    }

    public void MainMenu()
    {
        gm.MainMenu();
    }

    public void RestartLevel()
    {
        gm.RestartLevel();
    }
}
