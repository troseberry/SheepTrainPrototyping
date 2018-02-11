using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlashIcon : MonoBehaviour 
{
	public Image iconImage;

	public Color visible;
	public Color invisible;
	private Color tempColor;

	private bool doLerpIconColor = false;
	private bool didFadeOut = false;
	private float flashDuration = 2f;
	private float currentFlashTime;

	void Start () 
	{
		currentFlashTime = flashDuration;
		// doLerpIconColor = true;
	}
	
	void Update () 
	{
		if (doLerpIconColor)
		{
			if (currentFlashTime < flashDuration)
			{
				currentFlashTime += Time.deltaTime;

				tempColor = Color.Lerp(visible, invisible, Mathf.PingPong(Time.time/0.25f, 1));

				iconImage.color = tempColor;
			}
			else
			{
				doLerpIconColor = false;

				if (!didFadeOut)
				{
					iconImage.color = visible;
					// Invoke("FadeOutIcon", 3f);
					didFadeOut = true;
				}
			}
		}
	}

	public void FadeOutIcon()
	{
		// tempColor = Color.Lerp(visible, invisible, Mathf.PingPong(Time.time, 1));
		iconImage.color = invisible;
	}

	public void TriggerIcon()
	{
		currentFlashTime = 0;
		doLerpIconColor = true;
		didFadeOut = false;
	}
}