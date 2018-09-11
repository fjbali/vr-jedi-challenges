using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisions : MonoBehaviour
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
			Laser laser = collision.gameObject.GetComponent<Laser>();

			if(laser.can_damage)
			{
				score_manager.increase_misses();
				Debug.Log("PLAYER COLLISION");
			}
		}
	}
}
