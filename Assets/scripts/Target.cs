using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
	public float life_time = 0.0f;
	public ScoreManager score_manager;
	public GameObject target_particles_prefab;

	private float time_alive = 0.0f;
	private bool hit_by_laser = false;

	// Use this for initialization
	void Start()
	{
		//score_manager = GetComponentInParent<ScoreManager>();
	}
	
	// Update is called once per frame
	void Update()
	{
		time_alive += Time.deltaTime;
		if(time_alive >= life_time)
		{
			Destroy(gameObject);
		}
	}

	void OnCollisionEnter(Collision collision)
	{
		if(collision.gameObject.tag == "Laser")
		{
			hit_by_laser = true;
			Destroy(gameObject);
		}
	}

	void OnDestroy()
	{
		if(hit_by_laser)
		{
			score_manager.increase_targets_broken();
			Debug.Log("It recognizes it in console, but not in TrainingGameManager?");
			Instantiate(target_particles_prefab, transform.position, Quaternion.identity);
		}
	}
}
