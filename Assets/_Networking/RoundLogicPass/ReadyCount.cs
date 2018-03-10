// Each client has access to the same sync var because this script is active on
// on players, both the local and remote versions on all clients.
// Where as the NetworkedRoundManager is only active on the local player version

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ReadyCount : NetworkBehaviour {

	public static ReadyCount ReadyCountReference;

	[SyncVar (hook = "OnReadyCountChanged")]
	public int crossClientReadyCount = 0;
	public bool countReadyStatus = false;

	void Start()
	{
		ReadyCountReference = this;
	}

	void OnEnable()
	{
		ReadyCountReference = this;
	}

	void Update ()
	{
		// DebugPanel.Log("Ready Count:", "Round Logic", crossClientReadyCount);

		if (crossClientReadyCount == 4)
		{
			//start round
			Debug.Log("All Players Ready");
		}

		if (Input.GetKeyDown(KeyCode.R))
		{

			UpdateGroupReadyCount();
			
		}
	}

 
	public void UpdateRemoteCopyCounts(int newCount)
	{
		crossClientReadyCount = newCount;
	}

	public void UpdateGroupReadyCount()
	{
		countReadyStatus = !countReadyStatus;

		crossClientReadyCount += countReadyStatus ? 1 : -1;

		

		Debug.Log("Count (Local): " + crossClientReadyCount );

		CmdToggleReady(crossClientReadyCount);
	}

	[Command]
	void CmdToggleReady(int count)
	{
		crossClientReadyCount = count;

		GameObject[] allPlayers = GameObject.FindGameObjectsWithTag("Player");
		ReadyCount[] allCounts = new ReadyCount[allPlayers.Length];
		for (int i = 0; i < allPlayers.Length; i++)
		{
			allCounts[i] = allPlayers[i].GetComponent<ReadyCount>();
			allCounts[i].UpdateRemoteCopyCounts(crossClientReadyCount);
		}

		Debug.Log("Count (Command): " + crossClientReadyCount );
		Debug.Log("Arg (Command): " + count );

		RpcUpdateReadyStatus(crossClientReadyCount);
	}

	[ClientRpc]
	void RpcUpdateReadyStatus(int newCount)
	{
		crossClientReadyCount = newCount;
		Debug.Log("Count (Rpc): " + crossClientReadyCount );
		Debug.Log("Arg (Rpc): " + newCount );
	}

	void OnReadyCountChanged(int newCount)
	{
		crossClientReadyCount = newCount;
	}
}
