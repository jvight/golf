using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using GameAnalyticsSDK;

public class UIController : MonoBehaviour
{
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ClickRelay()
    {
        SceneManager.LoadScene("GameScene");
    }
}
