using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class PlayerReadyHandler : NetworkBehaviour {

	[SyncVar]
	public string readyStatusString = "Unready";
	public Text readyStatusPlayerText;
	public Text readyStatusButtonText;
	public bool readyStatus = false;

	void Update()
	{
		
	}

	public void ToggleReady()
	{
		NetworkedRoundManager.NRM.UpdateGroupReadyCount();

		readyStatus = !readyStatus;

		readyStatusString = readyStatus ? "Ready" : "Unready";

		readyStatusPlayerText.text = readyStatusString;
		readyStatusButtonText.text = readyStatusString;

		CmdToggleReady(readyStatusString);
	}

	[Command]
	void CmdToggleReady(string statusString)
	{
		readyStatusString = statusString;
		readyStatusPlayerText.text = readyStatusString;

		RpcUpdateReadyStatus(readyStatusString);
	}

	[ClientRpc]
	void RpcUpdateReadyStatus(string newStatus)
	{
		readyStatusPlayerText.text = newStatus;
	}
}