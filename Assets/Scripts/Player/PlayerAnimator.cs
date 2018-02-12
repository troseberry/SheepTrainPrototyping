using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AnimState {IDLE, WALK };

public class PlayerAnimator : MonoBehaviour 
{	
	public static PlayerAnimator PlayerAnimatorReference;

	private static Animator sheepAnimator;
	private static AnimState currentState;

	
	void Start()
	{
		sheepAnimator = GetComponent<Animator>();
	}

	void Update()
	{
		ProcessMovement();
	}
	
	public static void SetAnimState(AnimState newState) { currentState = newState; }

	public static void Idle() { sheepAnimator.SetInteger("Movement", 0); }

	public static void Walk() { sheepAnimator.SetInteger("Movement", 1); }

	public static void ProcessMovement()
	{
		switch (currentState)
		{
			case AnimState.IDLE:
				Idle();
				break;
			case AnimState.WALK:
				Walk();
				break;
		}
	}
}