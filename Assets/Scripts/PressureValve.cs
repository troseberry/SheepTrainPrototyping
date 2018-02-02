using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PressureValve : MiniGameScript
{
    public static PressureValve PressureValveReference;
    public static float timeLimit = 3.0f;

    public Slider currentPressureSlider;
    public Slider matchPressureSlider;

    int startingPressureAmount;
    int matchPressureAmount;

    bool isComplete = false;
    bool doStartGame = true;

    void Start()
    {
        PressureValveReference = this;
    }

    void Update()
    {

        if (!isComplete && PlayerMiniGameHandler.IsInGame() && doStartGame)
        {
            RandomizePressure();
            doStartGame = false;
        }

        currentPressureSlider.value += (RotateValve.rotationAmount / 25.0f);

        if (PlayerMiniGameHandler.timer > 0) isComplete = (currentPressureSlider.value <= matchPressureSlider.value);

        if (isComplete || PlayerMiniGameHandler.timer == 0f) EndGame();
    }

    void RandomizePressure()
    {
        startingPressureAmount = 200;
        matchPressureAmount = (int)Mathf.Round(Random.Range(0f, (float)startingPressureAmount - 75f));

        currentPressureSlider.value = startingPressureAmount;
        matchPressureSlider.value = matchPressureAmount;

        RotateValve.rotationAmount = 0f;
    }

    void EndGame()
    {
        PlayerMiniGameHandler.EndMiniGame(isComplete);
    }

    public override void ResetGame()
    {
        doStartGame = true;
        isComplete = false;

        RandomizePressure();
    }

    public override float GetTimeLimit() { return timeLimit; }
}
