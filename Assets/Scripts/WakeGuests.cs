using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WakeGuests : MonoBehaviour 
{
	public static WakeGuests WakeGuestsReference;
	public static float timeLimit = 7.5f;

	float horzAcceleration = 0;
	float vertAcceleration = 0;
	float depthAcceleration = 0;

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




	public float avrgTime = 0.5f;
	public float peakLevel = 0.6f;
	public float endCountTime = 0.6f;
	public int shakeDir;
	public int shakeCount;
		
	Vector3 avrgAcc = Vector3.zero;
	int countPos;
	int countNeg;
	int lastPeak;
	int firstPeak;
	bool counting;
	float timer;


	void Start () 
	{
		WakeGuestsReference = this;

		horzAcceleration = Input.acceleration.x;
		vertAcceleration = Input.acceleration.y;
		depthAcceleration = Input.acceleration.z;

		

		guest_01 = guestGroup.GetChild(0);
		guest_02 = guestGroup.GetChild(1);
		guest_03 = guestGroup.GetChild(2);
		guest_04 = guestGroup.GetChild(3);
		guest_05 = guestGroup.GetChild(4);

		currentMoveTime = 0;
	}
	
	void Update () 
	{
		if (Mathf.Abs(Input.acceleration.x) > Mathf.Abs(horzAcceleration)) horzAcceleration = Input.acceleration.x;
		if (Mathf.Abs(Input.acceleration.y) > Mathf.Abs(vertAcceleration)) vertAcceleration = Input.acceleration.y;
		if (Mathf.Abs(Input.acceleration.z) > Mathf.Abs(depthAcceleration)) depthAcceleration = Input.acceleration.z;

		DebugPanel.Log("Acceleration X: ", horzAcceleration);
		DebugPanel.Log("Acceleration Y: ", vertAcceleration);
		DebugPanel.Log("Acceleration Z: ", depthAcceleration);

		if (MiniGameManager.ManagerReference.IsInGame() && doStartGame)
		{
			RandomizeGuestTimes();
			StartCoroutine(StartGuestCycle());
			doStartGame = false;
		}

		if (failedEarly || MiniGameManager.ManagerReference.timer == 0) CloseGame();


		if (doLerpGuestGroup)
		{
			
			if (currentMoveTime < moveDuration) currentMoveTime += Time.deltaTime / .25f*moveDuration;
			guestGroup.localPosition = Vector3.Lerp(initialHorzPos, nextHorzPos, currentMoveTime);

			if (currentMoveTime >= moveDuration)
        	{
				guestGroup.localPosition = nextHorzPos;
				currentMoveTime = 0;

				vertAcceleration = 0;
				depthAcceleration = 0;

				doLerpGuestGroup = false;

				//check failure here. right at end of lerp? or right before lerping to next?
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
		// guestGroup.localPosition = new Vector3(guestGroup.localPosition.x -1500, 0, 0);
		initialHorzPos = guestGroup.localPosition;
		nextHorzPos = new Vector3(-1250, 0, 0);
		currentGuest = 1;
		doLerpGuestGroup = true;

		yield return new WaitForSeconds(1.5f);
		CheckFailure();
		// guestGroup.localPosition = new Vector3(guestGroup.localPosition.x -1500, 0, 0);
		initialHorzPos = guestGroup.localPosition;
		nextHorzPos = new Vector3(-2500, 0, 0);
		currentGuest = 2;
		doLerpGuestGroup = true;

		yield return new WaitForSeconds(1.5f);
		CheckFailure();
		// guestGroup.localPosition = new Vector3(guestGroup.localPosition.x -1500, 0, 0);
		initialHorzPos = guestGroup.localPosition;
		nextHorzPos = new Vector3(-3750, 0, 0);
		currentGuest = 3;
		doLerpGuestGroup = true;

		yield return new WaitForSeconds(1.5f);
		CheckFailure();
		// guestGroup.localPosition = new Vector3(guestGroup.localPosition.x -1500, 0, 0);
		initialHorzPos = guestGroup.localPosition;
		nextHorzPos = new Vector3(-5000, 0, 0);
		currentGuest = 4;
		doLerpGuestGroup = true;

		yield return new WaitForSeconds(1.5f);
		CheckFailure();
		// guestGroup.localPosition = new Vector3(guestGroup.localPosition.x -1500, 0, 0);
		initialHorzPos = guestGroup.localPosition;
		nextHorzPos = new Vector3(-6250, 0, 0);
		currentGuest = 5;
		doLerpGuestGroup = true;

		yield return new WaitForSeconds(1.5f);
		CheckFailure();
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
		// if (currentGuest == 1)
		// {
		// 	failedEarly = (guest01Rand == 0) ? (Mathf.Abs(vertAcceleration) < 2.5f || Mathf.Abs(depthAcceleration) < 2.5f) : (Mathf.Abs(vertAcceleration) >= 2.5f || Mathf.Abs(depthAcceleration) >= 2.5f);
		// }
		// else if (currentGuest == 2)
		// {
		// 	failedEarly = (guest02Rand == 0) ? (Mathf.Abs(vertAcceleration) < 2.5f || Mathf.Abs(depthAcceleration) < 2.5f) : (Mathf.Abs(vertAcceleration) >= 2.5f || Mathf.Abs(depthAcceleration) >= 2.5f);
		// }
		// else if (currentGuest == 3)
		// {
		// 	failedEarly = (guest03Rand == 0) ? (Mathf.Abs(vertAcceleration) < 2.5f || Mathf.Abs(depthAcceleration) < 2.5f) : (Mathf.Abs(vertAcceleration) >= 2.5f || Mathf.Abs(depthAcceleration) >= 2.5f);
		// }
		// else if (currentGuest == 4)
		// {
		// 	failedEarly = (guest04Rand == 0) ? (Mathf.Abs(vertAcceleration) < 2.5f || Mathf.Abs(depthAcceleration) < 2.5f) : (Mathf.Abs(vertAcceleration) >= 2.5f || Mathf.Abs(depthAcceleration) >= 2.5f);
		// }
		// else if (currentGuest == 5)
		// {
		// 	failedEarly = (guest05Rand == 0) ? (Mathf.Abs(vertAcceleration) < 2.5f || Mathf.Abs(depthAcceleration) < 2.5f) : (Mathf.Abs(vertAcceleration) >= 2.5f || Mathf.Abs(depthAcceleration) >= 2.5f);
		// }


		if (currentGuest == 1)
		{
			failedEarly = (guest01Rand == 0) ? !ShakeDetector() : ShakeDetector();
		}
		else if (currentGuest == 2)
		{
			failedEarly = (guest02Rand == 0) ? !ShakeDetector() : ShakeDetector();
		}
		else if (currentGuest == 3)
		{
			failedEarly = (guest03Rand == 0) ? !ShakeDetector() : ShakeDetector();
		}
		else if (currentGuest == 4)
		{
			failedEarly = (guest04Rand == 0) ? !ShakeDetector() : ShakeDetector();
		}
		else if (currentGuest == 5)
		{
			failedEarly = (guest05Rand == 0) ? !ShakeDetector() : ShakeDetector();
		}
	}




	void CloseGame()
	{
		MiniGameManager.ManagerReference.didWin = !failedEarly;
        MiniGameManager.ManagerReference.EndMiniGame();
        gameObject.SetActive(false);
	}

	public void ResetGame()
	{
		currentMoveTime = 0;
		
		doStartGame = true;
		failedEarly = false;

		currentGuest = 0;

		guestGroup.localPosition = new Vector3(0,0,0);

		guest_01.GetChild(0).GetComponent<RawImage>().enabled = false;
		guest_01.GetChild(1).GetComponent<RawImage>().enabled = false;

		guest_02.GetChild(0).GetComponent<RawImage>().enabled = false;
		guest_02.GetChild(1).GetComponent<RawImage>().enabled = false;

		guest_03.GetChild(0).GetComponent<RawImage>().enabled = false;
		guest_03.GetChild(1).GetComponent<RawImage>().enabled = false;

		guest_04.GetChild(0).GetComponent<RawImage>().enabled = false;
		guest_04.GetChild(1).GetComponent<RawImage>().enabled = false;

		guest_05.GetChild(0).GetComponent<RawImage>().enabled = false;
		guest_05.GetChild(1).GetComponent<RawImage>().enabled = false;
	}









	bool ShakeDetector()
	{
		// read acceleration:
		Vector3 curAcc = Input.acceleration; 
		// update average value:
		avrgAcc = Vector3.Lerp(avrgAcc, curAcc, avrgTime * Time.deltaTime);
		// calculate peak size:
		curAcc -= avrgAcc;
		// variable peak is zero when no peak detected...
		int peak = 0;
		// or +/- 1 according to the peak polarity:
		if (curAcc.y > peakLevel) peak = 1;
		if (curAcc.y < -peakLevel) peak = -1;
		// do nothing if peak is the same of previous frame:
		if (peak == lastPeak) return false;
		// peak changed state: process it
		lastPeak = peak; // update lastPeak
		if (peak != 0)
		{ // if a peak was detected...
			timer = 0; // clear end count timer...
			if (peak > 0) // and increment corresponding count
			{
				countPos++;
			}
			else
			{
				countNeg++;
			}
			if (!counting)
			{ // if it's the first peak...
				counting = true; // start shake counting
				firstPeak = peak; // save the first peak direction
			}
		} 
		else // but if no peak detected...
		{
			if (counting)
			{ // and it was counting...
				timer += Time.deltaTime; // increment timer
				if (timer > endCountTime)
				{ // if endCountTime reached...
					counting = false; // finish counting...
					shakeDir = firstPeak; // inform direction of first shake...
					if (countPos > countNeg) // and return the higher count
					{
						shakeCount = countPos;
					}
					else
					{
						shakeCount = countNeg;
					}
					// zero counters and become ready for next shake count
					countPos = 0;
					countNeg = 0;
					return true; // count finished
				}
			}
		}
		return false;
	}


}
