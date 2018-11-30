using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;
public class Card : MonoBehaviour {
    public int mID;//unique ID
    public int mNum;//a couple of card have the same number.
    public bool mNeedBack = false;
    public bool mIsCompleted=false;
    public GameObject mFront;//the front of the card
    public GameObject mBack;//the back of the card
    public Sprite mFrontSprite;
    public Sprite mBackSprite;
    public float mSpeed;//how long the card can display the front when we don't click anther card.
    public event EventHandler OnCompleted;
    private GameManager manager;
    
    private void Start()
    {
        manager = FindObjectOfType<GameManager>();
        mBack.GetComponent<Button>().onClick.AddListener(OnClick);
    }

    public void SetSprite()
    {
        mFront.GetComponent<Image>().sprite = mFrontSprite;
        mBack.GetComponent<Image>().sprite = mBackSprite;
    }

    /// <summary>
    /// Rotate the card to the front.
    /// </summary>
    /// <returns></returns>
    IEnumerator ToFront()
    {
        mBack.transform.DORotate(new Vector3(0, 90, 0), 0.3f);
        float open = 0.3f;
        while(open>0)
        {
            open -= Time.deltaTime;
            yield return 0;
        }
        mFront.transform.DORotate(new Vector3(0, 0, 0), 0.3f);
        open = 0.3f;
        while(open > 0)
        {
            open -= Time.deltaTime;
            yield return 0;
        }
        StartCoroutine(ToBack());
    }

    /// <summary>
    /// Rotate the card to the back.
    /// </summary>
    /// <returns></returns>
    IEnumerator ToBack()
    {
        float temp = mSpeed;
        while (temp > 0 && !mNeedBack)
        {
            temp -= Time.deltaTime;
            yield return 0;
        }
        if (!mIsCompleted)
        {
            manager.currentCard = null;
            StartCoroutine(SetButton());
            mFront.transform.DORotate(new Vector3(0, 90, 0), 0.3f);
            float close = 0.3f;
            while (close > 0)
            {
                close -= Time.deltaTime;
                yield return 0;
            }
            mBack.transform.DORotate(new Vector3(0, 0, 0), 0.3f);
            close = 0.3f;
            while (close > 0)
            {
                close -= Time.deltaTime;
                yield return 0;
            }
        }  
    }

    IEnumerator SetButton()
    {
        float temp = 0.6f;
        while (temp > 0)
        {
            temp -= Time.deltaTime;
            yield return 0;
        }
        mBack.GetComponent<Button>().enabled = true;
    }
    /// <summary>
    /// Call the method when click the card.
    /// Three cases maybe occur when we click the card
    /// </summary>
    public void OnClick()
    {
        mBack.GetComponent<Button>().enabled = false;
        StartCoroutine(ToFront());
        if(!manager.currentCard)
        {
            manager.currentCard = this;
        }
        else if(manager.currentCard && manager.currentCard.mNum==mNum)
        {
            mIsCompleted = true;
            manager.currentCard.mIsCompleted = true;
            manager.currentCard = null;
            if(OnCompleted!=null)
                OnCompleted(this, EventArgs.Empty);
        }
        else if(manager.currentCard && manager.currentCard.mNum!=mNum)
        {
            mNeedBack = true;
            manager.currentCard.mNeedBack = true;
        }
    }
   
}
