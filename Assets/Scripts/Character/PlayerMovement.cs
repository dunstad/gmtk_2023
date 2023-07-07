using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	public CharacterController2D controller;

	public float runSpeed = 40f;

	float horizontalMove = 0f;
	bool jump = false;
	bool jumpReleased = false;
	
	// Update is called once per frame
	void Update () {

		horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

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
		Debug.Log(horizontalMove);
		// Move our character
		controller.Move(horizontalMove * Time.fixedDeltaTime, jump, jumpReleased);
		jump = false;
		jumpReleased = false;
	}
}