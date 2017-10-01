using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroller : MonoBehaviour 
{
	public float scrollSpeed;	

	private Rigidbody2D backgroundRb;

	void Start()
	{
		backgroundRb = GetComponent<Rigidbody2D>();
		backgroundRb.velocity = new Vector2(scrollSpeed, 0);
	}

	void Update () 
	{
		
	}
}
