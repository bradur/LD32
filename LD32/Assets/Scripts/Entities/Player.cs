using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    public float rotationMagnitude;

    private Vector3 rotation;
    bool shotInTheAir = false;
    public float projectileSpeed;
    private Projectile projectile;
    private int sizeX;
    private int sizeY;

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

    public void Teleport(Vector3 pos)
    {
        Vector3 newpos = new Vector3((int)Mathf.Round(pos.x), 0.5f, (int)Mathf.Round(pos.z));
        transform.position = pos;
        this.shotInTheAir = false;
    }

    public void Shoot(){
        if (!shotInTheAir)
        {
            this.shotInTheAir = true;
            //Vector3 projectilePos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            Vector3 projectilePos = transform.forward * 2;
            GameObject projectileObject = (GameObject)Instantiate(Resources.Load("Projectile"), transform.position, transform.rotation);
            Rigidbody rigidbody = projectileObject.GetComponent<Rigidbody>();
            this.projectile = projectileObject.GetComponent<Projectile>();
            this.projectile.player = this;
            rigidbody.AddForce(-transform.forward * projectileSpeed);
        }
    }

    public void Spawn(Vector3 pos, int x, int y)
    {
        this.sizeX = x;
        this.sizeY = y;
        transform.position = new Vector3(pos.x, transform.position.y, pos.z);
    }
}
