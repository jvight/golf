using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
    public Transform ScorePlusParent;
    List<Plank> listPlank = new List<Plank>();
    List<Flag> listFlag = new List<Flag>();
    public Golf golf;
    public int AmountBall = 4;
    public bool GameDone = false;
    public JSONReader jsonReader;
    int coin = 0;
    void Awake()
    {
        Instance = this;
        // GameAnalytics.Initialize();
    }
    void Start()
    {   
        coin = PlayerPrefs.GetInt("Coin", 0);
        int numOff = PlayerPrefs.GetInt("RateOff", 0);
        Debug.Log(numOff);
        if (StaticData.level == 3 && numOff == 0 || StaticData.level == 10 && numOff == 1 || StaticData.level == 15 && numOff == 2)
        {
            IARManager.Instance.ShowBox();
            uiController.blackScreen.gameObject.SetActive(true);
        }
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
        // uiController.UpdateAmountFlag(listFlag.Count);
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
            // Time.timeScale = 1;
            ChangeTime(1);
            if (AmountBall > 0)
            {
                golf.ReBack();
                character.Veldle();
            }
            CheckEnd();
        }, 2f));
    }

    public void ChangeTime(float time)
    {
        Time.timeScale = time;
        for (int i = 0; i < ObjParent.childCount; i++)
        {
            ObjParent.GetChild(i).GetComponent<ObjMap>().Change();
        }
    }

    public void CheckEnd()
    {
        Plank plankRed = listPlank.Find(plank => plank.isRed && plank.poured);
        if (plankRed != null)
        {
            // GameLose();
            StartCoroutine(DelayFunc(() =>
           {
               if (AmountBall > 0)
               {
                   jsonReader.Read();

               }
               else
               {
                   GameLose();
               }
           }, 0.5f));

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
        Debug.Log(whiteDone);
        Debug.Log(flagDone);
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
        // uiController.UpdateFlagFly(flagFly);
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
        uiController.AddScore();
        StartCoroutine(DelayFunc(() =>
        {
            uiController.GameWinEvent();
            StartCoroutine(DelayFunc(() =>
            {
                StaticData.level += 1;
                if (StaticData.level >= 30)
                {
                    StaticData.level = 0;
                }
                PlayerPrefs.SetInt("Level", StaticData.level);
                SceneManager.LoadScene("GameScene");
            }, 2f));
        }, 2f));
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
