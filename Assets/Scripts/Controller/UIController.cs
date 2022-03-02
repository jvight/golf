using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;

public class UIController : MonoBehaviour
{
    public TMP_Text textEnd;
    public TMP_Text textAmountBall;
    public Image progressFlag;
    public Transform BaseFlag;
    public Image blackScreen;
    public GameObject SettingPopup;
    void Start()
    {
        UpdateFlagFly(StaticData.level);
    }

    public void SetAmountBall(int amount)
    {
        textAmountBall.text = amount.ToString() + "/4";
    }

    public void UpdateAmountFlag(int amount)
    {
        for (int i = 0; i < BaseFlag.childCount; i++)
        {
            if (i < amount)
            {
                BaseFlag.GetChild(i).gameObject.SetActive(true);
            }
        }
    }
    Tween fillTween;
    public void UpdateFlagFly(int amount)
    {
        UpdateAmountFlag(amount);
        DOTween.Kill(fillTween);
        fillTween = progressFlag.DOFillAmount(amount * 0.2f, 1f);
    }

    public void GameWinEvent()
    {
        blackScreen.gameObject.SetActive(true);
        blackScreen.DOFade(0.7f, 1);
        textEnd.gameObject.SetActive(true);
        textEnd.DOFade(0.7f, 1);
        textEnd.text = "VICTORY";
    }

    public void GameLoseEvent()
    {
        blackScreen.gameObject.SetActive(true);
        blackScreen.DOFade(0.7f, 1);
        textEnd.DOFade(0.7f, 1).OnComplete(() => {
             textEnd.gameObject.SetActive(false);
        });
        textEnd.text = "DEFEATED";
    }

    public void OnClickSetting()
    {
        blackScreen.gameObject.SetActive(true);
        SettingPopup.SetActive(true);
        SettingPopup.transform.DOScale(1, 1);
    }

    public void OnClickXSetting()
    {
        blackScreen.gameObject.SetActive(false);
        SettingPopup.transform.DOScale(0, 1).OnComplete(() =>
        {
            SettingPopup.SetActive(false);
        });
    }

    public void OnClickRating()
    {
        SettingPopup.SetActive(false);
        IARManager.Instance.ShowBox();
    }
    // Update is called once per frame
    void Update()
    {

    }

    public void ClickRelay()
    {
        SceneManager.LoadScene("GameScene");
        // GameController.Instance.ChangeTime(1f);
    }
}
