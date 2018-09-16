using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
	public float life_time = 0.0f;
	public ScoreManager score_manager;

	private bool breaking = false;
	private float break_time = 0.0f;
	private float time_alive = 0.0f;

	private AudioSource sound;
	// Use this for initialization
	void Start()
	{
		sound = GetComponent<AudioSource>();
		sound.Pause();
		//score_manager = GetComponentInParent<ScoreManager>();
		break_time = sound.clip.length;
	}
	
	// Update is called once per frame
	void Update()
	{
		time_alive += Time.deltaTime;
		
		if(breaking)
		{
			sound.Play();
			break_time += Time.deltaTime;
			if(break_time > sound.clip.length)
			{
				Destroy(gameObject);
			}
		}

		if(time_alive > life_time)
		{
			breaking = true;
		}
	}

	void OnCollisionEnter(Collision collision)
	{
		if(collision.gameObject.tag == "Laser")
		{
			breaking = true;
		}
	}

	void OnDestroy()
	{
		score_manager.increase_player_score();
	}
}
