using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMap : MonoBehaviour 
{
	public static MiniMap MiniMapReference;

	public FlashIcon[] carNotificationIcons;

	void Start () 
	{
		MiniMapReference = this;
	}
	
	void Update () 
	{
		
	}

	public static void DisplayNotification(int carIndex)
	{
		MiniMapReference.carNotificationIcons[carIndex].TriggerIcon();
	}

	public static void HideNotification(int carIndex)
	{
		MiniMapReference.carNotificationIcons[carIndex].FadeOutIcon();	
	}
}