using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IARManager : MonoBehaviour
{
    public RateBox rateBox;
    public static IARManager Instance;
    public bool showRate = false;
    // Start is called before the first frame update
    void Awake() {
        if (Instance == null)
        {
            Instance = this;
            // DontDestroyOnLoad(gameObject);
        }
    }
    void Start()
    {
    }

    public void ShowBox()
    {
        rateBox.numStar = 0;
        rateBox.gameObject.SetActive(true);
        showRate = true;
    }

    private void OnApplicationQuit()
    {
        if (!showRate) { return; }
        int numOff = PlayerPrefs.GetInt("RateOff", 0);
        PlayerPrefs.SetInt("RateOff", numOff + 1);
    }
}


