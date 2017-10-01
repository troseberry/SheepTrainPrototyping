using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundRepeater : MonoBehaviour 
{
	private BoxCollider2D backgroundCollider;
	private float backgroundHorizontalLength;


	void Awake()
	{
		backgroundCollider = GetComponent<BoxCollider2D>();
		backgroundHorizontalLength = backgroundCollider.size.x;
	}

	void Update() 
	{
		if (transform.position.x > backgroundHorizontalLength)
		{
			RepositionBackground();
		}
	}

	void RepositionBackground()
	{
		Vector2 backgroundOffset = new Vector2(-backgroundHorizontalLength, 0);

		transform.position = backgroundOffset;
	}
}
