using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RateBox : MonoBehaviour
{
    public Transform baseStar;
    public Sprite sprStarOn;
    public Sprite sprStarOff;
    int numStar = 0;
    // Start is called before the first frame update
    public void OnClickLater()
    {
        if (IARManager.Instance.showRate)
        {
            gameObject.SetActive(false);
            int numOff = PlayerPrefs.GetInt("RateOff", 0);
            PlayerPrefs.SetInt("RateOff", numOff + 1);
            IARManager.Instance.showRate = false;
            GameController.Instance.uiController.blackScreen.gameObject.SetActive(false);
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
            Debug.Log("Not Dien thoai");
        }
        else
        {
            SendEmail();
        }
        gameObject.SetActive(false);
        GameController.Instance.uiController.blackScreen.gameObject.SetActive(false);
    }

    void SendEmail()
    {
        string email = "feedback@cscmobi.com";
        string subject = MyEscapeURL("My Subject");
        string body = MyEscapeURL("My Body\r\nFull of non-escaped chars");
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

