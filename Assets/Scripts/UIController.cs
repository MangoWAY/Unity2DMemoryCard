using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class UIController : MonoBehaviour {
    public Slider mTimeSlider;
    public Text mTimeLable;
    public Text mTime;
    public GameObject mResultPanel;
    public Text mResultText;

    public void SetEvent(Timer timer)
    {
        timer.OnTimeChange += updateTimeUI;
        FindObjectOfType<GameManager>().OnGameEnd += displaEndPanel;
    }

    void displaEndPanel(object sender, EventArgs e)
    {
        mResultPanel.SetActive(true);
        GameEndArgs args = e as GameEndArgs;
        switch(args.result)
        {
            case GameResult.gameover:
                mResultText.text = "Game Over!";
                break;
            case GameResult.victory:
                mResultText.text = "Victory!";
                break;
            default:
                break;
        }     
    }

    void  updateTimeUI(object sender,EventArgs e)
    {
        TimeArgs args = e as TimeArgs;
        mTime.text = ((int)args.curTime).ToString();
        mTimeSlider.value = args.curTime / args.Time;
    }
}
