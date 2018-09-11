using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour 
{
	private int player_score = 0;
	private int enemy_score = 0;
	private int misses = 0;

	// Use this for initialization
	void Start()
	{
		
	}
	
	// Update is called once per frame
	void Update()
	{
		
	}

	public void increase_player_score()
	{
		player_score++;
		Debug.Log("Player score: " + player_score);
	}

	public void increase_enemy_score()
	{
		enemy_score++;
		Debug.Log("Enemy score: " + enemy_score);
	}

	public void increase_misses()
	{
		misses++;
		Debug.Log("misses: " + misses);
	}

	public int get_player_score()
	{
		return player_score;
	}

	public int get_enemy_score()
	{
		return enemy_score;
	}

	public int get_misses()
	{
		return misses;
	}
}
