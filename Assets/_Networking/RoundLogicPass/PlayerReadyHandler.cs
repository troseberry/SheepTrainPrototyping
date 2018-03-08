using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class PlayerReadyHandler : NetworkBehaviour {

	[SyncVar]
	public string readyStatusString = "Unready";
	public Text readyWorldText;
	public Text readyButtonText;
	public bool readyStatus = false;

	public Text countdownText;
	private float startingCountdownRef;

	public Canvas readyCanvas;
	public Canvas readyWorldCanvas;

	
	void Start()
	{
		if (isLocalPlayer)
		{
			readyCanvas.enabled = true;
		}
	}

	void OnEnable()
	{
		if (isLocalPlayer)
		{
			readyCanvas.enabled = true;
		}
	}

	void Update()
	{
		startingCountdownRef = NetworkedRoundManager.GetStartCountdown();
		if (NetworkedRoundManager.AreAllPlayersReady())
		{
			readyCanvas.transform.GetChild(0).gameObject.SetActive(false);
			readyWorldCanvas.enabled = false;

			if (startingCountdownRef >= 0)
			{
				countdownText.enabled = true;
				countdownText.text = Mathf.CeilToInt(startingCountdownRef).ToString();
			}
			else if (startingCountdownRef >= -1f && startingCountdownRef < 0)
			{
				countdownText.text = "GO!";
			}
			else
			{
				readyCanvas.enabled = false;
			}
		}
		
	}

	public void ToggleReady()
	{
		NetworkedRoundManager.NRM.UpdateGroupReadyCount();

		readyStatus = !readyStatus;

		readyStatusString = readyStatus ? "Ready" : "Unready";

		readyWorldText.text = readyStatusString;
		readyButtonText.text = readyStatusString;

		CmdToggleReady(readyStatusString);
	}

	[Command]
	void CmdToggleReady(string statusString)
	{
		readyStatusString = statusString;
		readyWorldText.text = readyStatusString;

		RpcUpdateReadyStatus(readyStatusString);
	}

	[ClientRpc]
	void RpcUpdateReadyStatus(string newStatus)
	{
		readyWorldText.text = newStatus;
	}
}