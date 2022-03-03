using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

public class HomeController : MonoBehaviour
{
    public Image progress;
    public Image blackScreen;
    public GameObject SettingPopup;
    void Start()
    {
        FindObjectOfType<IronSourceAdsController>().ShowBanner();
        // progress.DOFillAmount(1f, 1f).OnComplete(() =>
        // {
        //     SceneManager.LoadScene("GameScene");
        // });
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

    // Update is called once per frame
    void Update()
    {

    }
}
