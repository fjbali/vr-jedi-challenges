using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordCollision : MonoBehaviour
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
		if(collision.gameObject.tag == "Laser")
		{
			score_manager.increase_player_score();
			Debug.Log("SWORD COLLISION");
		}
	}
}
