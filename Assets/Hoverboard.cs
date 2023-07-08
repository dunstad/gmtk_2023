using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hoverboard : MonoBehaviour
{
    private GameObject m_latchedSurface;
	private Collision2D m_lastCollision;
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
        return true;
    }
    private void OnCollisionStay2D(Collision2D other) 
	{
		List<ContactPoint2D> contactPoints = new List<ContactPoint2D>(15);
		int count = other.GetContacts(contactPoints);
		string pointsListStr = contactPoints.ToStringExt();
		//Debug.Log($"Contact Point Count: {count}, Contact Points: {pointsListStr}");
	}
	private void OnCollisionEnter2D(Collision2D other) 
	{
		m_lastCollision = other;
		m_latchedSurface = other.gameObject;
		Debug.Log($"Collision: {m_lastCollision}");
	}
}
