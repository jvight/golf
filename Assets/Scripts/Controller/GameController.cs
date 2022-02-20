using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameAnalyticsSDK;
using System;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController Instance;
    public UIController uiController;
    public Character character;
    public Transform PlankParent;
    public Transform FlagParent;
    public Transform ObjParent;
    List<Plank> listPlank = new List<Plank>();
    List<Flag> listFlag = new List<Flag>();
    public Golf golf;
    public int AmountBall = 4;
    public bool GameDone = false;
    void Awake()
    {
        Instance = this;
        // GameAnalytics.Initialize();
    }
    void Start()
    {
        uiController.SetAmountBall(AmountBall);
        Time.timeScale = 1;
        // Plank plankRed = listPlank.Find(plank => plank.tag == "Plank");
        // Debug.Log(list.Length);
    }

    public void CreateDone()
    {
        for (int i = 0; i < PlankParent.childCount; i++)
        {
            listPlank.Add(PlankParent.GetChild(i).GetComponent<Plank>());
        }
        for (int i = 0; i < FlagParent.childCount; i++)
        {
            listFlag.Add(FlagParent.GetChild(i).GetComponent<Flag>());
        }
        uiController.UpdateAmountFlag(listFlag.Count);
    }

    public void PlayGolf()
    {
        AmountBall--;
        uiController.SetAmountBall(AmountBall);
    }

    public void PourDone()
    {
        StopAllCoroutines();
        StartCoroutine(DelayFunc(() =>
        {   
            character.Idle();
            Time.timeScale = 1;
            golf.ReBack();
            CheckEnd();
        }, 1f));
    }

    public void CheckEnd()
    {
        Plank plankRed = listPlank.Find(plank => plank.isRed && plank.poured);
        if (plankRed != null)
        {
            GameLose();
            return;
        }
        List<Plank> whitePlank = new List<Plank>();
        listPlank.ForEach(plank =>
        {
            if (!plank.isRed)
            {
                whitePlank.Add(plank);
            }
        });
        bool whiteDone = whitePlank.TrueForAll(plank => !plank.isRed && plank.poured);
        bool flagDone = listFlag.TrueForAll(flag => flag.isFly);
        if (whiteDone && flagDone)
        {
            GameWin();
        }
        else
        {
            if (AmountBall <= 0)
            {
                GameLose();
            }
        }
        Debug.Log(flagDone);
        Debug.Log(whiteDone);
    }

    public void UpdateFlagFly()
    {
        int flagFly = 0;
        listFlag.ForEach(flag =>
        {
            if (flag.isFly)
            {
                flagFly++;
            }
        });
        uiController.UpdateFlagFly(flagFly);
    }

    public void GameLose()
    {
        StartCoroutine(DelayFunc(() =>
        {
            uiController.GameLoseEvent();
        }, 0.5f));
        GameDone = true;
    }

    public void GameWin()
    {
        StartCoroutine(DelayFunc(() =>
        {
            uiController.GameWinEvent();
            StartCoroutine(DelayFunc(() =>
            {
                StaticData.level += 1;
                if (StaticData.level >= 5)
                {
                    StaticData.level = 0;
                }
                SceneManager.LoadScene("GameScene");
            }, 1f));
        }, 1f));
        GameDone = true;
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator DelayFunc(Action call, float time)
    {
        yield return new WaitForSeconds(time);
        call();
    }
}
