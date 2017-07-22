using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridPosition : MonoBehaviour 
{
	float horizontalPosition;
	float verticalPosition;

	bool isOccupied;

	public GridPosition(float horz, float vert)
	{
		horizontalPosition = horz;
		verticalPosition = vert;
	}

	void Start () 
	{
		
	}
	
	void Update () 
	{
		
	}

	public void SetIsOccupied(bool state)
	{
		isOccupied = state;
	}

	public bool GetIsOccupied()
	{
		return isOccupied;
	}
}
