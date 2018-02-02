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

	public bool isActive = false;
	public bool isBeingPlayed = false;

	// void Update()
	// {
	// 	DebugPanel.Log(gameObject.name + " Status: [A]" + isActive + " [BP]" + isBeingPlayed, "Mini Game Scripts");
	// }
}