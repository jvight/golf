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
    void Start()
    {
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
        DOTween.Kill(fillTween);
        fillTween = progressFlag.DOFillAmount(amount * 0.2f, 1f);
    }

    public void GameWinEvent()
    {
        blackScreen.gameObject.SetActive(true);
        blackScreen.DOFade(0.7f, 1);
        textEnd.DOFade(0.7f, 1);
        textEnd.text = "VICTORY";
    }

    public void GameLoseEvent()
    {
        blackScreen.gameObject.SetActive(true);
        blackScreen.DOFade(0.7f, 1);
        textEnd.DOFade(0.7f, 1);
        textEnd.text = "DEFEATED";
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
