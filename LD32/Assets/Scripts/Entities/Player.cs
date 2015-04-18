using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    public float rotationMagnitude;

    private Vector3 rotation;

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
    }

    public void Spawn(Vector3 pos)
    {
        transform.position = pos;
    }
}
