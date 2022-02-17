using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameAnalyticsSDK;

public class GameController : MonoBehaviour
{
    public static GameController Instance;
    void Awake()
    {
        // Instance = this;
        // GameAnalytics.Initialize();
    }
    void Start()
    {
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
