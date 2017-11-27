using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TraceZone : MonoBehaviour 
{
	public Sprite startingSprite;
	public Sprite endingSprite;

	public Color startingColor;
	public Color endingColor;

	public bool useColor;

	public void Trace()
	{
		if (!useColor)
		{
			GetComponent<SpriteRenderer>().sprite = endingSprite;
		}
		else
		{
			GetComponent<SpriteRenderer>().color = endingColor;
		}
	}

	public void Reset()
	{
		if (!useColor)
		{
			GetComponent<SpriteRenderer>().sprite = startingSprite;
		}
		else
		{
			GetComponent<SpriteRenderer>().color = startingColor;
		}
	}
}