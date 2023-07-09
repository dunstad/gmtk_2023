using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;

public class Hoverboard : MonoBehaviour
{
    private GameObject m_latchedSurface;
	private Collision2D m_lastCollision;
    private Vector2 m_boardDirection = new Vector2(1,0).normalized;
    private Vector2 m_boardNormal;

    [field: SerializeField]
    public Rigidbody2D ParentBody { get; set; }

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
        
    }

    public bool IsAttachedToSurface()
    {
        if(m_latchedSurface == null)
        {
            return false;
        }
        return true;
    }
    public void RotateClockwise(float degrees)
    {
        ParentBody.MoveRotation(ParentBody.rotation + degrees);
    }
    public void RotateCounterClockwise(float degrees)
    {
        RotateClockwise(-1 * degrees);
    }
    public void SetRotationTo(Vector2 newDirection)
    {
        var angle = Vector2.SignedAngle(new Vector2(0,1), newDirection);
        //Debug.Log($"Before set Rotation {ParentBody.rotation}, Setting to {angle}");
        ParentBody.MoveRotation(angle);
        //ParentBody.rotation = angle;
        BoardNormal = newDirection;
        BoardDirection = Vector2.Perpendicular(newDirection * -1);
    }
    public void OnCollisionExit2D(Collision2D other) 
	{
		m_lastCollision = null;
        m_latchedSurface = null;
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
        m_latchedSurface = hit.collider.gameObject;
        // Debug.Log($"Raycast Origin: {ParentBody.position}, Direction {downFromBoard}, Hit Normal: {hit.normal}, LayerMask {layerMask}");
        // Debug.Log($"Hit dist: {hit.distance}, Collider Obj: {hit.collider.gameObject}");
        // Debug.Log($"BoardDirection {BoardDirection} Down from Board {downFromBoard}, Raycast Hit {hit.normal}");
        if(normal != BoardNormal)
        {  
            SetRotationTo(normal);
        }
	}
}
