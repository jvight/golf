using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoogleAds : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        AppOpenAdManager.Instance.LoadAd();
    }

    public void OnApplicationPause(bool paused)
    {
        // Display the app open ad when the app is foregrounded
        if (!paused)
        {
            AppOpenAdManager.Instance.ShowAdIfAvailable();
        }
    }
}
