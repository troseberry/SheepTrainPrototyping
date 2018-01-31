using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WakeGuests : MiniGameScript 
{
	public static WakeGuests WakeGuestsReference;
	public static float timeLimit = 7.5f;

	private Vector3 shakeForce;
	private bool didShake;
	private bool didHoldStill;
	private float shakeThreshold = 3.0f;
	private float stillThreshold = 1.25f;

	public Transform guestGroup;

	private Transform[] guestTransforms;

	private int currentGuest = 0;
	private int[] randGuestTimes = new int[5];

	private bool doStartGame = true;
	public bool failedEarly = false;

	private bool doLerpGuestGroup = false;
	public float moveDuration;
	private float currentMoveTime;
	private Vector3 initialHorzPos;
	private Vector3 nextHorzPos;


	void Start () 
	{
		WakeGuestsReference = this;

		guestTransforms = new Transform[]
		{
			guestGroup.GetChild(0),
			guestGroup.GetChild(1),
			guestGroup.GetChild(2),
			guestGroup.GetChild(3),
			guestGroup.GetChild(4)
		};

		currentMoveTime = 0;
	}
	
	void Update () 
	{
		if (MiniGameManager.IsInGame() && doStartGame)
		{
			RandomizeGuestTimes();
			StartCoroutine("StartGuestCycle");
			doStartGame = false;
		}

		if (failedEarly || MiniGameManager.timer == 0) EndGame();


		if (doLerpGuestGroup)
		{
			if (currentMoveTime < moveDuration) currentMoveTime += Time.deltaTime / .25f*moveDuration;
			guestGroup.localPosition = Vector3.Lerp(initialHorzPos, nextHorzPos, currentMoveTime);

			if (currentMoveTime >= moveDuration)
        	{
				guestGroup.localPosition = nextHorzPos;
				currentMoveTime = 0;

				doLerpGuestGroup = false;
			}
		}

		CalculateShakeForce();
		Debug.Log("Force: " + shakeForce.sqrMagnitude);
	}

	IEnumerator StartGuestCycle()
	{
		StartCoroutine("CycleGuestGroup");
		yield return null;
		if (failedEarly || MiniGameManager.timer == 0) StopCoroutine("CycleGuestGroup");
	}


	IEnumerator CycleGuestGroup()
	{

		for (int i = 0; i < 5; i++)
		{
			yield return new WaitForSeconds(0.25f);
		
			initialHorzPos = guestGroup.localPosition;
			nextHorzPos = new Vector3((-1475 * (i + 1)), 0, 0);
			currentGuest = (i + 1);
			doLerpGuestGroup = true;

			yield return new WaitForSeconds(1.25f);
			
			failedEarly = DetermineShakeResult(randGuestTimes[currentGuest - 1]);
			if (failedEarly)
			{
				guestTransforms[i].GetChild(3).GetComponent<RawImage>().enabled = true;
			}
			else
			{
				guestTransforms[i].GetChild(2).GetComponent<RawImage>().enabled = true;
			}
		}
		yield return new WaitForSeconds(0.25f);
	}

	void RandomizeGuestTimes()
	{
		for (int i = 0; i < 5; i++)
		{
			randGuestTimes[i] = (int)Mathf.Round(Random.Range(0f, 1f));
			guestTransforms[i].GetChild(randGuestTimes[i]).GetComponent<RawImage>().enabled = true;
		}
	}

	void CalculateShakeForce()
	{
		shakeForce = Input.acceleration;

		didShake = (shakeForce.sqrMagnitude >= shakeThreshold);
		didHoldStill = (shakeForce.sqrMagnitude <= stillThreshold);
	}
	
	bool DetermineShakeResult(int guestTime)
	{
		bool result = false;

		if (guestTime == 0) result = didHoldStill;
		else if (guestTime == 1) result = didShake;

		didShake = false;
		didHoldStill = false;

		return result;
	}


	void EndGame()
	{
		StopCoroutine("StartGuestCycle");

		MiniGameManager.EndMiniGame(!failedEarly);
	}

	public override void ResetGame()
	{
		currentMoveTime = 0;
		currentGuest = 0;
		
		guestGroup.localPosition = new Vector3(0,0,0);

		for (int i = 0; i < 5; i++)
		{
			guestTransforms[i].GetChild(0).GetComponent<RawImage>().enabled = false;
			guestTransforms[i].GetChild(1).GetComponent<RawImage>().enabled = false;
			guestTransforms[i].GetChild(2).GetComponent<RawImage>().enabled = false;
			guestTransforms[i].GetChild(3).GetComponent<RawImage>().enabled = false;
		}
		
		doStartGame = true;
		failedEarly = false;
	}

	public override float GetTimeLimit() { return timeLimit; }
}
