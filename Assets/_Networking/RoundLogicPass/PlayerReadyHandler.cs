using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class PlayerReadyHandler : NetworkBehaviour {

	[SyncVar]
	public string readyStatusString = "Unready";

	[SyncVar (hook = "OnReadyCountChanged")]
	public int crossClientReadyCount = 0;

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
		startingCountdownRef = RoundManager.GetStartCountdown();
		if (RoundManager.AreAllPlayersReady())
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
			else if (startingCountdownRef >= -2f && startingCountdownRef < -1f)
			{
				countdownText.enabled = false;
				readyCanvas.enabled = false;
			}
		}

		if (startingCountdownRef <= -2f)
			{
				// crossClientReadyCount = 0;
				// CmdToggleReady(0, "Unready");
				Debug.Log("Ready Handler. Clear Ready by Toggle");
				if (readyStatus) ToggleReset();
			}

		if (RoundManager.GetRoundTimer() <= 0 || ChaosManager.ReachedMaxChaos())
		{
			if (!readyCanvas.enabled)
			{
				Debug.Log("Reset Calls?");
			
				
				// ToggleReady();
				
				ResetPlayerRoundItems();
			}
		}
		
	}

	public void ToggleReady()
	{
		readyStatus = !readyStatus;
		crossClientReadyCount += readyStatus ? 1 : -1;

		readyStatusString = readyStatus ? "Ready" : "Unready";

		readyWorldText.text = readyStatusString;
		readyButtonText.text = readyStatusString;

		CmdToggleReady(crossClientReadyCount, readyStatusString);
	}

	// These two methods also update the ready world canvas across clients.
	// That canvas is currently disabled on the player prefab
	[Command]
	void CmdToggleReady(int count, string statusString)
	{
		crossClientReadyCount = count;

		GameObject[] allPlayers = GameObject.FindGameObjectsWithTag("Player");
		PlayerReadyHandler[] allCounts = new PlayerReadyHandler[allPlayers.Length];
		for (int i = 0; i < allPlayers.Length; i++)
		{
			allCounts[i] = allPlayers[i].GetComponent<PlayerReadyHandler>();
			allCounts[i].UpdateRemoteCopyCounts(crossClientReadyCount);
		}

		readyStatusString = statusString;
		readyWorldText.text = readyStatusString;

		RpcUpdateReadyStatus(crossClientReadyCount, readyStatusString);
	}

	[ClientRpc]
	void RpcUpdateReadyStatus(int newCount, string newStatus)
	{
		crossClientReadyCount = newCount;
		readyWorldText.text = newStatus;
	}

	public void UpdateRemoteCopyCounts(int newCount)
	{
		crossClientReadyCount = newCount;
	}

	void OnReadyCountChanged(int newCount)
	{
		crossClientReadyCount = newCount;
	}

	public void ToggleReset()
	{
		readyStatus = false;
		crossClientReadyCount = 0;

		readyStatusString = "Unready";

		readyWorldText.text = readyStatusString;
		readyButtonText.text = readyStatusString;

		CmdToggleReady(crossClientReadyCount, readyStatusString);
	}

	public void ResetPlayerRoundItems()
	{
		readyStatus = false;
		readyStatusString = "Unready";
		readyButtonText.text = readyStatusString;

		readyCanvas.transform.GetChild(0).gameObject.SetActive(true);
		readyCanvas.enabled = true;

		Debug.Log("Ready Status: " + readyStatus);
		Debug.Log("Ready String: " + readyStatusString);
	}
}