using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    public float rotationMagnitude;

    private Vector3 rotation;
    bool shotInTheAir = false;
    public float projectileSpeed;
    public float projectileLifeTime = 10f;
    private int sizeX;
    private int sizeY;
    public int ammoCount = 2;
    public GameManager gameManager;
    public AnimatedText ammoCountDisplay;
    private float projectileTime;

    public AudioSource soundTeleport;
    public AudioSource soundShoot;
    public AudioSource soundPowerup;

    // Use this for initialization
    void Start () {
        rotation = new Vector3(0f, rotationMagnitude, 0f);
        ammoCountDisplay.SetCount(this.ammoCount);
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
        if(shotInTheAir){
            projectileTime -= Time.deltaTime;
            if (projectileTime <= 0f)
            {
                Destroy(GameObject.FindGameObjectWithTag("projectile"));
                gameManager.EndGame();
            }
        }
    }

    IEnumerator WeaponCoolDown()
    {
        yield return new WaitForSeconds(1);
        if (this.ammoCount == 0)
        {
            gameManager.EndGame();
        }
    }

    public void Teleport(Vector3 pos)
    {
        Vector3 newpos = new Vector3((int)Mathf.Round(pos.x), 0.5f, (int)Mathf.Round(pos.z));
        transform.position = pos;
        this.shotInTheAir = false;
        soundTeleport.Play();
        //StartCoroutine(WeaponCoolDown());
    }

    public void AddAmmo()
    {
        this.ammoCount += 1;
        ammoCountDisplay.Animate(1);
        soundPowerup.Play();
    }

    /*public void SetAmmo(int ammo)
    {
        this.ammoCount = ammo;
        ammoCountDisplay.Animate(this.ammoCount);
    }*/

    public void Shoot(){
        if (!shotInTheAir)
        {
            if (this.ammoCount == 0)
            {
                StartCoroutine(WeaponCoolDown());
            }
            else
            {
                soundShoot.Play();
                this.shotInTheAir = true;
                projectileTime = projectileLifeTime;
                this.ammoCount -= 1;
                ammoCountDisplay.Animate(-1);
                //gameManager.LoseAmmo();
                //Vector3 projectilePos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
                GameObject projectileObject = (GameObject)Instantiate(Resources.Load("Projectile"), transform.position, transform.rotation);
                Projectile projectile = projectileObject.GetComponent<Projectile>();
                projectile.Init(this);
                projectile.Shoot(-transform.forward * projectileSpeed);
            }
        }
    }

    public void Spawn(Vector3 pos, int x, int y)
    {
        this.sizeX = x;
        this.sizeY = y;
        transform.position = new Vector3(pos.x, transform.position.y, pos.z);
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    void OnCollisionEnter(Collision collision){
        if (collision.gameObject.tag == "hole")
        {
            gameManager.EndGame();
        }
    }
}
