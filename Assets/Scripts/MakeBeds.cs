using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class MakeBeds : MonoBehaviour 
{
	public static MakeBeds MakeBedsReference;
	public static float timeLimity = 3.0f;

	public Transform wrinkle_01;
	public Transform wrinkle_02;
	public Transform wrinkle_03;

	Vector2 randomWrinklePos_01 = Vector2.zero;
	Vector2 randomWrinklePos_02 = Vector2.zero;
	Vector2 randomWrinklePos_03 = Vector2.zero;	

	Range twoHorzRange_01;
	Range twoHorzRange_02;

	Range twoVertRange_01;
	Range twoVertRange_02;

	Range threeHorzRange_01;
	Range threeHorzRange_02;
	Range threeHorzRange_03;

	Range threeVertRange_01;
	Range threeVertRange_02;
	Range threeVertRange_03;

	bool enoughHorzSpaceOneTwo = true;
	bool enoughVertSpaceOneTwo = true;

	void Start () 
	{
		MakeBedsReference = this;

		
	}
	
	void Update () 
	{
		if (Input.GetButtonDown("Jump"))
		{
			randomizeWrinklePositions();
		}
	}

	void randomizeWrinklePositions()
	{
		randomWrinklePos_01 = new Vector2(Random.Range(-500, -300), Random.Range(-200, 200));
		randomWrinklePos_02 = new Vector2(Random.Range(-150, 150), Random.Range(-200, 200));
		randomWrinklePos_03 = new Vector2(Random.Range(300, 500), Random.Range(-200, 200));

		// oneToTwoDistance = Vector2.Distance(randomWrinklePos_01, randomWrinklePos_02);
		// oneToThreeDistance = Vector2.Distance(randomWrinklePos_01, randomWrinklePos_03);
		// twoToThreeDistance = Vector2.Distance(randomWrinklePos_02, randomWrinklePos_03);

		// while (oneToTwoDistance < 250) 
		// {
		// 	randomWrinklePos_02 = new Vector2(Random.Range(-500, 500), Random.Range(-200, 200));
		// 	oneToTwoDistance = Vector2.Distance(randomWrinklePos_01, randomWrinklePos_02);
		// }

		// while (oneToThreeDistance < 250 || twoToThreeDistance < 250)
		// {
		// 	randomWrinklePos_03 = new Vector2(Random.Range(-500, 500), Random.Range(-200, 200));

		// 	oneToThreeDistance = Vector2.Distance(randomWrinklePos_01, randomWrinklePos_03);
		// 	twoToThreeDistance = Vector2.Distance(randomWrinklePos_02, randomWrinklePos_03);
		// }

		
		// randomWrinklePos_01 = new Vector2(Random.Range(-500, -500), Random.Range(-200, 200));


		// twoHorzRange_01 = new Range(-500, randomWrinklePos_01.x - 100);
		// twoHorzRange_02 = new Range(randomWrinklePos_01.x + 100, 500);
		// twoVertRange_01 = new Range(-200, randomWrinklePos_01.y - 100);
		// twoVertRange_02 = new Range(randomWrinklePos_01.y + 100, 200);

		// randomWrinklePos_02 = new Vector2(RandomValueFromRanges(twoHorzRange_01, twoHorzRange_02), RandomValueFromRanges(twoVertRange_01, twoVertRange_02));

		// enoughHorzSpaceOneTwo = (Mathf.Abs(randomWrinklePos_01.x - randomWrinklePos_02.x) > 125);
		// enoughVertSpaceOneTwo = (Mathf.Abs(randomWrinklePos_01.y - randomWrinklePos_02.y) > 125);


		// if (randomWrinklePos_02.x > randomWrinklePos_01.x)
		// {
		// 	threeHorzRange_01 = new Range(-500, randomWrinklePos_01.x - 100);
		// 	threeHorzRange_02 = new Range(randomWrinklePos_01.x + 100, randomWrinklePos_02.x - 100);
		// 	threeHorzRange_03 = new Range(randomWrinklePos_02.x + 100, 500);
		// }
		// else
		// {
		// 	threeHorzRange_01 = new Range(randomWrinklePos_01.x + 100, 500);
		// 	threeHorzRange_02 = new Range(randomWrinklePos_01.x - 100, randomWrinklePos_02.x + 100);
		// 	threeHorzRange_03 = new Range(randomWrinklePos_02.x - 100, -500);
		// }

		// if (randomWrinklePos_02.y > randomWrinklePos_01.y)
		// {
		// 	threeVertRange_01 = new Range(-200, randomWrinklePos_01.y - 100);
		// 	threeVertRange_02 = new Range(randomWrinklePos_01.y + 100, randomWrinklePos_02.y - 100);
		// 	threeVertRange_03 = new Range(randomWrinklePos_02.y + 100, 200);
		// }
		// else
		// {
		// 	threeVertRange_01 = new Range(randomWrinklePos_01.y + 100, 200);
		// 	threeVertRange_02 = new Range(randomWrinklePos_01.y - 100, randomWrinklePos_02.y + 100);
		// 	threeVertRange_03 = new Range(randomWrinklePos_02.y - 100, -200);
		// }

		// if (enoughHorzSpaceOneTwo && enoughVertSpaceOneTwo)
		// {
		// 	randomWrinklePos_03 = new Vector2(RandomValueFromRanges(threeHorzRange_01, threeHorzRange_02, threeHorzRange_03), RandomValueFromRanges(threeVertRange_01, threeVertRange_02, threeVertRange_03));
		// }
		// else if (!enoughHorzSpaceOneTwo && enoughVertSpaceOneTwo)
		// {
		// 	randomWrinklePos_03 = new Vector2(RandomValueFromRanges(threeHorzRange_01, threeHorzRange_03), RandomValueFromRanges(threeVertRange_01, threeVertRange_02, threeVertRange_03));
		// }
		// else if (enoughHorzSpaceOneTwo && !enoughVertSpaceOneTwo)
		// {
		// 	randomWrinklePos_03 = new Vector2(RandomValueFromRanges(threeHorzRange_01, threeHorzRange_02,threeHorzRange_03), RandomValueFromRanges(threeVertRange_01, threeVertRange_03));
		// }
		// else if (!enoughHorzSpaceOneTwo && !enoughVertSpaceOneTwo)
		// {
		// 	randomWrinklePos_03 = new Vector2(RandomValueFromRanges(threeHorzRange_01, threeHorzRange_03), RandomValueFromRanges(threeVertRange_01, threeVertRange_03));
		// }



		wrinkle_01.localPosition = randomWrinklePos_01;
		wrinkle_02.localPosition = randomWrinklePos_02;
		wrinkle_03.localPosition = randomWrinklePos_03;
	}



	// public struct Range
	// {
	// 	public int min;
	// 	public int max;
	// 	public int range {get {return max-min + 1;}}
	// 	public Range(int aMin, int aMax)
	// 	{
	// 		min = aMin; max = aMax;
	// 	}
	// 	public Range(float aMin, float aMax)
	// 	{
	// 		min = (int)Mathf.Round(aMin); 
	// 		max = (int)Mathf.Round(aMax);
	// 	}
	// }
	
	// public static int RandomValueFromRanges(params Range[] ranges)
	// {
	// 	if (ranges.Length == 0)
	// 		return 0;
	// 	int count = 0;
	// 	foreach(Range r in ranges)
	// 		count += r.range;
	// 	int sel = Random.Range(0,count);
	// 	foreach(Range r in ranges)
	// 	{
	// 		if (sel < r.range)
	// 		{
	// 			return r.min + sel;
	// 		}
	// 		sel -= r.range;
	// 	}
	// 	throw new System.Exception("This should never happen");
	// }
}
