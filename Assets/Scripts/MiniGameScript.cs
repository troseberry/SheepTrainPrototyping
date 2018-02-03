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


	public int gameIndex;

	public bool isActive = false;
	public bool isBeingPlayed = false;

	private float deletionTimer;

	public Image activeImage;
	public Image inactiveImage;

	public void SetGameActive()
	{
		isActive = true;
		if (activeImage) activeImage.enabled = true;
		if (inactiveImage) inactiveImage.enabled = false;
	}

	public void SetGameInactive()
	{
		isActive = false;
		isBeingPlayed = false;

		if (activeImage) activeImage.enabled = false;
		if (inactiveImage) inactiveImage.enabled = true;
	}

	// Need to stop deletion if the task gets "answered"
	public IEnumerator DeleteTask()
	{
		Debug.Log(gameObject.name + " Deletion in " + RoundManager.timeBeforeDeletion + " Seconds...");
		yield return new WaitForSeconds(RoundManager.timeBeforeDeletion);

		if (!isBeingPlayed)
		{
			SetGameInactive();
			RoundManager.SetMiniGameStatusInactive(gameIndex);

			Debug.Log(gameObject.name + " Task Deleted");
		}
	}
}