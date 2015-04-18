using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

    Animator animator;
	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "explosion")
        {
            Debug.Log("I've been hit!");
            AnimateDeath();
        }
    }

    void AnimateDeath()
    {
        animator.SetBool("die", true);
    }

    public void Die()
    {
        Destroy(gameObject);
    }
}
