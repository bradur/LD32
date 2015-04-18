using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

    public Player player;
    // Use this for initialization
    void Start () {
    
    }
    
    // Update is called once per frame
    void Update () {
    
    }

    void OnCollisionEnter(Collision coll)
    {
        if (coll.gameObject.tag == "wall")
        {
            player.Teleport(transform.position);
            Debug.Log("[projectilepos]: " + transform.position);
            Destroy(gameObject);
        }
    }
}
