using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameOverMenu : MonoBehaviour {

    public float slowDownTime;
    public float speedUpTime;

    public AudioSource music;
    private GameManager gm;
    public Text message;
    public GameObject continueButton;

    // Use this for initialization
    void Start () {
        music = GameObject.FindGameObjectWithTag("music").GetComponent<AudioSource>();
        gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        //message = GetComponent
    }
    
    // Update is called once per frame
    void Update () {
    
    }

    public void ShowContinue (){
        continueButton.SetActive(true);
    }

    public void HideContinue()
    {
        continueButton.SetActive(false);
    }

    public void SetMessage(string newMessage)
    {
        this.message.text = newMessage;
    }

    public void Continue ()
    {
        gameObject.SetActive(false); //gm.Continue();
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
