// Each client has access to the same sync var because this script is active on
// on players, both the local and remote versions on all clients.
// Where as the NetworkedRoundManager is only active on the local player version

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ReadyCount : NetworkBehaviour {

	[SyncVar]
	public int crossClientReadyCount = 0;
	public bool countReadyStatus = false;

	void Update ()
	{
		DebugPanel.Log("Ready Count:", "Round Logic", crossClientReadyCount);

		if (crossClientReadyCount == 2)
		{
			//start round
			Debug.Log("All Players Ready");
		}

		if (Input.GetKeyDown(KeyCode.R))
		{
			UpdateGroupReadyCount();
		}
	}

 
	public void UpdateGroupReadyCount()
	{
		countReadyStatus = !countReadyStatus;

		crossClientReadyCount += countReadyStatus ? 1 : -1;
		CmdToggleReady(crossClientReadyCount);
	}

	[Command]
	void CmdToggleReady(int count)
	{
		crossClientReadyCount = count;

		RpcUpdateReadyStatus(crossClientReadyCount);
	}

	[ClientRpc]
	void RpcUpdateReadyStatus(int newCount)
	{
		crossClientReadyCount = newCount;
	}
}
