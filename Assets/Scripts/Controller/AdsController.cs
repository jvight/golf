using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdsController : MonoBehaviour
{
    private static AdsController instance;
    public static AdsController Instance { get { return instance; } }
    private List<IAdsInterface> adsController;
    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            // for (int i = 0; i < transform.childCount; i++)
            //     if (transform.GetChild(i).GetComponent<IAdsInterface>() != null)
            //         adsController.Add(transform.GetChild(i).GetComponent<IAdsInterface>());
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}


