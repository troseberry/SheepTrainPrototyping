using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class OverriddenNetworkManager : NetworkManager
{
	public NetworkDiscovery discovery;

	private static int playerCount = 0;

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
}
