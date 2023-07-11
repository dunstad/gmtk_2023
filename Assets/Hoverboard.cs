using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;

public class Hoverboard : MonoBehaviour
{
    private GameObject m_latchedSurface;
    private GameObject LatchedSurface 
    { 
        get
        {
            return m_latchedSurface;
        }
        set
        {
            if(value == null && m_latchedSurface != null)
            {
                Debug.Log($"Leaving latched surface {m_latchedSurface.ToString()}");
            }
            if(m_latchedSurface == null && value != null)
            {
                Debug.Log($"Attaching to surface {value.ToString()}");
            }
            m_latchedSurface = value;
        }
    }
	private Collision2D m_lastCollision;
    private Vector2 m_boardDirection = new Vector2(1,0).normalized;
    private Vector2 m_boardNormal;
    [field: SerializeField]
    public float LatchDistance { get; set; }
    [field: SerializeField]
    public float BoardSpeed { get; set; }

    [field: SerializeField]
    public float ThrustForce { get; set; }

    [field: SerializeField]
    public Rigidbody2D ParentBody { get; set; }

    public float attachmentStrength;

    public Vector2 BoardDirection 
    { 
        get
        {
            return m_boardDirection;
        } 
        set
        {
            m_boardDirection = value.normalized;
        } 
    }
    public Vector2 BoardNormal 
    { 
        get
        {
            return m_boardNormal;
        } 
        set
        {
            m_boardNormal = value.normalized;
            BoardDirection = Vector2.Perpendicular(m_boardNormal * -1);
        } 
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void FixedUpdate() 
    {
        Vector2 downBoardForce = BoardNormal * -1 * attachmentStrength;
        if(IsAttachedToSurface())
        {
            ParentBody.angularVelocity = 0f;
            // prevent bouncing away from attached surface
            ParentBody.velocity = Vector2.Dot(ParentBody.velocity, BoardDirection) * BoardDirection;
            ParentBody.AddForce(downBoardForce);
        }
        else
        {
            int layerMask = 1 << LayerMask.NameToLayer("Terrain");
            RaycastHit2D hit = Physics2D.Raycast(ParentBody.position, downBoardForce, 10, layerMask);
            if(hit.collider != null)
            {
                Debug.Log($"Not attached, but raycast hit {hit.collider.name}");
                if(hit.distance < LatchDistance)
                {
                    Debug.Log($"Not Latched on, but close enough to apply the force: Distance = {hit.distance}, Collider = {hit.collider}");
                    ParentBody.AddForce(downBoardForce);
                    LatchedSurface = hit.collider.gameObject;
                }
            }
            else
            {
                LatchedSurface = null;
            }
            
        }
    }

    public bool IsAttachedToSurface()
    {
        if(LatchedSurface == null)
        {
            return false;
        }
        return true;
    }
    public void RotateClockwise(float degrees)
    {
        float newAngle = ParentBody.rotation + degrees;
        //Vector3 newBoardNormal3 = Quaternion.Euler(0, 0, degrees) * BoardNormal;
        Vector3 newBoardNormal3 = Quaternion.Euler(0,0, newAngle) * new Vector3(0,1,0);
        Vector2 newBoardNormal2 = new Vector2(newBoardNormal3.x, newBoardNormal3.y);
        //Debug.Log($"Board Normal: {BoardNormal}, New Board Normal: {newBoardNormal2}, New Board Normal 3: {newBoardNormal3}");
        BoardNormal = newBoardNormal2.normalized;
        ParentBody.angularVelocity = 0f;
        ParentBody.MoveRotation(newAngle);
    }
    public void RotateCounterClockwise(float degrees)
    {
        RotateClockwise(-1 * degrees);
    }
    public void SetRotationTo(Vector2 newDirection)
    {
        var angle = Vector2.SignedAngle(new Vector2(0,1), newDirection);
        //Debug.Log($"Before set Rotation {ParentBody.rotation}, Setting to {angle}");
        ParentBody.angularVelocity = 0f;
        ParentBody.MoveRotation(angle);
        //ParentBody.rotation = angle;
        BoardNormal = newDirection;
        //BoardDirection = Vector2.Perpendicular(newDirection * -1);
    }
    public void OnCollisionExit2D(Collision2D other) 
	{
		m_lastCollision = null;
        LatchedSurface = null;
        ParentBody.angularVelocity = 0;
        Debug.Log($"Collision Exit");
	}
    public void OnCollisionStay2D(Collision2D other)
	{
        Vector2 downFromBoard = new Vector2(BoardDirection.y, -BoardDirection.x);
        int layerMask = 1 << LayerMask.NameToLayer("Terrain");
        RaycastHit2D hit = Physics2D.Raycast(ParentBody.position, downFromBoard, Mathf.Infinity, layerMask);
        // Debug.Log($"Raycast Origin: {ParentBody.position}, Direction {downFromBoard}, Hit Normal: {hit.normal}, LayerMask {layerMask}");
        // Debug.Log($"Hit dist: {hit.distance}, Collider Obj: {hit.collider.gameObject}");
        //Debug.Log($"BoardDirection {BoardDirection} Down from Board {downFromBoard}, Raycast Hit {hit.normal}");
		var normal = hit.normal;
        if(normal != BoardNormal)
        {  
            SetRotationTo(normal);
        }
        //SetRotationTo(contactPoint.normal);
	}
	public void OnCollisionEnter2D(Collision2D other)
	{
        m_lastCollision = other;
        Vector2 downFromBoard = new Vector2(BoardDirection.y, -BoardDirection.x);
        RaycastHit2D hit = Physics2D.Raycast(ParentBody.centerOfMass, downFromBoard);
		var normal = hit.normal;
        LatchedSurface = hit.collider.gameObject;
        // Debug.Log($"Raycast Origin: {ParentBody.position}, Direction {downFromBoard}, Hit Normal: {hit.normal}, LayerMask {layerMask}");
        // Debug.Log($"Hit dist: {hit.distance}, Collider Obj: {hit.collider.gameObject}");
        // Debug.Log($"BoardDirection {BoardDirection} Down from Board {downFromBoard}, Raycast Hit {hit.normal}");
        if(normal != BoardNormal)
        {  
            SetRotationTo(normal);
        }
	}
}
