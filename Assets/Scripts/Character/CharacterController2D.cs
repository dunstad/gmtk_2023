using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using System.Collections.Generic;
using System;
using System.Text;

public class CharacterController2D : MonoBehaviour
{
	[SerializeField] private float m_JumpForce = 400f;							// Amount of force added when the player jumps.
	// [Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;	// How much to smooth out the movement
	[SerializeField] private bool m_AirControl;							// Whether or not a player can steer while jumping;
	[SerializeField] private List<LayerMask> m_WhatIsGround;							// A mask determining what is ground to the character
	[SerializeField] private Transform m_GroundCheck;							// A position marking where to check if the player is grounded.
	[SerializeField] private Transform m_CeilingCheck;							// A position marking where to check for ceilings
	[SerializeField] private float maxVerticalSpeed;
	[SerializeField] private float maxHorizontalSpeed;
	[SerializeField] private float maxMovementSpeed;
	[SerializeField] private float acceleration;
	[SerializeField] private float brakeForce;

	const float k_GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded
	private bool m_Grounded;            // Whether or not the player is grounded.
	const float k_CeilingRadius = .2f; // Radius of the overlap circle to determine if the player can stand up
	private Rigidbody2D m_rigidBody;
	private bool m_FacingRight = true;  // For determining which way the player is currently facing.
	private Vector3 velocity = Vector3.zero;
	


	// so variable jump height can't cancel vertical momentum if you didn't jump
	// canceling will be lost on knockback or recoil
	public bool canCancelJump = false;

	private Vector3 startpos;
	private int startHealth;

	private void Awake()
	{
		m_rigidBody = GetComponent<Rigidbody2D>();
	}

	private void Update()
	{
	}

	
	
	private void FixedUpdate()
	{
		m_Grounded = false;

		// The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
		// This can be done using layers instead but Sample Assets will not overwrite your project settings.
		List<Collider2D> colliders = new List<Collider2D>();
		foreach (LayerMask layerIndex in m_WhatIsGround)
		{
			colliders.AddRange(Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, layerIndex));
		}
		for (int i = 0; i < colliders.Count; i++)
		{
			if (colliders[i].gameObject != gameObject)
				m_Grounded = true;
		}

		// limit speed
		float clampedVerticalSpeed = Mathf.Clamp(m_rigidBody.velocity.y, -maxVerticalSpeed, maxVerticalSpeed);
		float clampedHorizontalSpeed = Mathf.Clamp(m_rigidBody.velocity.x, -maxHorizontalSpeed, maxHorizontalSpeed);
		m_rigidBody.velocity = new Vector2(clampedHorizontalSpeed, clampedVerticalSpeed);
		//Debug.Log($"Velocity {m_rigidBody.velocity}, Gravity Scale {m_rigidBody.gravityScale}");

	}
	
	private Vector2 getAim()
	{
		Vector3 mousePosInput = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		Vector2 mousePos = new Vector2(mousePosInput.x, mousePosInput.y);
		Vector2 playerPos = new Vector2(m_rigidBody.position.x, m_rigidBody.position.y);
		Vector2 aim = mousePos - playerPos;
		return aim;
	}


	public void Move(Vector2 move)
	{
		//Vector3 thrustVector = new Vector2(move * 100, 0);
		m_rigidBody.AddForce(move, ForceMode2D.Force);
		Debug.Log($"Move Force: {move}, Rigidbody Speed: {m_rigidBody.velocity}");
		// actually stop when slow
		// without this we roll forever due to frictionless material
	}
	public void Jump(Vector2 jumpDirection)
	{
		Debug.Log($"Jump Force: {m_JumpForce}");
		m_rigidBody.AddForce(jumpDirection * m_JumpForce, ForceMode2D.Impulse);
	}

	private void Flip()
	{
		// Switch the way the player is labelled as facing.
		m_FacingRight = !m_FacingRight;

		// Multiply the player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

	void Start()
	{
		gameObject.tag = "Player";
		gameObject.layer = 6;

		startpos = m_rigidBody.position;
		startHealth = gameObject.GetComponent<Health>().currentHealth;
	}

	public void Reset()
	{
		gameObject.GetComponent<Health>().currentHealth = startHealth;
		gameObject.GetComponent<Rigidbody2D>().position = startpos;
		gameObject.transform.position = startpos;
	}
}
