using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBehaviour : MonoBehaviour
{
	public GameObject player_sword;

	public float aggression;
	public float reaction_time;
	public float speed;

	private float time = 0.0f;
	private bool should_lunge = false;
	private bool should_advance = false;
	private bool should_retreat = false;

	private GameObject sword; 


	// Use this for initialization
	void Start()
	{
		sword = GameObject.FindGameObjectWithTag("EnemyScoringPortion");
		while(aggression > 1)
		{
			aggression /= 10;
		}
	}
	
	// Update is called once per frame
	void Update()
	{
		if(time > reaction_time)
		{
			should_lunge = false;
			should_advance = false;
			should_retreat = false;
			determine_reaction();
			time = 0;	
		}
		execute_reactions();


		time += Time.deltaTime;
	}

	private void determine_reaction()
	{
		float sword_distance = Vector3.Magnitude(transform.position - player_sword.transform.position);
		float chance_to_get_hit;
		float choice_to_lunge = Random.Range(0.0f, 1.0f);


		if(sword_distance > 1.11)
		{
			chance_to_get_hit = 1.0f / sword_distance;
		}

		else
		{
			chance_to_get_hit = 0.9f;
		}
		
		
		// very likely to get hit, will either retreat or do a lunge
		if(chance_to_get_hit >= 0.85f)
		{
			if(choice_to_lunge < aggression)
			{
				should_lunge = true;
			}

			else
			{
				should_retreat = true;
			}
		}

		// very likely to get hit - player is close enough to do a lunge
		else if(chance_to_get_hit > 0.8f)
		{
			if(choice_to_lunge < aggression / 1.25f)
			{
				should_lunge = true;
			}

			else if(choice_to_lunge < aggression)
			{
				should_advance = true;
			}

			else
			{
				should_retreat = true;
			}
		}

		// advance lunge can probably hit
		else if(chance_to_get_hit > 0.5f)
		{
			if(choice_to_lunge < aggression / 1.5f)
			{
				should_lunge = true;
			}

			else if(choice_to_lunge < aggression)
			{
				should_advance = true;
			}

			else
			{
				should_retreat = true;
			}
		}

		// farish away to far away
		else
		{
			if(choice_to_lunge < aggression / 2.0f)
			{
				should_lunge = true;
			}

			else if(choice_to_lunge < aggression)
			{
				should_advance = true;
			}

			else
			{
				should_retreat = true;
			}
		}
	}

	private void execute_reactions()
	{
		if(should_lunge)
		{
			lunge();
		}

		if(should_advance)
		{
			advance();
		}

		if(should_retreat)
		{
			retreat();
		}
	}

	private void advance()
	{
		float step = speed * Time.deltaTime;
		transform.Translate(Vector3.forward * step);
	}

	private void lunge()
	{
		float step = 4 * speed * Time.deltaTime;
		transform.Translate(Vector3.forward * step);
	}

	private void feint()
	{

	}

	private void retreat()
	{
		float step = speed * Time.deltaTime;
		transform.Translate(-1.0f * Vector3.forward * step);
	}

	private void parry()
	{
		float step = speed * Time.deltaTime;
		sword.transform.position = Vector3.MoveTowards(sword.transform.position,
										player_sword.transform.position,
										step);
	}
}
