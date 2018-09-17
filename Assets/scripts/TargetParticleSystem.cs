using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetParticleSystem : MonoBehaviour
{
	private float time = 0.0f;
	// Use this for initialization
	void Start()
	{
		
	}
	
	// Update is called once per frame
	void Update()
	{
		time += Time.deltaTime;

		if(time > 0.8f)
		{
			Destroy(gameObject);
		}
	}
}
