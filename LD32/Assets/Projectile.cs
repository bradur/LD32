using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

    public Player player;
    private Rigidbody rigidbody;
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
            this.player.Teleport(transform.position);
            //Debug.Log("[projectilepos]: " + transform.position);
            GameObject explosionObject = (GameObject)Instantiate(Resources.Load("explosion"), transform.position, transform.rotation);
            Explosion explosion = explosionObject.GetComponent<Explosion>();
            explosion.Explode();
            Hide();
            //Debug.Log("Explode");
            //Destroy(gameObject);
        }
    }

    public void Init(Player player)
    {
        this.player = player;
        this.rigidbody = GetComponent<Rigidbody>();
        //Hide();
    }

    public void Hide()
    {
        Destroy(gameObject);
        //gameObject.SetActive(false);
    }

    public void Shoot(Vector3 force)
    {
        //gameObject.SetActive(true);
        rigidbody.AddForce(force);
    }
}
