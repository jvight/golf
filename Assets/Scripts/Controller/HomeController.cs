using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class HomeController : MonoBehaviour
{
    public Image blackScreen;
    public GameObject SettingPopup;
    public TMP_Text textLevel;
    public GameObject ShopPopup;
    void Start()
    {
        UnityCore.Audio.AudioController.instance.PlayAudio(UnityCore.Audio.AudioType.BGM);
        FindObjectOfType<IronSourceAdsController>().ShowBanner();
        // progress.DOFillAmount(1f, 1f).OnComplete(() =>
        // {
        //     SceneManager.LoadScene("GameScene");
        // });
        textLevel.text = "Level " + (PlayerPrefs.GetInt("Level", 0) + 1).ToString();
    }

    public void OnClickRating()
    {
        SettingPopup.SetActive(false);
        IARManager.Instance.ShowBox();
    }

    public void OnClickSetting()
    {
        blackScreen.gameObject.SetActive(true);
        SettingPopup.SetActive(true);
        SettingPopup.transform.DOScale(1, 0.5f);
    }

    public void OnClickXSetting()
    {
        blackScreen.gameObject.SetActive(false);
        SettingPopup.transform.DOScale(0, 0.5f).OnComplete(() =>
        {
            SettingPopup.SetActive(false);
        });
    }

    public void ClickStart()
    {
        // UnityCore.Audio.AudioController.instance.PlayAudio(UnityCore.Audio.AudioType.BGM);
        SceneManager.LoadScene("LoadScene");
    }

    public void ClickShop()
    {
        ShopPopup.SetActive(true);
    }

    public void OnClickMenu()
    {
        ShopPopup.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
