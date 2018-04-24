using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class OverriddenNetworkManager : NetworkManager
{
	public NetworkDiscovery discovery;

	private static int playerCount = 0;

	public GameObject[] playerOptions;

	public override void OnStartHost()
	{
		discovery.Initialize();
		discovery.StartAsServer();

	}

	public override void OnStartClient(NetworkClient client)
	{
		discovery.showGUI = false;
		
        
	}

	public override void OnStopClient()
	{
		discovery.StopBroadcast();
		discovery.showGUI = true;

		AdjustPlayerCount();
	}

	void Update()
	{
		playerCount = GameObject.FindGameObjectsWithTag("Player").Length;
		DebugPanel.Log("Players: ", "Network Manager", playerCount);
	}

	public static void AdjustPlayerCount()
	{
		playerCount = GameObject.FindGameObjectsWithTag("Player").Length;
	}

	public static int GetPlayerCount() { return playerCount; }

	// public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId)
	// {
	// 	// GameObject player = (GameObject)Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
    //     // player.GetComponent<Player>().color = Color.red;
    //     // NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);

	// 	Debug.Log("Player Number: " + playerCount);

	// 	if (playerCount == 0)
	// 	{
	// 		GameObject generalPlayer = (GameObject) Instantiate(spawnPrefabs[0], transform.position, Quaternion.identity);
	// 	}
	// 	else if (playerCount == 1)
	// 	{
	// 		GameObject generalPlayer = (GameObject) Instantiate(spawnPrefabs[1], transform.position, Quaternion.identity);
	// 	}
	// 	else if (playerCount == 2)
	// 	{
	// 		GameObject generalPlayer = (GameObject) Instantiate(spawnPrefabs[2], transform.position, Quaternion.identity);
	// 	}
	// }

	public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId)
	{
		if (playerOptions == null)
		{
			if (LogFilter.logError) { Debug.LogError("The Player Options Array is empty on the NetworkManager. Please setup a Player Options Array."); }
			return;
		}

		for (int i = 0; i < playerOptions.Length; i++)
		{
			if (playerOptions[i].GetComponent<NetworkIdentity>() == null)
			{
				if (LogFilter.logError) { Debug.LogError("The Player Options at index " + i + " does not have a NetworkIdentity. Please add a NetworkIdentity to that player option."); }
				return;
			}
		}

		if (playerControllerId < conn.playerControllers.Count  && conn.playerControllers[playerControllerId].IsValid && conn.playerControllers[playerControllerId].gameObject != null)
		{
			if (LogFilter.logError) { Debug.LogError("There is already a player at that playerControllerId for this connections."); }
			return;
		}

		GameObject player;
		Transform startPos = GetStartPosition();
		if (startPos != null)
		{
			player = (GameObject)Instantiate(playerOptions[playerCount], startPos.position, startPos.rotation);
		}
		else
		{
			player = (GameObject)Instantiate(playerOptions[playerCount], Vector3.zero, Quaternion.identity);
		}

		NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);
	}
}
