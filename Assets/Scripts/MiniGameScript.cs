using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGameScript : MonoBehaviour 
{
	public virtual float GetTimeLimit() {return 0f;}
	public virtual void ResetGame(){}
}