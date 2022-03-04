using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RateBox : MonoBehaviour
{
    public Transform baseStar;
    public Sprite sprStarOn;
    public Sprite sprStarOff;
    public GameObject blackScreen;
    public int numStar = 0;
    // Start is called before the first frame update
    public void OnClickLater()
    {
        if (IARManager.Instance.showRate)
        {
            gameObject.SetActive(false);
            int numOff = PlayerPrefs.GetInt("RateOff", 0);
            if (StaticData.level == 3 && numOff == 0 || StaticData.level == 10 && numOff == 1 || StaticData.level == 15 && numOff == 2)
            {
                PlayerPrefs.SetInt("RateOff", numOff + 1);
            }
            IARManager.Instance.showRate = false;
            blackScreen.SetActive(false);
        }
    }

    public void OnClickRate()
    {
        if (this.numStar == 5)
        {
#if UNITY_ANDROID
            Application.OpenURL("market://details?id=com.mag.offline.fps.shooter.missiongame&hl=vi&gl=US");
            Debug.Log("Android");
#elif UNITY_IOS
                Application.OpenURL("itms-apps://itunes.apple.com/app/id12345678");
                Debug.Log("Iphone");
#endif
        }
        else
        {
            SendEmail();
        }
        int numOff = PlayerPrefs.GetInt("RateOff", 0);
        if (StaticData.level == 3 && numOff == 0 || StaticData.level == 10 && numOff == 1 || StaticData.level == 15 && numOff == 2)
        {
            PlayerPrefs.SetInt("RateOff", numOff + 1);
        }
        gameObject.SetActive(false);
        blackScreen.SetActive(false);
    }

    void SendEmail()
    {
        string email = "feedback@cscmobi.com";
        string subject = MyEscapeURL("Feedback");
        string body = MyEscapeURL("Your feedback");
        Application.OpenURL("mailto:" + email + "?subject=" + subject + "&body=" + body);
    }
    string MyEscapeURL(string url)
    {
        return WWW.EscapeURL(url).Replace("+", "%20");
    }
    public void ChooseStar(int star)
    {
        this.numStar = star;
        for (int i = 0; i < baseStar.childCount; i++)
        {
            if (i == star - 1)
            {
                baseStar.GetChild(i).GetComponent<Image>().sprite = sprStarOn;
            }
            else
            {
                baseStar.GetChild(i).GetComponent<Image>().sprite = sprStarOff;
            }
        }
    }
}


