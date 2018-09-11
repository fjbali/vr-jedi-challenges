using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TrainingGameManager : MonoBehaviour
{
	public ScoreManager score_manager;
	public Emitter emitter;
	public Target target_prefab;
	public float game_time;
	public int win_score;
	public int lose_misses; // need to integrate

	private Text text;
	private RectTransform rectTransform;
	private float timer;
	private float calculated_score;

	// Use this for initialization
	void Start()
	{
		text = GetComponentInChildren<Text>();
		timer = game_time;
	}
	
	// Update is called once per frame
	void Update()
	{
		text.text = make_text();
		
		if(emitter.is_firing())
		{
			create_targets();
		}
	}

	private string make_text()
	{
		string timer_text = "";
		string score_text = "";
		string misses_text = "";
		string calculated_score_text = "";

		calculated_score = score_manager.get_player_score() - 2.0f * score_manager.get_misses();
	
		if(game_time != 0.0f)
		{
			timer -= Time.deltaTime;
			
			if(timer > 0.0f)
			{
				timer_text = "Time: " + timer.ToString("F2");
			}

			else
			{
				timer_text = "Game over!";
			}
		}

		score_text = "Score: " + score_manager.get_player_score().ToString();
		misses_text = "Misses: " + score_manager.get_misses().ToString();
		calculated_score_text = "Adjusted Score: " + calculated_score.ToString("F2");

		if(win_score > 0 && score_manager.get_player_score() >= win_score)
		{
			return "You win!";
		}

		if(score_manager.get_misses() >= lose_misses)
		{
			return "You lose! " + misses_text;
		}

		return timer_text + "\t" + score_text + "\n" + 
						misses_text + "\t" + calculated_score_text;
	}

	private void create_targets()
	{
		Vector3 position = Vector3.zero;
		if((int) Time.time % 2 == 0)
		{
			position = new Vector3(Random.Range(-5.0f, 5.0f), Random.Range(0.5f, 3.0f), Random.Range(-6.0f, -1.0f));
		}

		else
		{
			position = new Vector3(Random.Range(-5.0f, 5.0f), Random.Range(0.5f, 3.0f), Random.Range(1.0f, 6.0f));
		}

		float fscale = Random.Range(0.75f, 1.5f);
		Vector3 scale = new Vector3(fscale, fscale, fscale);

		Target target = Instantiate(target_prefab, position, Quaternion.identity);
		target.transform.localScale = scale;
		target.life_time = Random.Range(3, 10);		
	}
}
