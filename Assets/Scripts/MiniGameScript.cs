using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using UnityEngine.Networking;


// Make these networked/inherit from NetworkBehavior
// Have the scripts send their status to the server

public class MiniGameScript : MonoBehaviour 
{
	public virtual float GetTimeLimit() {return 0f;}
	public virtual void ResetGame(){}

	public void SetActive()
	{
		isActive = true;
		deletionTimer = RoundManager.timeBeforeDeletion;
	}

	public int gameIndex;

	public bool isActive = false;
	public bool isBeingPlayed = false;

	private float deletionTimer;


	public IEnumerator DeleteTask()
	{
		Debug.Log(gameObject.name + " Deletion in " + RoundManager.timeBeforeDeletion + " Seconds...");
		yield return new WaitForSeconds(RoundManager.timeBeforeDeletion);

		if (!isBeingPlayed)
		{
			isActive = false;
			RoundManager.SetMiniGameStatusInactive(gameIndex);

			Debug.Log(gameObject.name + " Task Deleted");
		}
	}
}