using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	public CharacterController2D controller;
	[field: SerializeField]
	public Hoverboard Hoverboard { get; set; }
	public float runSpeed = 50f;

	float horizontalMove = 0f;
	bool jump = false;
	bool jumpReleased = false;
	bool isAttached;
	
	// Update is called once per frame
	void Update () {

		horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
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
		controller.Move(horizontalMove * Time.fixedDeltaTime, jump, jumpReleased);
		jump = false;
		jumpReleased = false;
	}
}
