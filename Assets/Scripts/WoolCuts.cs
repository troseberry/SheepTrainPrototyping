using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class WoolCuts : MonoBehaviour 
{
	public static WoolCuts WoolCutsReference;
	public static float timeLimit = 5f;

	// public GameObject[] traceImages;
	public Transform traceSquare;
	private bool pointerIsOver = false;

	bool doStartGame = true;

	void Start () 
	{
		WoolCutsReference = this;
	}
	
	void Update () 
	{
		// if (MiniGameManager.ManagerReference.IsInGame())
		// {

		// }
	}

	public void TracingImage()
	{
		Debug.Log("Tracing");
		Debug.Log(pointerIsOver);
	}

	public void ChangePointerStatus()
	{
		pointerIsOver = !pointerIsOver;
	}
}