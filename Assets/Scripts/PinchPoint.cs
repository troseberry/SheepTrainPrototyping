using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PinchPoint : MonoBehaviour 
{
	private bool startedPinching;
	private bool pinchPointDone;

	private float pinchThreshold = 5.0f;

	public bool isVisible;
	private RawImage pinchPointImage;

	public bool outwardPinch;
	private bool pinchComparison;

	void Start () 
	{
		startedPinching = false;
		pinchPointDone = false;
		pinchPointImage = GetComponent<RawImage>();
	}
	
	void Update () 
	{
		if (startedPinching)
		{
			if (Input.touchCount == 2)
			{
				// Store both touches.
				Touch touchZero = Input.GetTouch(0);
				Touch touchOne = Input.GetTouch(1);

				// Find the position in the previous frame of each touch.
				Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
				Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

				// Find the magnitude of the vector (the distance) between the touches in each frame.
				float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
				float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

				// Find the difference in the distances between each frame.
				float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;
				// Debug.Log("Pinch Delta: " + deltaMagnitudeDiff);

				pinchComparison = outwardPinch ? (deltaMagnitudeDiff <= -pinchThreshold) : (deltaMagnitudeDiff >= pinchThreshold);

				if (pinchComparison)
				{
					pinchPointDone = true; 
					if (isVisible) pinchPointImage.enabled = false;
					startedPinching = false;
				}
			}
		}
	}

	public void RegisterPinching ()
	{
		startedPinching = true;
	}

	public void BlockPinching ()
	{
		startedPinching = false;
	}

	public bool IsPinchPointDone ()
	{
		return pinchPointDone;
	}

	public void ResetPinchPoint ()
	{
		pinchPointDone = false;
		if (isVisible) pinchPointImage.enabled = true;
	}

	public void EnableImage ()
	{
		if (isVisible) pinchPointImage.enabled = true;
	}

	public void DisableImage()
	{
		if (isVisible) pinchPointImage.enabled = false;
	}
}