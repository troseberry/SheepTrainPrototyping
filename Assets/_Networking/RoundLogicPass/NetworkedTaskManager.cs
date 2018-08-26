using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkedTaskManager : NetworkBehaviour 
{
	public static NetworkedTaskManager TaskManagerReference;

	// [SyncVar (hook = "OnIndexGenerated")]
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
		if (Input.GetKeyDown(KeyCode.Space))
		{
			GenerateIndex(TaskManager.TaskManagerReference.GetInactiveGameIndexes());
		}

		#region DEBUG
		// string inactive = "";
		// for (int j = 0; j < TaskManager.TaskManagerReference.GetInactiveGameIndexes().Count; j ++)
		// {
		// 	inactive += (TaskManager.TaskManagerReference.GetInactiveGameIndexes()[j] + ", ");
		// }
		// DebugPanel.Log("Inactive List: ", "NTM", inactive);

		// string active = "";
		// for (int l = 0; l < TaskManager.TaskManagerReference.GetActiveGameIndexes().Count; l ++)
		// {
		// 	active += (TaskManager.TaskManagerReference.GetActiveGameIndexes()[l] + ", ");
		// }
		// DebugPanel.Log("Active List: ", "NTM", active);
		#endregion
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

		// int chosenIndex = Random.Range(0, 3);

		generatedIndex = chosenIndex;
		CmdGenerateIndex(chosenIndex);
	}

	[Command]
	void CmdGenerateIndex(int genIndex)
	{
		generatedIndex = genIndex;

		// GameObject[] allPlayers = GameObject.FindGameObjectsWithTag("Player");
		// for (int i = 0; i < allPlayers.Length; i++)
		// {
		// 	allPlayers[i].GetComponent<NetworkedTaskManager>().UpdateRemoteGeneratedIndex(generatedIndex);
		// }

		Debug.Log("Generated Index (CMD): " + generatedIndex + "(" + name + ")");
		RpcUpdateGeneratedIndex(generatedIndex);
	}

	[ClientRpc]
	void RpcUpdateGeneratedIndex(int newIndex)
	{
		generatedIndex = newIndex;
		Debug.Log("Generated Index (RPC): " + newIndex + "(" + name + ")");
	}

	void OnIndexGenerated(int genIndex)
	{
		generatedIndex = genIndex;
	}

	public int GetGeneratedIndex() { return generatedIndex; }
}