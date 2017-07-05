using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WakeGuests : MonoBehaviour 
{
	public static WakeGuests WakeGuestsReference;
	public static float timeLimit = 7.5f;

	float horzAcceleration;
	float vertAcceleration;
	float depthAcceleration;

	public Transform guestGroup;

	Transform guest_01;
	Transform guest_02;
	Transform guest_03;
	Transform guest_04;
	Transform guest_05;

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
		horzAcceleration = Input.acceleration.x;
		vertAcceleration = Input.acceleration.y;
		depthAcceleration = Input.acceleration.z;

		DebugPanel.Log("Acceleration X: ", horzAcceleration);
		DebugPanel.Log("Acceleration Y: ", vertAcceleration);
		DebugPanel.Log("Acceleration Z: ", depthAcceleration);	

		DebugPanel.Log("Move Duration: ", moveDuration);
		DebugPanel.Log("Current Move Time: ", currentMoveTime);
		DebugPanel.Log("Inital Horz: ", initialHorzPos.x);
		DebugPanel.Log("Next Horz: ", nextHorzPos.x);

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
		// guestGroup.localPosition = new Vector3(guestGroup.localPosition.x -1500, 0, 0);
		initialHorzPos = guestGroup.localPosition;
		nextHorzPos = new Vector3(-1250, 0, 0);
		doLerpGuestGroup = true;

		yield return new WaitForSeconds(1.5f);
		// guestGroup.localPosition = new Vector3(guestGroup.localPosition.x -1500, 0, 0);
		initialHorzPos = guestGroup.localPosition;
		nextHorzPos = new Vector3(-2500, 0, 0);
		doLerpGuestGroup = true;

		yield return new WaitForSeconds(1.5f);
		// guestGroup.localPosition = new Vector3(guestGroup.localPosition.x -1500, 0, 0);
		initialHorzPos = guestGroup.localPosition;
		nextHorzPos = new Vector3(-3750, 0, 0);
		doLerpGuestGroup = true;

		yield return new WaitForSeconds(1.5f);
		// guestGroup.localPosition = new Vector3(guestGroup.localPosition.x -1500, 0, 0);
		initialHorzPos = guestGroup.localPosition;
		nextHorzPos = new Vector3(-5000, 0, 0);
		doLerpGuestGroup = true;

		yield return new WaitForSeconds(1.5f);
		// guestGroup.localPosition = new Vector3(guestGroup.localPosition.x -1500, 0, 0);
		initialHorzPos = guestGroup.localPosition;
		nextHorzPos = new Vector3(-6250, 0, 0);
		doLerpGuestGroup = true;
	}

	void RandomizeGuestTimes()
	{
		guest_01.GetChild((int)Mathf.Round(Random.Range(0f, 1f))).GetComponent<RawImage>().enabled = true;
		guest_02.GetChild((int)Mathf.Round(Random.Range(0f, 1f))).GetComponent<RawImage>().enabled = true;
		guest_03.GetChild((int)Mathf.Round(Random.Range(0f, 1f))).GetComponent<RawImage>().enabled = true;
		guest_04.GetChild((int)Mathf.Round(Random.Range(0f, 1f))).GetComponent<RawImage>().enabled = true;
		guest_05.GetChild((int)Mathf.Round(Random.Range(0f, 1f))).GetComponent<RawImage>().enabled = true;
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
}
