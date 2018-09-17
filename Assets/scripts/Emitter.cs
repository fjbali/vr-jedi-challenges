using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Emitter : MonoBehaviour
{
	public Laser laser_prefab;
	public ScoreManager score_manager;
	public GameObject target_object;
	public float fire_rate;
	public int advance_score;
	public float laser_speed;

	private CapsuleCollider target;
	private Vector3[] bezier_points;
	private EmitterSound sound;

	private float time = 0.0f;
	private float time_between = 0.0f;
	private int fire_mode = 0;
	private bool moving = false;
	private bool firing = false;
	private int level = 0;
	private float move_speed = 0.2f;

	// score - will be score_manager.get_player_score
	// misses - will be score_manager.get_enemy_score()

	void Start()
	{
		target = target_object.GetComponentInChildren<CapsuleCollider>();
		sound = GetComponent<EmitterSound>();
		bezier_points = new Vector3[4];
		bezier_points[0] = transform.position;
	}
	
	void Update()
	{
		time += Time.deltaTime;
		time_between += Time.deltaTime;

		if(fire_mode == 0)
		{	
			if(time_between >= fire_rate / Random.Range(1, 3))
			{
				fire();
				time_between = 0.0f;
			}

			else if(time >= fire_rate)
			{
				fire();
				bezier_points = pick_bezier_points(-7.0f, 7.0f, 2.0f, 6.0f, -7.0f, 7.0f);
				moving = true;
				time = 0.0f;
				time_between = 0.0f;
			}

			else
			{
				firing = false;
			}
		}
		
		else // more rapid-fire mode, moves a lot and sends lasers while moving more
		{
			
		}

		if(moving)
		{
			transform.position = bezier_movement();

			if(Vector3.Magnitude(transform.position - bezier_points[3]) < 1.0f)
			{
				moving = false;
			}
		}

		else
		{
			transform.position = idle_movement();
		}

		if(score_manager.get_player_score() > advance_score)
		{
			level++;
			fire_rate *= 0.9f;
			laser_speed *= 1.2f;
			advance_score += (int) ((float) advance_score * 1.1f);

			if(level % 3 == 0)
			{
			//	fire_mode = 1;
			}

			else
			{
				fire_mode = 0;
			}
		}
	}


	public bool is_firing()
	{
		return firing;
	}


	private void fire()
	{
		firing = true;
		sound.play_blast();
		float x_goal = target.gameObject.transform.position.x + Random.Range(-0.8f  * target.radius, 0.8f * target.radius);
		float y_goal = target.gameObject.transform.position.y + Random.Range(-1.0f * target.height / 2.0f, target.height / 2.0f);
		float z_goal = target.gameObject.transform.position.z + Random.Range(-0.8f * target.radius, 0.8f  * target.radius);
		
		Vector3 laser_goal = new Vector3(x_goal, y_goal, z_goal);

		Laser laser = Instantiate(laser_prefab, transform.position, Quaternion.identity); 
		laser.transform.forward = laser_goal - transform.position;
		laser.speed = laser_speed;
	}


	private Vector3 idle_movement()
	{
		Debug.Log("IN IDLE MOVEMENT");
		Vector3 position = new Vector3(transform.position.x + Mathf.Cos(Time.time) * Time.deltaTime * Mathf.PI / 4 * move_speed,
										transform.position.y + Mathf.Sin(Time.time) * Time.deltaTime * Mathf.PI / 4 * move_speed,
										transform.position.z + Mathf.Cos(Time.time) * Time.deltaTime * Mathf.PI / 4 * move_speed);
		return position;
	}


	private Vector3 bezier_movement()
	{
		Debug.Log("BEZIER MOVEMENT");
		float t = time / 2.0f * move_speed;
		Vector3 new_position = 	Mathf.Pow(1 - t, 3.0f) * bezier_points[0] +
								3 * Mathf.Pow(1 - t, 2.0f) * t * bezier_points[1] +
								3 * (1 - t) * Mathf.Pow(t, 2) * bezier_points[2] +
								Mathf.Pow(t, 3) * bezier_points[3];
		return new_position;
	}

	private Vector3[] pick_bezier_points(float x_min, float x_max, float y_min, float y_max, float z_min, float z_max)
	{
		Debug.Log("ENTERED PICK BEZIER POINTS");
		Vector3 point_3 = new Vector3(Random.Range(x_min, x_max), Random.Range(y_min, y_max), Random.Range(z_min, z_max)); // goal
		Vector3 point_2 = new Vector3(Random.Range(x_min, x_max), Random.Range(y_min, y_max), Random.Range(z_min, z_max));
		Vector3 point_1 = new Vector3(Random.Range(x_min, x_max), Random.Range(y_min, y_max), Random.Range(z_min, z_max));
		if(Vector3.Magnitude(transform.position - point_3) < 3.0f || Time.deltaTime < 0.11f)
		{
			point_3.x += 0.5f;
			point_3.y += 0.5f;
			point_3.z += 0.5f;
		}
		
		Vector3[] points = new Vector3[4]{transform.position, point_1, point_2, point_3};
		Debug.Log("GOT THROUGH PICK BEZIER POINTS");
		return points;
	}
}
