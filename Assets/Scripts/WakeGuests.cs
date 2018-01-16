using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WakeGuests : MiniGameScript 
{
	public static WakeGuests WakeGuestsReference;
	public static float timeLimit = 7.5f;

	float previousDepthAccel = 0;
	float currentDepthAccel = 0;
	float deltaDepthAccel = 0;
	int depthShakeCount = 0;

	public Transform guestGroup;

	Transform guest_01;
	Transform guest_02;
	Transform guest_03;
	Transform guest_04;
	Transform guest_05;

	int currentGuest = 0;
	int guest01Rand;
	int guest02Rand;
	int guest03Rand;
	int guest04Rand;
	int guest05Rand;

	bool doStartGame = true;
	public bool failedEarly = false;

	bool doLerpGuestGroup = false;
	public float moveDuration;
	float currentMoveTime;
	Vector3 initialHorzPos;
	Vector3 nextHorzPos;


	void Start () 
	{
		WakeGuestsReference = this;

		guest_01 = guestGroup.GetChild(0);
		guest_02 = guestGroup.GetChild(1);
		guest_03 = guestGroup.GetChild(2);
		guest_04 = guestGroup.GetChild(3);
		guest_05 = guestGroup.GetChild(4);

		currentMoveTime = 0;
	}
	
	void Update () 
	{
		UpdateDepthShakeCount();

		if (MiniGameManager.ManagerReference.IsInGame() && doStartGame)
		{
			RandomizeGuestTimes();
			StartCoroutine("StartGuestCycle");
			doStartGame = false;
		}

		if (failedEarly || MiniGameManager.ManagerReference.timer == 0) EndGame();


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
	}

	IEnumerator StartGuestCycle()
	{
		StartCoroutine("CycleGuestGroup");
		yield return null;
		if (failedEarly || MiniGameManager.ManagerReference.timer == 0) StopCoroutine("CycleGuestGroup");
	}


	IEnumerator CycleGuestGroup()
	{
		yield return new WaitForSeconds(0.25f);
		
		initialHorzPos = guestGroup.localPosition;
		nextHorzPos = new Vector3(-1475, 0, 0);
		currentGuest = 1;
		doLerpGuestGroup = true;

		yield return new WaitForSeconds(1.25f);
		CheckFailure();
		if (failedEarly)
		{
			guest_01.GetChild(3).GetComponent<RawImage>().enabled = true;
		}
		else
		{
			guest_01.GetChild(2).GetComponent<RawImage>().enabled = true;
		}
		yield return new WaitForSeconds(0.25f);

		initialHorzPos = guestGroup.localPosition;
		nextHorzPos = new Vector3(-2950, 0, 0);
		currentGuest = 2;
		doLerpGuestGroup = true;

		yield return new WaitForSeconds(1.25f);
		CheckFailure();
		if (failedEarly)
		{
			guest_02.GetChild(3).GetComponent<RawImage>().enabled = true;
		}
		else
		{
			guest_02.GetChild(2).GetComponent<RawImage>().enabled = true;
		}
		yield return new WaitForSeconds(0.25f);
		
		initialHorzPos = guestGroup.localPosition;
		nextHorzPos = new Vector3(-4425, 0, 0);
		currentGuest = 3;
		doLerpGuestGroup = true;

		yield return new WaitForSeconds(1.25f);
		CheckFailure();
		if (failedEarly)
		{
			guest_03.GetChild(3).GetComponent<RawImage>().enabled = true;
		}
		else
		{
			guest_03.GetChild(2).GetComponent<RawImage>().enabled = true;
		}
		yield return new WaitForSeconds(0.25f);

		
		initialHorzPos = guestGroup.localPosition;
		nextHorzPos = new Vector3(-5900, 0, 0);
		currentGuest = 4;
		doLerpGuestGroup = true;

		yield return new WaitForSeconds(1.25f);
		CheckFailure();
		if (failedEarly)
		{
			guest_04.GetChild(3).GetComponent<RawImage>().enabled = true;
		}
		else
		{
			guest_04.GetChild(2).GetComponent<RawImage>().enabled = true;
		}
		yield return new WaitForSeconds(0.25f);
		
		initialHorzPos = guestGroup.localPosition;
		nextHorzPos = new Vector3(-7375, 0, 0);
		currentGuest = 5;
		doLerpGuestGroup = true;

		yield return new WaitForSeconds(1.5f);
		CheckFailure();
		if (failedEarly)
		{
			guest_05.GetChild(3).GetComponent<RawImage>().enabled = true;
		}
		else
		{
			guest_05.GetChild(2).GetComponent<RawImage>().enabled = true;
		}
		yield return new WaitForSeconds(0.25f);
	}

	void RandomizeGuestTimes()
	{
		guest01Rand = (int)Mathf.Round(Random.Range(0f, 1f));
		guest_01.GetChild(guest01Rand).GetComponent<RawImage>().enabled = true;

		guest02Rand = (int)Mathf.Round(Random.Range(0f, 1f));
		guest_02.GetChild(guest02Rand).GetComponent<RawImage>().enabled = true;

		guest03Rand = (int)Mathf.Round(Random.Range(0f, 1f));
		guest_03.GetChild(guest03Rand).GetComponent<RawImage>().enabled = true;

		guest04Rand = (int)Mathf.Round(Random.Range(0f, 1f));
		guest_04.GetChild(guest04Rand).GetComponent<RawImage>().enabled = true;

		guest05Rand = (int)Mathf.Round(Random.Range(0f, 1f));
		guest_05.GetChild(guest05Rand).GetComponent<RawImage>().enabled = true;
	}


	void CheckFailure()
	{
		if (currentGuest == 1)
		{
			failedEarly = (guest01Rand == 0) ? !DepthShakeDetected() : DepthShakeDetected();	
		}
		else if (currentGuest == 2)
		{
			failedEarly = (guest02Rand == 0) ? !DepthShakeDetected() : DepthShakeDetected();
		}
		else if (currentGuest == 3)
		{
			failedEarly = (guest03Rand == 0) ? !DepthShakeDetected() : DepthShakeDetected();
		}
		else if (currentGuest == 4)
		{
			failedEarly = (guest04Rand == 0) ? !DepthShakeDetected() : DepthShakeDetected();
		}
		else if (currentGuest == 5)
		{
			failedEarly = (guest05Rand == 0) ? !DepthShakeDetected() : DepthShakeDetected();
		}
	}

	void UpdateDepthShakeCount()
	{
		previousDepthAccel = currentDepthAccel;
		currentDepthAccel = Input.acceleration.z;
		deltaDepthAccel = currentDepthAccel - previousDepthAccel;
		if (Mathf.Abs(deltaDepthAccel) > 0.5f) depthShakeCount++;
	}
	
	bool DepthShakeDetected()
	{
		bool result = depthShakeCount > 10;
		depthShakeCount = 0;
		return result;
	}


	void EndGame()
	{
		StopCoroutine("StartGuestCycle");

		MiniGameManager.ManagerReference.didWin = !failedEarly;
        MiniGameManager.ManagerReference.EndMiniGame();
	}

	public override void ResetGame()
	{
		currentMoveTime = 0;
		
		doStartGame = true;
		failedEarly = false;

		currentGuest = 0;

		guestGroup.localPosition = new Vector3(0,0,0);

		guest_01.GetChild(0).GetComponent<RawImage>().enabled = false;
		guest_01.GetChild(1).GetComponent<RawImage>().enabled = false;
		guest_01.GetChild(2).GetComponent<RawImage>().enabled = false;
		guest_01.GetChild(3).GetComponent<RawImage>().enabled = false;

		guest_02.GetChild(0).GetComponent<RawImage>().enabled = false;
		guest_02.GetChild(1).GetComponent<RawImage>().enabled = false;
		guest_02.GetChild(2).GetComponent<RawImage>().enabled = false;
		guest_02.GetChild(3).GetComponent<RawImage>().enabled = false;

		guest_03.GetChild(0).GetComponent<RawImage>().enabled = false;
		guest_03.GetChild(1).GetComponent<RawImage>().enabled = false;
		guest_03.GetChild(2).GetComponent<RawImage>().enabled = false;
		guest_03.GetChild(3).GetComponent<RawImage>().enabled = false;

		guest_04.GetChild(0).GetComponent<RawImage>().enabled = false;
		guest_04.GetChild(1).GetComponent<RawImage>().enabled = false;
		guest_04.GetChild(2).GetComponent<RawImage>().enabled = false;
		guest_04.GetChild(3).GetComponent<RawImage>().enabled = false;

		guest_05.GetChild(0).GetComponent<RawImage>().enabled = false;
		guest_05.GetChild(1).GetComponent<RawImage>().enabled = false;
		guest_05.GetChild(2).GetComponent<RawImage>().enabled = false;
		guest_05.GetChild(3).GetComponent<RawImage>().enabled = false;
	}
}
