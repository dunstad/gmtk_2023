using UnityEngine.InputSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	public CharacterController2D controller;
	[field: SerializeField]
	public Hoverboard Hoverboard { get; set; }
	public float moveSpeed = 50f;
	[field: SerializeField]
	public float AngularSpeed { get; set; }

	float horizontalMove = 0f;
	bool isAttached;
	bool rotateCCW = false;
	bool rotateCW = false;
	bool jumpFlip = false;
	bool boostThrust = false;
	bool brakes = false;
	bool gas = false;
	
	// Update is called once per frame
	void Update () {
	}

	void FixedUpdate ()
	{
		horizontalMove = (gas ? 1 : 0) + (brakes ? -1 : 0);
		isAttached = Hoverboard.IsAttachedToSurface();
		//Debug.Log($"Horizontal Move: {horizontalMove}, IsAttached {isAttached}");
		// Move our character
		if(isAttached)
		{
			var move =  400 * horizontalMove * Hoverboard.BoardDirection * moveSpeed * Time.fixedDeltaTime;
			//Debug.Log($"Is Attached Move: {move}");
			controller.Move(move);
			if(jumpFlip)
			{
				jumpFlip = false;
				controller.Jump(Hoverboard.BoardNormal);
			}
		}
		else
		{
			//Debug.Log($"RotateCW {rotateCW}, RotateCCW {rotateCCW}");
			if(rotateCW && !rotateCCW)
			{
				//Debug.Log("Rotating CW");
				Hoverboard.RotateClockwise(AngularSpeed);
			}
			if(rotateCCW && !rotateCW)
			{
				//Debug.Log("Rotating CCW");
				Hoverboard.RotateCounterClockwise(AngularSpeed);
			}
		}

		//controller.Move(10 * horizontalMove * Time.fixedDeltaTime * Vector2.right);
	}
	private void OnBoostThrust(InputValue value)
	{
		boostThrust = value.isPressed;
		string onOffStr = value.isPressed ? "On" : "Off" ;
		Debug.Log($"BoostThrust {onOffStr}");
	}
	private void OnJumpFlip(InputValue value)
	{
		jumpFlip = value.isPressed;
		string onOffStr = value.isPressed ? "On" : "Off" ;
		Debug.Log($"JumpFlip {onOffStr}");
	}
	private void OnRotateCCW(InputValue value)
	{
		rotateCCW = value.isPressed;
		string onOffStr = value.isPressed ? "On" : "Off" ;
		Debug.Log($"RotateCCW {onOffStr}");
	}
	private void OnRotateCW(InputValue value)
	{
		rotateCW = value.isPressed;
		string onOffStr = value.isPressed ? "On" : "Off" ;
		Debug.Log($"RotateCW {onOffStr}");
	}
	private void OnGas(InputValue value)
	{
		gas = value.isPressed;
		rotateCW = value.isPressed;
		string onOffStr = value.isPressed ? "On" : "Off" ;
		Debug.Log($"Gas {onOffStr}");
	}
	private void OnBrakes(InputValue value)
	{
		brakes = value.isPressed;
		rotateCCW = value.isPressed;
		string onOffStr = value.isPressed ? "On" : "Off" ;
		Debug.Log($"Brakes {onOffStr}");
	}
	private void OnCollisionExit2D(Collision2D other) 
	{
		Hoverboard.OnCollisionExit2D(other);
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
