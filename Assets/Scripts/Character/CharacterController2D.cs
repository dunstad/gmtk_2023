using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using System.Collections.Generic;
using System;

public class CharacterController2D : MonoBehaviour
{
	[SerializeField] private float m_JumpForce = 400f;							// Amount of force added when the player jumps.
	// [Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;	// How much to smooth out the movement
	[SerializeField] private bool m_AirControl = false;							// Whether or not a player can steer while jumping;
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
	private Rigidbody2D m_Rigidbody2D;
	private bool m_FacingRight = true;  // For determining which way the player is currently facing.
	private Vector3 velocity = Vector3.zero;

	protected bool activated1 = false;
	protected bool activated2 = false;


	// so variable jump height can't cancel vertical momentum if you didn't jump
	// canceling will be lost on knockback or recoil
	public bool canCancelJump = false;

	private Vector3 startpos;
	private int startHealth;

	private void Awake()
	{
		m_Rigidbody2D = GetComponent<Rigidbody2D>();
	}

	private void Update()
	{

		if (activated1)
		{
			Activate1();
		}
		if (activated2)
		{
			Activate2();
		}
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
		float clampedVerticalSpeed = Mathf.Clamp(m_Rigidbody2D.velocity.y, -maxVerticalSpeed, maxVerticalSpeed);
		float clampedHorizontalSpeed = Mathf.Clamp(m_Rigidbody2D.velocity.x, -maxHorizontalSpeed, maxHorizontalSpeed);
		m_Rigidbody2D.velocity = new Vector2(clampedHorizontalSpeed, clampedVerticalSpeed);

	}
	
	private Vector2 getAim()
	{
		Vector3 mousePosInput = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		Vector2 mousePos = new Vector2(mousePosInput.x, mousePosInput.y);
		Vector2 playerPos = new Vector2(m_Rigidbody2D.position.x, m_Rigidbody2D.position.y);
		Vector2 aim = mousePos - playerPos;
		return aim;
	}


	public void Move(float move, bool jump, bool jumpReleased)
	{
		//only control the player if grounded or airControl is turned on
		if (m_Grounded || m_AirControl)
		{

			// Move the character by finding the target velocity
			Vector3 targetVelocity = new Vector3(move * acceleration * m_Rigidbody2D.mass, 0f, 0f);

			// if not at top speed
			if (Math.Abs(m_Rigidbody2D.velocity.x) < maxHorizontalSpeed)
			{
				m_Rigidbody2D.AddForce(targetVelocity, ForceMode2D.Force);
			}

			// brake force
			float moveSign = move / Math.Abs(move);
			float xSign = m_Rigidbody2D.velocity.x / Math.Abs(m_Rigidbody2D.velocity.x);
			bool movingOppositeMomentum = moveSign != xSign && Math.Abs(m_Rigidbody2D.velocity.x) > .05f;
			if (movingOppositeMomentum)
			{
				m_Rigidbody2D.AddForce(new Vector3(-xSign * (brakeForce * m_Rigidbody2D.mass), 0f, 0f), ForceMode2D.Force);
			}
			// brake down from bonus recoil speed (or other) to normal speed
			bool overTopSpeed = Math.Abs(m_Rigidbody2D.velocity.x) > maxMovementSpeed;
			if (overTopSpeed)
			{
				m_Rigidbody2D.AddForce(new Vector3(-xSign * .3f * (brakeForce * m_Rigidbody2D.mass), 0f, 0f), ForceMode2D.Force);
			}

			// actually stop when slow
			// without this we roll forever due to frictionless material
			if (Math.Abs(m_Rigidbody2D.velocity.x) < 1f)
			{
				m_Rigidbody2D.velocity = new Vector2(0f, m_Rigidbody2D.velocity.y);
			}

			// If the input is moving the player right and the player is facing left...
			if (move > 0 && !m_FacingRight)
			{
				// ... flip the player.
				Flip();
			}
			// Otherwise if the input is moving the player left and the player is facing right...
			else if (move < 0 && m_FacingRight)
			{
				// ... flip the player.
				Flip();
			}
		}
		// If the player should jump...
		if (m_Grounded && jump)
		{
			// Add a vertical force to the player.
			m_Grounded = false;
			canCancelJump = true;
			m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
		}

		// variable jump height
		if (canCancelJump && jumpReleased)
		{
			if (m_Rigidbody2D.velocity.y > 0f)
			{
				m_Rigidbody2D.velocity = new Vector3(m_Rigidbody2D.velocity.x, 0f, 0f);
			}
			
		}

		// to prevent jump cancelling from being used to stop falls
		if (m_Rigidbody2D.velocity.y < -.5f)
		{
			canCancelJump = false;
		}
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
		activated1 = false;
		activated2 = false;

		startpos = m_Rigidbody2D.position;
		startHealth = gameObject.GetComponent<Health>().currentHealth;
	}

	public void Reset()
	{
		gameObject.GetComponent<Health>().currentHealth = startHealth;
		gameObject.GetComponent<Rigidbody2D>().position = startpos;
		gameObject.transform.position = startpos;
	}

	void OnActivate1(InputValue value)
	{
		activated1 = value.isPressed;
	}

	void Activate1()
	{
		Debug.Log("activate1");
	}

	void OnActivate2(InputValue value)
	{
		activated2 = value.isPressed;
	}

	void Activate2()
	{
		Debug.Log("activate2");
	}

	void OnPickUp1(InputValue value)
	{
		Debug.Log("pick up 1");
	}

	void OnPickUp2(InputValue value)
	{
		Debug.Log("pick up 2");
	}
}
