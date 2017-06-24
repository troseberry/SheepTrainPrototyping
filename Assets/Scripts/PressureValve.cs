using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PressureValve : MonoBehaviour
{
    public static PressureValve PressureValveReference;
    public static float timeLimit = 3.0f;

    public Slider currentPressureSlider;
    public Slider matchPressureSlider;

    int startingPressureAmount;
    int matchPressureAmount;

    bool isComplete;

    void Start()
    {
        PressureValveReference = this;


        //Pick a random value for starting pressure
        startingPressureAmount = 200;// (int)Mathf.Round(Random.Range(30f, 200f));
        //pick a lower random value for match pressure
        matchPressureAmount = (int)Mathf.Round(Random.Range(0f, (float)startingPressureAmount - 75f));

        currentPressureSlider.value = startingPressureAmount;
        matchPressureSlider.value = matchPressureAmount;
    }

    void Update()
    {
        currentPressureSlider.value += (RotateValve.rotationAmount / 25.0f);

        isComplete = (currentPressureSlider.value <= matchPressureSlider.value);

        if (isComplete || MiniGameManager.ManagerReference.timer == 0f) CloseGame();
    }

    void CloseGame()
    {
        MiniGameManager.ManagerReference.didWin = isComplete;
        MiniGameManager.ManagerReference.EndMiniGame();
        gameObject.SetActive(false);
    }

    public void ResetGame()
    {
        startingPressureAmount = 200;// (int)Mathf.Round(Random.Range(30f, 100f));
        matchPressureAmount = (int)Mathf.Round(Random.Range(0f, (float)startingPressureAmount - 30f));

        currentPressureSlider.value = startingPressureAmount;
        matchPressureSlider.value = matchPressureAmount;

        RotateValve.rotationAmount = 0f;
    }
}
