using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;


// Track status for each mini game 
// Use this to determine if a game is able to be played
// Also use it to determine if another player is currently in that game


public class NetworkSyncTest : NetworkBehaviour 
{
	public static NetworkSyncTest NMGM;


	[SyncVar]
	public string lastPlayed = "None";

	public Text lastPlayedText;
	

	void Start () 
	{
		NMGM = this;

		Debug.Log(name + " Is Server: " + isServer);
	}
	
	void Update () 
	{
		if (Input.GetKeyDown(KeyCode.Space) && isLocalPlayer)
		{
			Debug.Log("Executed on Client");
			lastPlayed = "Space Test";
			CmdSetLastGame(lastPlayed);
		}

		lastPlayedText.text = lastPlayed;	
	}

	public void ChangeLastGame(string game)
	{
		if (isLocalPlayer)
		{
			lastPlayed = game;
			CmdSetLastGame(lastPlayed);
		}
	}

	[Command]
	public void CmdSetLastGame(string last)
	{
		
		Debug.Log("Called Server Command");

		lastPlayed = last;
	}

	public void SetLastText(Text last)
	{
		lastPlayedText = last;
	}
}