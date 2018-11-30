using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[System.Serializable]
public struct Sliderbar
{
    public string mLabel;
    public Text mText;
    public Slider mSlider;
}

public class SliderController : MonoBehaviour {
    public Sliderbar mNum;
    public Sliderbar mTime;
    public Sliderbar mSpeed;

    void Init()
    {
        mNum.mText.text = mNum.mLabel + " " + Settings.CardNum * 2;
        mNum.mSlider.value = Settings.CardNum;
        mTime.mText.text = mTime.mLabel + " " + Settings.GameTime;
        mTime.mSlider.value = Settings.GameTime;
        mSpeed.mText.text = mSpeed.mLabel + " " + Settings.RotateSpeed.ToString("f2");
        mSpeed.mSlider.value = Settings.RotateSpeed;
    }

    void Start () {
        Init();
        mNum.mSlider.onValueChanged.AddListener(delegate {
            SetSetting();
        });
        mTime.mSlider.onValueChanged.AddListener(delegate {
            SetSetting();
        });
        mSpeed.mSlider.onValueChanged.AddListener(delegate {
            SetSetting();
        });
    }
	
	void SetSetting()
    {
        mNum.mText.text = mNum.mLabel + " " + mNum.mSlider.value * 2;
        mTime.mText.text = mTime.mLabel + " " + (int)mTime.mSlider.value;
        mSpeed.mText.text = mSpeed.mLabel + " " + mSpeed.mSlider.value.ToString("f2");
        Settings.CardNum = (int)(mNum.mSlider.value);
        Settings.GameTime = (int)mTime.mSlider.value;
        Settings.RotateSpeed = mSpeed.mSlider.value;
    }
}
