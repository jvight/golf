using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;
using System;

public class UIController : MonoBehaviour
{
    public TMP_Text textEnd;
    public TMP_Text textAmountBall;
    public Image progressFlag;
    public Transform BaseFlag;
    public Image blackScreen;
    public GameObject SettingPopup;
    public TMP_Text scorePlusPrefab;

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
        textEnd.DOFade(0.7f, 1).OnComplete(() =>
        {
            textEnd.gameObject.SetActive(false);
        });
        textEnd.text = "DEFEATED";
    }
    public void AddScore()
    {
        StartCoroutine(DelayFunc(() =>
        {
            for (var i = 0; i < GameController.Instance.PlankParent.childCount; i++)
            {
                var rd = UnityEngine.Random.Range(1f, 2f);

                var scorePlus = Instantiate(scorePlusPrefab);
                scorePlus.transform.SetParent(GameController.Instance.ScorePlusParent);
                scorePlus.transform.position = GameController.Instance.PlankParent.GetChild(i).transform.position;
                var seq = DOTween.Sequence()
                .Append(scorePlus.transform.DOMoveY(GameController.Instance.PlankParent.GetChild(i).transform.position.y + 1.6f, rd))
                .Play();
                var seq2 = DOTween.Sequence()
                .Append(scorePlus.DOFade(1, 1f))
                .Append(scorePlus.DOFade(0, 1f))
                .Play();

            }
        }, 0.5f));


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
    IEnumerator DelayFunc(Action call, float time)
    {
        yield return new WaitForSeconds(time);
        call();
    }
}
