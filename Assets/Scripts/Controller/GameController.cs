using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameAnalyticsSDK;
using System;

public class GameController : MonoBehaviour
{
    public static GameController Instance;
    public Transform PlankParent;
    public Transform FlagParent;
    public Transform ObjParent;
    public List<Plank> listPlank = new List<Plank>();
    void Awake()
    {
        Instance = this;
        // GameAnalytics.Initialize();
    }
    void Start()
    {
        Time.timeScale = 1;
        for (int i = 0; i < PlankParent.childCount; i++)
        {
            listPlank.Add(PlankParent.GetChild(i).GetComponent<Plank>());
        }
        Plank plankRed = listPlank.Find(plank => plank.tag == "Domino");
        Debug.Log(plankRed);
    }

    public void CheckEnd()
    {
        StopAllCoroutines();
        StartCoroutine(DelayFunc(() =>
        {
            Plank plankRed = listPlank.Find(plank => plank.name == "Domino");
            Debug.Log(plankRed);
        }, 2f));
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
