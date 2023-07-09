using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	public CharacterController2D controller;
	[field: SerializeField]
	public Hoverboard Hoverboard { get; set; }
	public float moveSpeed = 50f;

	float horizontalMove = 0f;
	bool jump = false;
	bool jumpReleased = false;
	bool isAttached;
	
	// Update is called once per frame
	void Update () {

		horizontalMove = Input.GetAxisRaw("Horizontal") * moveSpeed;
		isAttached = Hoverboard.IsAttachedToSurface();
		if (Input.GetButtonDown("Jump"))
		{
			jump = true;
		}
		if (Input.GetButtonUp("Jump"))
		{
			jumpReleased = true;
		}

	}

	void FixedUpdate ()
	{
		// Move our character
		// if(isAttached)
		// {
		// 	var move = Hoverboard.BoardDirection * moveSpeed * Time.fixedDeltaTime;
		// 	controller.Move(move, jump, jumpReleased);
		// }
		controller.Move(100 * horizontalMove * Time.fixedDeltaTime * Vector2.right, jump, jumpReleased);
		jump = false;
		jumpReleased = false;
	}
	private void OnCollisionStay2D(Collision2D other)
	{
		Hoverboard.OnCollisionStay2D(other);
	}
	private void OnCollisionEnter2D(Collision2D other)
	{
		Hoverboard.OnCollisionEnter2D(other);
	}
}
