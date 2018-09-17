using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
	public float speed;
	public bool can_damage = true;

	private float lifetime = 0.0f;
	private AudioSource sound;

	// compare the laser's normalized transform.forward to the normalized positions
	private Vector3 goal_target;
	private Rigidbody rigidbody;

	// Use this for initialization
	void Start()
	{
		goal_target = Vector3.zero;
		sound = GetComponent<AudioSource>();
		rigidbody = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update()
	{
		sound.Play();
		transform.position += speed * Time.deltaTime * transform.forward;
		lifetime += Time.deltaTime;

		if(lifetime > 5.0f)
		{
			Destroy(gameObject);
		}

		if(!can_damage && goal_target != Vector3.zero)
		{
			transform.forward = goal_target - transform.position;
		}
	}

	// must be normalized
	private Vector3 reflection(Vector3 n)
	{
		return transform.forward - 2 * (Vector3.Dot(transform.forward, n)) * n;
	}


	private Vector3 pick_target(GameObject[] targets)
	{
		float smallest_angle = 360.0f;
		Vector3 target = transform.forward;
		for(int i = 0; i < targets.Length; i++)
		{
			float angle = Vector3.Angle(transform.forward, targets[i].transform.position);
			if(angle < smallest_angle)
			{
				smallest_angle = angle;
				target = targets[i].transform.position;
			}
		}

		if(smallest_angle < 45.0f)
		{
			return target;
		}

		return Vector3.zero;
	}


	void OnCollisionEnter(Collision collision)
	{
		if(collision.gameObject.tag == "PlayerScoringPortion")
		{
			can_damage = false;
			transform.forward = reflection(Vector3.Normalize(collision.gameObject.transform.forward));
			GameObject[] targets = GameObject.FindGameObjectsWithTag("Target");
			goal_target = pick_target(targets);
		}

		else if(collision.gameObject.tag == "Environment" || collision.gameObject.transform.root.tag == "Environment" || 
			collision.gameObject.tag == "Player" || collision.gameObject.transform.root.tag == "Player")
		{
			Destroy(gameObject);
		}

		else if(collision.gameObject.tag == "Target" && !can_damage)
		{
			Destroy(gameObject);
		}
	}
}