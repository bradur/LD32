using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    public float rotationMagnitude;

    private Vector3 rotation;
    bool shotInTheAir = false;
    public float projectileSpeed;
    private Projectile projectile;

    // Use this for initialization
    void Start () {
        rotation = new Vector3(0f, rotationMagnitude, 0f);
    }
    
    // Update is called once per frame
    void Update () {
        if(Input.GetKey(KeyCode.LeftArrow)) {
            transform.Rotate(-rotation);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Rotate(rotation);
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            Shoot();
        }
    }

    public void Shoot(){
        if (!shotInTheAir)
        {
            shotInTheAir = true;
            Vector3 projectilePos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            GameObject projectileObject = (GameObject)Instantiate(Resources.Load("Projectile"), transform.position, transform.rotation);
            Rigidbody rigidbody = projectileObject.GetComponent<Rigidbody>();
            projectile = projectileObject.GetComponent<Projectile>();
            rigidbody.AddForce(-transform.forward * projectileSpeed);
        }
    }

    public void Spawn(Vector3 pos)
    {
        transform.position = new Vector3(pos.x, transform.position.y, pos.z);
    }
}
