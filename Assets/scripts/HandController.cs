using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandController : MonoBehaviour 
{
	public float max_speed;
	public float acceleration;

	private float speed;
	private Rigidbody rb;

	// Use this for initialization
	void Start()
	{
		rb = GetComponent<Rigidbody>();
		speed = 0;
	}
	
	// Update is called once per frame
	void FixedUpdate()
	{
		bool accelerate_speed = controller_input();

		if(!accelerate_speed)
		{
			speed -= acceleration * Time.deltaTime;
		}

		// upper clamp joystick speed at maxspeed
		if(speed > max_speed)
		{
			speed = max_speed;
		}

		// lower clamp joystick speed to 0
		if(speed < 0)
		{
			speed = 0;
		}
	}

	/* 	
		controller_input polls for joystick movement and 
		is called each frame by Update. If there is joystick
		movement, it will return true, which Update uses
		to know not to decrease the joystick movement speed,
		which resets at 0 after some time.

		This works as intended; for a fencing strip, the movement 
		should mostly be back and forth.
	*/
	private bool controller_input()
	{
		if(OVRInput.Get(OVRInput.Button.PrimaryThumbstickUp))
		{
			if(transform.position.z < 7)
			{
				speed += acceleration * Time.deltaTime * Time.deltaTime;
				Vector3 new_position = transform.position + transform.forward * speed;
				transform.position = new_position;
			}
			return true;
		}

		else if(OVRInput.Get(OVRInput.Button.PrimaryThumbstickDown))
		{
			if(transform.position.z > -7)
			{
				speed += acceleration * Time.deltaTime * Time.deltaTime;
				Vector3 new_position = transform.position - transform.forward * speed;
				transform.position = new_position;
			}
			return true;
		}

		else if(OVRInput.Get(OVRInput.Button.PrimaryThumbstickRight))
		{
			if(transform.position.x < 1)
			{
				speed += acceleration * Time.deltaTime * Time.deltaTime;
				Vector3 new_position = transform.position + transform.right * speed;
				transform.position = new_position;
			}
			return true;
		}

		else if(OVRInput.Get(OVRInput.Button.PrimaryThumbstickLeft))
		{
			if(transform.position.x > -1)
			{
				speed += acceleration * Time.deltaTime * Time.deltaTime;
				Vector3 new_position = transform.position -  transform.right * speed;
				transform.position = new_position;
			}
			return true;
		}

		return false;
	}
}
