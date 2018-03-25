using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkedTaskManager : NetworkBehaviour 
{
	public static NetworkedTaskManager TaskManagerReference;

	[SyncVar (hook = "OnIndexGenerated")]
	public int generatedIndex;


	void Start () 
	{
		TaskManagerReference = this;
	}

	void OnEnable () 
	{
		TaskManagerReference = this;
	}
	
	void Update () 
	{
		
	}

	public static void StartTaskGeneration()
	{
		Debug.Log("Started Task Generation");
	}

	[Server]
	public void GenerateIndex(List<int> inactiveGameIndexes)
	{
		// Debug.Log("Server Calls: ");
		int chosenIndex = inactiveGameIndexes[Random.Range(0, inactiveGameIndexes.Count)];

		generatedIndex = chosenIndex;
		CmdGenerateIndex(chosenIndex);
	}

	[Command]
	void CmdGenerateIndex(int genIndex)
	{
		generatedIndex = genIndex;

		GameObject[] allPlayers = GameObject.FindGameObjectsWithTag("Player");
		for (int i = 0; i < allPlayers.Length; i++)
		{
			allPlayers[i].GetComponent<NetworkedTaskManager>().UpdateRemoteGeneratedIndex(generatedIndex);
		}

		// Debug.Log("Generated Index (CMD): " + generatedIndex);
		RpcUpdateGeneratedIndex(generatedIndex);
	}

	[ClientRpc]
	void RpcUpdateGeneratedIndex(int newIndex)
	{
		generatedIndex = newIndex;
		// Debug.Log("Generated Index (RPC): " + newIndex);
	}

	void UpdateRemoteGeneratedIndex(int newIndex)
	{
		generatedIndex = newIndex;
	}

	void OnIndexGenerated(int genIndex)
	{
		generatedIndex = genIndex;
	}

	public int GetGeneratedIndex() { return generatedIndex; }
}