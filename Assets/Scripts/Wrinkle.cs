using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Wrinkle : MonoBehaviour 
{
	bool startedPinching;
	bool wrinkleDone;

	RawImage wrinkleImage;
	
	float pinchThreshold = -5.0f;

	void Start () 
	{
		startedPinching = false;
		wrinkleDone = false;
		wrinkleImage = GetComponent<RawImage>();
	}
	
	void Update () 
	{
		DebugPanel.Log(gameObject.name + ": ", startedPinching);
		if (startedPinching)
		{
			//check for pinch gesture
			//stop running checks when the user lifts two fingers. call blockPinching()
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


				if (deltaMagnitudeDiff <= pinchThreshold)
				{
					wrinkleDone = true;
					wrinkleImage.enabled = false;
					// completedCheckText.text = "True";
				}
			}
			
		}
	}


	public void RegisterPinching ()
	{
		startedPinching = true;
	}

	public void blockPinching()
	{
		startedPinching = false;
	}
}
