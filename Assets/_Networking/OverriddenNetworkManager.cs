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
}
