using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public enum AnimState {IDLE, WALK };

public class PlayerAnimator : MonoBehaviour 
{	
	public static PlayerAnimator PlayerAnimatorReference;

	private static NetworkAnimator sheepNetworkAnimator;
	private static Animator sheepAnimator;
	private static AnimState currentState;

	
	void Start()
	{
		PlayerAnimatorReference = this;

		sheepAnimator = GetComponent<Animator>();
		sheepNetworkAnimator = GetComponent<NetworkAnimator>();
	}

	void Update()
	{
		ProcessAnimation();

		if (Input.GetKeyDown(KeyCode.I))
		{
			StartInteracting();
		}
		if (Input.GetKeyDown(KeyCode.U))
		{
			StopInteracting();
		}
	}
	
	public static void SetAnimState(AnimState newState) { currentState = newState; }

	public static void Idle() { sheepAnimator.SetFloat("Movement", 0f); }

	public static void Walk() { sheepAnimator.SetFloat("Movement", 1f); }

	public static void StartInteracting() { sheepAnimator.SetBool("Interact", true); }

	public static void StopInteracting() { sheepAnimator.SetBool("Interact", false); }

	public static void Celebrate()
	{
		Debug.Log("Celebrating: " + PlayerAnimatorReference.gameObject.name);
		// sheepAnimator.SetTrigger("Celebrate");
		sheepNetworkAnimator.SetTrigger("Celebrate");
	}

	public static void TauntWave()
	{
		sheepAnimator.SetTrigger("Taunt/Wave");
		sheepNetworkAnimator.SetTrigger("Taunt/Wave");
	}

	public static void ProcessAnimation()
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