using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChaosMeter : MonoBehaviour 
{
	public static ChaosMeter ChaosMeterReference;

	public RectTransform fillMaskTransform;

	private float currentMeterValue;
	private float nextMeterValue;
	private float meterLerpValue;

	void Start () 
	{
		ChaosMeterReference = this;
	}
	
	void Update () 
	{
		// DebugPanel.Log("Current Chaos Meter: ", currentSliderValue);
		// DebugPanel.Log("Next Chaos Meter: ", nextSliderValue);
		// DebugPanel.Log("Meter Value: ", meterValue);
		// DebugPanel.Log(" Offset: ", fillMaskTransform.offsetMax);

		if (NetworkedChaosManager.ChaosManagerReference)
		{
			if (RoundManager.GetRoundTimer() > 0)
			{
				currentMeterValue = Mathf.Ceil(Mathf.Abs(fillMaskTransform.offsetMax.x));
				nextMeterValue = (float) (600 -(NetworkedChaosManager.ChaosManagerReference.GetChaosValue() * 6));

				if (nextMeterValue != currentMeterValue)
				{
					meterLerpValue = Mathf.Lerp(currentMeterValue, nextMeterValue, Time.deltaTime * 15);

					fillMaskTransform.offsetMax = new Vector2(-meterLerpValue,fillMaskTransform.offsetMax.y );

					if (meterLerpValue <= (nextMeterValue * 1.01))
					{
						// Close enough. lerp won't get exactly to value unless it has more
						// time. 15x speed on the lerp isn't enough. but faster doesn't
						// show a lot of movement
						fillMaskTransform.offsetMax = new Vector2(-nextMeterValue,fillMaskTransform.offsetMax.y );
					}
				}
			}
		}
	}

	public static void ResetChaosMeter()
	{ 
		ChaosMeterReference.fillMaskTransform.offsetMax = new Vector2(-600 ,ChaosMeterReference.fillMaskTransform.offsetMax.y );
	}
}