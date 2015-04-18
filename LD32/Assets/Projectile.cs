using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

    public Player player;
    private Rigidbody rigidbody;
    private Vector3 lastPos;
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
        else if (coll.gameObject.tag == "mirror")
        {
           /*foreach (ContactPoint contactPoint in coll.contacts)
           {
              Quaternion newVelocity = Quaternion.AngleAxis(180, contactPoint.normal) * transform.forward * -1;
           }
           transform.rotation = newVelocity;*/
            RaycastHit hit;
            if(Physics.Raycast(transform.position, lastPos, out hit, 100f)){
                rigidbody.velocity = (Vector3.Reflect(hit.point, hit.normal));
            }

           //rigidbody.velocity = Vector3.Reflect(transform.position, Vector3.right);
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
        lastPos = transform.position;
    }
}
