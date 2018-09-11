using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimation : MonoBehaviour
{
	Animator anim;
	int idle_hash = Animator.StringToHash("IdleHoldSword");
	int entry_hash = Animator.StringToHash("Entry");
	int jump_hash = Animator.StringToHash("Jump");

	// Use this for initialization
	void Start()
	{
		anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update()
	{
		AnimatorStateInfo state_info = anim.GetCurrentAnimatorStateInfo(0);
		anim.SetTrigger(jump_hash);
	}
}
