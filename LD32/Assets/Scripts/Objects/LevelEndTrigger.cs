using UnityEngine;
using System.Collections;

public class LevelEndTrigger : MonoBehaviour {

    public GameManager gameManager;


    // Use this for initialization
    void Start () {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }
    
    // Update is called once per frame
    void Update () {
    
    }

    void OnCollisionEnter(Collision coll)
    {
        Debug.Log("[end]: collision with " + coll.gameObject.name);
        if (coll.gameObject.tag == "Player")
        {
            gameManager.LoadNextLevel();
        }
        else if (coll.gameObject.tag == "projectile") 
        {
            gameManager.LoadNextLevel();
        }
    }

}
