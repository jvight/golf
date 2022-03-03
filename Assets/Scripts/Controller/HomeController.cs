using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

public class HomeController : MonoBehaviour
{
    public Image progress;
    void Awake()
    {
    }
    void Start()
    {
        FindObjectOfType<IronSourceAdsController>().ShowBanner();
        progress.DOFillAmount(1f, 1f).OnComplete(() =>
        {
            SceneManager.LoadScene("GameScene");
        });
    }

    // Update is called once per frame
    void Update()
    {

    }
}
