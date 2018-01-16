using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGameObject : MonoBehaviour 
{
	public MiniGameScript miniGameScript;

	public void ResetGame()
	{
		miniGameScript.ResetGame();
	}
}