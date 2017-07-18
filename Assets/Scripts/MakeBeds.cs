using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class MakeBeds : MonoBehaviour 
{
	public static MakeBeds MakeBedsReference;
	public static float timeLimity = 3.0f;

	// string wrinkleName;

	// public EventSystem uiEventSystem;

	void Start () 
	{
		MakeBedsReference = this;
		// wrinkleName = "none";

	}
	
	void Update () 
	{
		// if (Input.GetMouseButtonDown(0))
		// {
		// 	wrinkleName = uiEventSystem.currentSelectedGameObject.name;
		// 	if (wrinkleName != "Wrinkle_02" || wrinkleName != "Wrinkle_03" || wrinkleName != "Wrinkle_01")
		// 	{
		// 		wrinkleName = "none";
		// 	}
		// 	DebugPanel.Log("Wrinkle Intercted: ",  wrinkleName);
		// }
	}
}
