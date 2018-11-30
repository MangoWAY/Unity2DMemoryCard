using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Text;
using System;

public enum GameResult
{
    gameover,
    victory
}

public class GameEndArgs:EventArgs
{
    public GameResult result;
    public GameEndArgs(GameResult r){result = r;}
}

public class GameManager : MonoBehaviour {
    public CardStyle cardStyle;
    public Timer mTimer;
    public event EventHandler OnGameEnd;
    public UIController uiController;
    public Card currentCard;
    public GameObject cardPrefab;
    public Transform cardContent;
    public int cardNum = 8;
    private Card[] cards;
    private List<int> randomList;
    private float gameTime;
    private float rotateSpeed;
    private int completedCard=0;

    /// <summary>
    /// Generate some random figures based on cardNum and add those to list.
    /// The list doesn't contain duplicate figures.
    /// </summary>
    void GenerateRandom()
    {
        int tempNum = 0;
        while(randomList.Count!= cardNum)
        {
            tempNum = UnityEngine.Random.Range(0, cardNum);
            if (!randomList.Contains(tempNum))
                randomList.Add(tempNum);
        }
    }
    /// <summary>
    /// Initialize some data
    /// </summary>
    void Init()
    {
        randomList = new List<int>();
        //Read the setting file.
        gameTime = Settings.GameTime;
        rotateSpeed = Settings.RotateSpeed;
        cardNum = Settings.CardNum*2;
        //Instantiate cards.
        for(int i=0;i<cardNum; i++)
        {
            var go = Instantiate(cardPrefab);
            go.transform.SetParent(cardContent);
        }
        //Set cards
        cards = GameObject.FindObjectsOfType<Card>();
        if (cards!=null)
        {
            for(int i=0;i< cardNum; i++)
            {
                cards[i].mID = i;
                cards[i].mSpeed = rotateSpeed;
                cards[i].OnCompleted += Victory;
            }
        }
        GenerateRandom();
        for(int i=0;i<randomList.Count;i++)
        {
            cards[randomList[i]].mNum = i / 2;
        }
        cardStyle.initCardStyle(cards);
        mTimer.OnTimeOver += GameOver;
        uiController.SetEvent(mTimer);
    }
    private void Awake()
    {
        Init();  
    }
    void Start()
    {
        mTimer.setTimer(gameTime);
        mTimer.startTimer();
    }
    void GameOver(object sender,EventArgs e)
    {
        if (OnGameEnd != null)
            OnGameEnd(this, new GameEndArgs(GameResult.gameover));
    }

    void Victory(object sender,EventArgs e)
    {
        if((++completedCard== cardNum / 2)&&(OnGameEnd != null))
        {
            mTimer.stopTimer();
            OnGameEnd(this, new GameEndArgs(GameResult.victory));
        }          
    }
}
