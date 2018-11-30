using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class TimeArgs:EventArgs
{
    public float Time;
    public float curTime;
    public TimeArgs(float mTime,float mCurTime)
    {
        Time = mTime;
        curTime = mCurTime;
    }
}

public class Timer : MonoBehaviour {
    public event EventHandler OnTimeChange;
    public event EventHandler OnTimeOver;
    private float mTime = 30;
    private float curTime = 30;
    
    public void setTimer(float time)
    {
        mTime = time;
        curTime = time;
    }

    public void startTimer()
    {
        StartCoroutine(timer());
    }

    public void stopTimer()
    {
        StopCoroutine(timer());
    }

	IEnumerator timer()
    {
        while(curTime>0)
        {
            curTime -= Time.deltaTime;
            OnTimeChange(this, new TimeArgs(mTime,curTime));
            yield return 0;
        }
        curTime = 0;
        OnTimeChange(this, new TimeArgs(mTime, curTime));
        OnTimeOver(this, EventArgs.Empty);
    }
}
