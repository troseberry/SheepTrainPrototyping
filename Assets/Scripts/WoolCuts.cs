using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class WoolCuts : MiniGameScript 
{
	public static WoolCuts WoolCutsReference;
	public static float timeLimit = 5f;
	private bool isComplete;

	public GameObject[] possibleTraceObjects;
	int chosenTraceObjectIndex;
	
	private bool pointerIsOver = false;

	bool doStartGame = true;

	private bool startedTrace = false;
	public List<GameObject> tracedZones;
	private GameObject previouslyTraced;
	private GameObject currentlyTracing;
	
	private int traceObjectZoneCount = 16;

	private bool outOfSequence = false;
	private int lastZoneInSequence;

	void Start () 
	{
		WoolCutsReference = this;

		//Should do this at the start of the game instead of here
		//Only needed as a precaution since we're using a custom Standalone Input module class
		EventSystem.current.gameObject.GetComponent<ExtendedStandaloneInputModule>().forceModuleActive = true;
	}
	
	void Update () 
	{
		// Debug.Log("Pointer Is Over: " + pointerIsOver);
		// Debug.Log("Is Complete: " + isComplete);
		// Debug.Log("Started Trace: " + startedTrace);
		// Debug.Log("Do Start Game: " + doStartGame);

		if (!isComplete && PlayerMiniGameHandler.IsInGame() && doStartGame)
		{
			GenerateTraceImage();
			doStartGame = false;
		}

		if (isComplete || PlayerMiniGameHandler.timer == 0) EndGame();

		if (PlayerMiniGameHandler.timer > 0) isComplete = (tracedZones.Count == 16);
	}

	void GenerateTraceImage()
	{
		chosenTraceObjectIndex = Random.Range(0, possibleTraceObjects.Length);

		possibleTraceObjects[chosenTraceObjectIndex].SetActive(true);
	}

	public void TracingImage()
	{
		if (PlayerMiniGameHandler.timer > 0)
		{
			if (pointerIsOver)
			{
				if (!startedTrace)
				{
					startedTrace = true;
					previouslyTraced = ExtendedStandaloneInputModule.GetPointerEventData().pointerEnter;
					currentlyTracing = ExtendedStandaloneInputModule.GetPointerEventData().pointerEnter;
				}
				else
				{
					previouslyTraced = currentlyTracing;
					currentlyTracing = ExtendedStandaloneInputModule.GetPointerEventData().pointerEnter;
				}
			}
			
			if (!previouslyTraced.name.Equals(currentlyTracing.name))
			{
				if (!outOfSequence)
				{
					if (ZoneIsNextInSequence(previouslyTraced, currentlyTracing)) AddToTrace(previouslyTraced);
				}
				else
				{
					if (ZoneIsNextInSequence(currentlyTracing)) AddToTrace(currentlyTracing);
				}
			}
		}
	}

	public void StoppedTracing()
	{
		if (PlayerMiniGameHandler.timer > 0)
		{
			if (tracedZones.Count != 16)
			{
				for (int i = 0; i < tracedZones.Count; i++)
				{
					tracedZones[i].GetComponent<TraceZone>().Reset();
				}
				tracedZones.Clear();
				startedTrace = false;
			}
		}
	}

	public void ChangePointerStatus()
	{
		pointerIsOver = !pointerIsOver;
	}
	
	public void AddToTrace(GameObject tracedObject)
	{
		if ((tracedZones.Count < traceObjectZoneCount) && (previouslyTraced.name.Contains("Zone")) && !tracedZones.Contains(tracedObject))
		{
			tracedObject.GetComponent<TraceZone>().Trace();
			tracedZones.Add(tracedObject);
		}
	}

	bool ZoneIsNextInSequence(GameObject current, GameObject next)
	{
		int currentZone = int.Parse(current.name.Split('_')[1]);
		int nextZone = int.Parse(next.name.Split('_')[1]);

		// Debug.Log("Curent: " + currentZone);
		// Debug.Log("Next: " + nextZone);


		bool inSeq = (nextZone == currentZone + 1)
		|| (nextZone == currentZone - 1)
		|| (nextZone == currentZone % (traceObjectZoneCount - 1))
		|| (nextZone == (currentZone + (traceObjectZoneCount - 1)) % traceObjectZoneCount);

		if (!inSeq) 
		{
			lastZoneInSequence = currentZone;
			outOfSequence = true;
			Debug.Log("Out Of Sequence: " + currentZone + " | " + nextZone);
		}

		return inSeq;
	}

	bool ZoneIsNextInSequence(GameObject next)
	{
		int nextZone = int.Parse(next.name.Split('_')[1]);

		bool inSeq = (nextZone == lastZoneInSequence + 1)
		|| (nextZone == lastZoneInSequence - 1)
		|| (nextZone == lastZoneInSequence % (traceObjectZoneCount - 1))
		|| (nextZone == (lastZoneInSequence + (traceObjectZoneCount - 1)) % traceObjectZoneCount);

		if (!inSeq) 
		{
			outOfSequence = true;
			Debug.Log("Out Of Sequence: " + lastZoneInSequence + " | " + nextZone);
		}
		else
		{
			outOfSequence = false;
		}

		return inSeq;
	}

	void EndGame()
	{
		PlayerMiniGameHandler.EndMiniGame(isComplete);
	}

	public override void ResetGame()
	{
		for (int i = 0; i < tracedZones.Count; i++)
		{
			tracedZones[i].GetComponent<TraceZone>().Reset();
		}
		tracedZones.Clear();
		startedTrace = false;
		pointerIsOver = false;
		outOfSequence = false;

		possibleTraceObjects[chosenTraceObjectIndex].SetActive(false);

		isComplete = false;
		doStartGame = true;
	}

	public override float GetTimeLimit() { return timeLimit; }
}