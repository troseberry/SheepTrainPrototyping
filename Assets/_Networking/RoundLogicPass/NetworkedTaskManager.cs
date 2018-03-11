using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkedTaskManager : NetworkBehaviour 
{
	public static NetworkedTaskManager TaskManagerReference;

	[SyncVar (hook = "OnIndexGenerated")]
	public int generatedIndex;

	// private static List<int> inactiveGameIndexes = new List<int> {0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14};
	// private static List<int> activeGameIndexes = new List<int>();


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

	public void GenerateIndex(List<int> inactiveGameIndexes)
	{
		int chosenIndex = inactiveGameIndexes[Random.Range(0, inactiveGameIndexes.Count)];

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

		RpcUpdateGeneratedIndex(generatedIndex);
	}

	[ClientRpc]
	void RpcUpdateGeneratedIndex(int newIndex)
	{
		generatedIndex = newIndex;
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