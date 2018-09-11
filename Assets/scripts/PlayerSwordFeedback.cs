using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSwordFeedback : MonoBehaviour
{

	// Use this for initialization
	void Start()
	{
		
	}
	
	// Update is called once per frame
	void Update()
	{
		
	}

	void OnCollilsionEnter(Collision collision)
	{
		Debug.Log("Collision enter");
		if(collision.collider.gameObject.tag == "EnemyScoringPortion")
		{
			OVRInput.SetControllerVibration(0.2f, 0.2f, OVRInput.Controller.RTouch);
			Debug.Log("Swords collided");
		}

		if(collision.collider.gameObject.name == "Sword_09")
		{
			OVRInput.SetControllerVibration(0.5f, 0.5f, OVRInput.Controller.LTouch);
			Debug.Log("SWORDS COLLIDED");
		}
	}
}
