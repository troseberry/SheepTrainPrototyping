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
		if (transform.localPosition.x > (backgroundHorizontalLength * 3f))
		{
			RepositionBackground();
		}
	}

	void RepositionBackground()
	{
		Vector2 backgroundOffset = new Vector2(-backgroundHorizontalLength * 3f, 0);

		transform.localPosition = backgroundOffset;
	}
}
