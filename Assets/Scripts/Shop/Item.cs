using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    [HideInInspector]
    public int ID = 0;
    [HideInInspector]
    public string Name;
    public TMP_Text textName;
    public TMP_Text textBtn;
    // Start is called before the first frame update
    void Start()
    {

    }

    public void SetID(int id)
    {
        this.ID = id;
    }

    public void ClickChoose()
    {
        this.GetComponent<Image>().enabled = true;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
