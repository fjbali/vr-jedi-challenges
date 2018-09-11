using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour 
{
	// only have one in scene
	public ScoreManager score_manager;

	private Transform ovr_child;
	// Use this for initialization
	void Start()
	{
		
	}
	
	// Update is called once per frame
	void FixedUpdate()
	{
		
	}

	void OnCollisionEnter(Collision collision)
	{
		if(collision.collider.gameObject.tag == "EnemyScoringPortion")
		{
			OVRInput.SetControllerVibration(0.5f, 0.5f, OVRInput.Controller.LTouch);
			Debug.Log("Enemy score should be increased");
			score_manager.increase_enemy_score();
		}
	}
}
