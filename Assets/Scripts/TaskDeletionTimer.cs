using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaskDeletionTimer : MonoBehaviour
{
	public Image fillRadial;
	public RectTransform armPivot;

	public void StartTimerCoroutine(float deletionTime)
	{
		StartCoroutine(StartTimer(deletionTime));
	}

	IEnumerator StartTimer(float deletion)
	{
		float time = 0.0f;

		while (time < deletion)
		{
			time += Time.deltaTime * (Time.timeScale / deletion);

			fillRadial.fillAmount = Mathf.Lerp(1, 0, time);
			armPivot.rotation = Quaternion.Euler(0, 180, Mathf.Lerp(0, 360, time));

			yield return 0;
		}
		yield return null;
	}
}
