using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
// using UnityEngine.Networking;


// Make these networked/inherit from NetworkBehavior
// Have the scripts send their status to the server

public class MiniGameScript : MonoBehaviour 
{
	public virtual float GetTimeLimit() {return 0f;}
	public virtual void ResetGame(){}

	public TaskDeletionTimer deletionTimerUI;

	public int gameIndex;		// this is assigned in the PlayerMiniGameHandler

	public bool isActive = false;
	public bool isBeingPlayed = false;

	private float deletionTimer;
	public bool awaitingDeletion = false;

	public Image activeImage;
	public Image inactiveImage;

	private bool interruptWait = false;

	public void SetGameActive()
	{
		isActive = true;
		awaitingDeletion = true;

		deletionTimerUI.gameObject.SetActive(true);
		deletionTimerUI.StartTimerCoroutine(deletionTimer);

		if (activeImage) activeImage.enabled = true;
		if (inactiveImage) inactiveImage.enabled = false;
	}

	public void SetGameBusy()
	{
		isBeingPlayed = true;
		awaitingDeletion = false;

		// deletionTimerUI.gameObject.SetActive(false);
		// Debug.Log("Game Busy");
	}

	public void SetGameInactive()
	{
		isActive = false;
		isBeingPlayed = false;
		awaitingDeletion = false;

		deletionTimerUI.gameObject.SetActive(false);

		if (activeImage) activeImage.enabled = false;
		if (inactiveImage) inactiveImage.enabled = true;
	}

	public void SetDeletionTime(float time) { deletionTimer = time; }
}