using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour 
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

	void OnCollisionEnter(Collision collision) // everything needs rigidbodies rip OR set all colliders to have an isTrigger
	{	
		if(collision.collider.gameObject.tag == "PlayerScoringPortion")
		{
			OVRInput.SetControllerVibration(0.5f, 0.5f, OVRInput.Controller.RTouch);
			Debug.Log("Player score should be increased.");
			score_manager.increase_player_score();
		}
	}
}
