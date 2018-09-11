using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightsaberDraw : MonoBehaviour
{
	public Transform start_position;
	public Transform end_position;	
	private LineRenderer linerenderer;
	private float texture_offset = 0.0f;
	// Use this for initialization
	void Start()
	{
		linerenderer = GetComponent<LineRenderer>();	
	}
	
	// Update is called once per frame
	void Update()
	{
		linerenderer.SetPosition(0, start_position.position);
		linerenderer.SetPosition(1, end_position.position);

		texture_offset -= Time.deltaTime;
		if(texture_offset < -10.0f)
		{
			texture_offset += 10.0f;
		}

		linerenderer.sharedMaterials[1].SetTextureOffset("_MainTex", new Vector2(texture_offset, 0.0f));
	}
}
