using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChaosMeter : MonoBehaviour 
{
	public static ChaosMeter ChaosMeterReference;

	public Slider chaosMeterSlider;

	private float currentSliderValue;
	private float nextSliderValue;

	void Start () 
	{
		ChaosMeterReference = this;
	}
	
	void Update () 
	{
		if (RoundManager.GetRoundTimer() > 0)
		{
			nextSliderValue = (float) ChaosManager.GetChaosValue() / 100;
			currentSliderValue = chaosMeterSlider.value;

			if (nextSliderValue != currentSliderValue)
			{
				chaosMeterSlider.value = Mathf.Lerp(currentSliderValue, nextSliderValue, Time.deltaTime * 5);
			}
		}
	}

	public static void ResetChaosMeter() { ChaosMeterReference.chaosMeterSlider.value = 0; }
}