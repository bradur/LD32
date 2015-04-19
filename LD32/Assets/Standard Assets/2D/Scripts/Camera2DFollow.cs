using System;
using UnityEngine;

public class Camera2DFollow : MonoBehaviour
{
    public Transform target;
    /*public float damping = 1;
    public float lookAheadFactor = 3;
    public float lookAheadReturnSpeed = 0.5f;
    public float lookAheadMoveThreshold = 0.1f;
    */

    public float maxDistance = 5f;

    /*private float m_OffsetZ;
    private Vector3 m_LastTargetPosition;
    private Vector3 m_CurrentVelocity;
    private Vector3 m_LookAheadPos;*/
    private Vector3 newPos;
    public Vector3 defaultPositions;

    // Use this for initialization
    private void Start()
    {
        //UpdatePos();
    }


    /*public void UpdatePos()
    {
        //m_LastTargetPosition = target.position;
       // m_OffsetZ = (transform.position - target.position).z;
        //transform.position = target.position; transform.parent = null;
    }*/

    // Update is called once per frame
    private void Update()
    {
        // only update lookahead pos if accelerating or changed direction
        //float xMoveDelta = (target.position - m_LastTargetPosition).x;
        //float zMoveDelta = (target.position - m_LastTargetPosition).z;

        //bool updateLookAheadTarget = Mathf.Abs(xMoveDelta) > lookAheadMoveThreshold;

        /*if (updateLookAheadTarget)
        {
            m_LookAheadPos = lookAheadFactor*Vector3.right*Mathf.Sign(xMoveDelta);
        }
        else
        {
            m_LookAheadPos = Vector3.MoveTowards(m_LookAheadPos, Vector3.zero, Time.deltaTime*lookAheadReturnSpeed);
        }*/

        if (transform.position != target.position)
        {
            newPos = Vector3.MoveTowards(transform.position, target.position, this.maxDistance);
            float x = defaultPositions.x != 0 ? defaultPositions.x : newPos.x;
            float y = defaultPositions.y != 0 ? defaultPositions.y : newPos.y;
            float z = defaultPositions.z != 0 ? defaultPositions.z : newPos.z;
            transform.position = new Vector3(x, y, z);
        }

        //Vector3 aheadTargetPos = target.position + m_LookAheadPos + Vector3.forward*m_OffsetZ;
        //Vector3 newPos = Vector3.SmoothDamp(transform.position, aheadTargetPos, ref m_CurrentVelocity, damping);

        //transform.position = new Vector3(newPos.x, transform.position.y, newPos.z);

        //m_LastTargetPosition = target.position;
    }
}
