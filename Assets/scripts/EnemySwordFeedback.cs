using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySwordFeedback : MonoBehaviour
{
	public ScoreManager score_manager;
	// Use this for initialization
	void Start()
	{
		
	}
	
	// Update is called once per frame
	void Update()
	{
		
	}

	void OnCollisionEnter(Collision collision)
	{
		Debug.Log("Collision enter");
		if(collision.collider.gameObject.tag == "PlayerScoringPortion ")
		{
			OVRInput.SetControllerVibration(0.2f, 0.2f, OVRInput.Controller.RTouch);
			Debug.Log("Swords collided");
		}

		if(collision.collider.gameObject.tag == "Player")
		{
			OVRInput.SetControllerVibration(0.5f, 0.5f, OVRInput.Controller.LTouch);
			Debug.Log("Enemy score should be increased");
			score_manager.increase_enemy_score();
		}
	}
}
