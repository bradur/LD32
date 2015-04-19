using UnityEngine;
using System.Collections;

public class Explosion : MonoBehaviour {

    private Animator animator;
    //private Projectile projectile;
    // Use this for initialization
    void Start () {
        
    }
    
    // Update is called once per frame
    void Update () {
    
    }

    public void Explode()
    {
        this.animator = GetComponent<Animator>();
        this.animator.SetBool("explode", true);
    }

    public void EndExplosion()
    {
        //Debug.Log("End");
        this.animator.SetBool("explode", false);
        //this.projectile.Hide();
        
        Destroy(gameObject);
    }
}
