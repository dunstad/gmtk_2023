using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;

public class Hoverboard : MonoBehaviour
{
    private GameObject m_latchedSurface;
	private Collision2D m_lastCollision;

    [field: SerializeField]
    public Rigidbody2D ParentBody { get; set; }
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
    public void OnCollisionStay2D(Collision2D other)
	{
		List<ContactPoint2D> contactPoints = new List<ContactPoint2D>(15);
		int count = other.GetContacts(contactPoints);
		string pointsListStr = contactPoints.ToStringExt();
        ContactPoint2D contactPoint = contactPoints.First();
        //var rotatation = ParentBody.transform.rotation;
        var angle = Vector2.SignedAngle(new Vector2(0,1), contactPoint.normal);
        Debug.Log($"Before set Rotation {angle}");
        ParentBody.rotation = angle;
        //transform.parent.gameObject.GetComponent<Rigidbody2D>();
        
		//Debug.Log($"Contact Point Count: {count}, Contact Points: {pointsListStr}");
	}
	public void OnCollisionEnter2D(Collision2D other)
	{
		m_lastCollision = other;
		m_latchedSurface = other.gameObject;
        Debug.Log($"Collision: {m_lastCollision}");
	}
}
