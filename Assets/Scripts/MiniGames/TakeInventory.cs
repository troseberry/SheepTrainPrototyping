using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TakeInventory : MiniGameScript 
{
	public static TakeInventory CheckInventoryReference;
	public static float timeLimit = 5.0f;
	bool isComplete = false;

	private int squareCount = 4;
	private int circleCount = 4;
	private int redCount = 4;
	private int greenCount = 4;
	private int smallCount = 4;
	private int largeCount = 4;

	private int countedSquare = 0;
	private int countedCircle = 0;
	private int countedRed = 0;
	private int countedGreen = 0;
	private int countedSmall = 0;
	private int countedLarge = 0;


	GridPosition[] gridPositions;
	public GameObject[] inventoryItems;

	public GameObject squareButton;
	public GameObject circleButton;
	public GameObject redButton;
	public GameObject greenButton;
	public GameObject largeButton;
	public GameObject smallButton;

	public Text shapeCountText, colorCountText, sizeCountText;
	string currentShapeCount, currentColorCount, currentSizeCount;


	bool correctShape, correctColor, correctSize;
	
	bool doStartGame = true;

	void Start () 
	{
		CheckInventoryReference = this;

		gridPositions = new GridPosition[8] {
			new GridPosition(-600, 275),
			new GridPosition(-200, 275),
			new GridPosition(200, 275),
			new GridPosition(600, 275),
			new GridPosition(-600, -25),
			new GridPosition(-200, -25),
			new GridPosition(200, -25),
			new GridPosition(600, -25)
		};

		currentShapeCount = "0";
		currentColorCount = "0";
		currentSizeCount = "0";
	}
	
	void Update () 
	{
		shapeCountText.text = currentShapeCount;
		colorCountText.text = currentColorCount;
		sizeCountText.text = currentSizeCount;

		if (!isComplete && PlayerMiniGameHandler.IsInGame() && doStartGame)
		{
			RandomizeItems();
			RandomizeButtons();
			doStartGame = false;
		}
		CheckCounts();

		if (PlayerMiniGameHandler.timer > 0) isComplete = (correctShape && correctColor && correctSize);

		if (isComplete || PlayerMiniGameHandler.timer == 0f) EndGame();
	}

	void RandomizeItems()
	{
		for (int i = 0; i < 8; i++)
		{
			inventoryItems[i].GetComponent<RawImage>().enabled = true;
		}

		squareCount = 4;
		circleCount = 4;
		redCount = 4;
		greenCount = 4;
		smallCount = 4;
		largeCount = 4;

		ArrayList randomNumbers = new ArrayList();

		int firstItem = (int) Random.Range(0f, 7f);
		randomNumbers.Add(firstItem);

		
		int secondItem = (int) Random.Range(0f, 7f);
		while(randomNumbers.Contains(secondItem))
		{
			secondItem = (int) Random.Range(0f, 7f);
		}
		randomNumbers.Add(secondItem);


		int thirdItem = (int) Random.Range(5f, 7f);
		while(randomNumbers.Contains(thirdItem))
		{
			thirdItem = (int) Random.Range(0f, 7f);
		}
		randomNumbers.Add(thirdItem);

		inventoryItems[firstItem].GetComponent<RawImage>().enabled = false;
		inventoryItems[secondItem].GetComponent<RawImage>().enabled = false;
		inventoryItems[thirdItem].GetComponent<RawImage>().enabled = false;



		for (int i = 0; i < 3; i++)
		{
			if (inventoryItems[(int)randomNumbers[i]].name.Contains("Large"))
			{
				largeCount--;
			}
			else
			{
				smallCount--;
			}

			if (inventoryItems[(int)randomNumbers[i]].gameObject.name.Contains("Red"))
			{
				redCount--;
			}
			else
			{
				greenCount--;
			}

			if (inventoryItems[(int)randomNumbers[i]].gameObject.name.Contains("Circle"))
			{
				circleCount--;
			}
			else
			{
				squareCount--;
			}
		}
	}

	void RandomizeButtons()
	{
		squareButton.SetActive(false);
		circleButton.SetActive(false);
		redButton.SetActive(false);
		greenButton.SetActive(false);
		smallButton.SetActive(false);
		largeButton.SetActive(false);



		int randomShape = Random.Range(0, 2);
		int randomColor = Random.Range(0, 2);
		int randomSize = Random.Range(0, 2);

		// Debug.Log("Random Shape:" + randomShape);
		// Debug.Log("Random Color:" + randomColor);
		// Debug.Log("Random Size:" + randomSize);

		if (randomShape == 0)
		{
			squareButton.SetActive(true);
		}
		else
		{
			circleButton.SetActive(true);
		}


		if (randomColor == 0)
		{
			redButton.SetActive(true);
		}
		else
		{
			greenButton.SetActive(true);
		}

		if (randomSize == 0)
		{
			largeButton.SetActive(true);
		}
		else
		{
			smallButton.SetActive(true);
		}
	}


	public void countSquare()
	{
		countedSquare++;
		currentShapeCount = "" + countedSquare;
	}

	public void countCircle()
	{
		countedCircle++;
		currentShapeCount = "" + countedCircle;
	}

	public void countRed()
	{
		countedRed++;
		currentColorCount = "" + countedRed;
	}

	public void countGreen()
	{
		countedGreen++;
		currentColorCount = "" + countedGreen;
	}

	public void countLarge()
	{
		countedLarge++;
		currentSizeCount = "" + countedLarge;
	}

	public void countSmall()
	{
		countedSmall++;
		currentSizeCount = "" + countedSmall;
	}




	void CheckCounts()
	{
		if (squareButton.activeSelf)
		{
			correctShape = (squareCount == countedSquare);
		}
		else
		{
			correctShape = (circleCount == countedCircle);
		}

		if (redButton.activeSelf)
		{
			correctColor = (redCount == countedRed);
		}
		else
		{
			correctColor = (greenCount == countedGreen);
		}

		if (largeButton.activeSelf)
		{
			correctSize = (largeCount == countedLarge);
		}
		else
		{
			correctSize = (smallCount == countedSmall);
		}
	}



	void EndGame()
	{
		PlayerMiniGameHandler.EndMiniGame(isComplete);
	}

	public override void ResetGame()
	{
		countedSquare = 0;
		countedCircle = 0;
		countedRed = 0;
		countedGreen = 0;
		countedSmall = 0;
		countedLarge = 0;
		
		currentShapeCount = "0";
		currentColorCount = "0";
		currentSizeCount = "0";

		shapeCountText.text = currentShapeCount;
		colorCountText.text = currentColorCount;
		sizeCountText.text = currentSizeCount;

		doStartGame = true;
	}

	public override float GetTimeLimit() { return timeLimit; }
}
