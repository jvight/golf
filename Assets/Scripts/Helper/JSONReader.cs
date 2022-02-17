using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using System.Globalization;

public class JSONReader : MonoBehaviour
{
    public TextAsset textJSON;
    public Transform PlankParent;
    public TMP_Text text;
    [System.Serializable]
    public class Plank
    {
        public string pos;
        public string angle;
    }

    [System.Serializable]
    public class PlankList
    {
        public Plank[] plank;
    }
    public PlankList plankList = new PlankList();
    // Start is called before the first frame update
    void Start()
    {
        plankList = JsonUtility.FromJson<PlankList>(textJSON.text);
        for (int i = 0; i < plankList.plank.Length; i++)
        {
            // string[] strPos = plankList.plank[i].pos.Split(char.Parse(","));
            // Vector3 pos = new Vector3(float.Parse(strPos[0]), float.Parse(strPos[1]), float.Parse(strPos[2]));
            // PlankParent.GetChild(i).transform.position = pos;
            // string[] strAngle = plankList.plank[i].angle.Split(char.Parse(","));
            // Vector3 angle = new Vector3(float.Parse(strAngle[0]), float.Parse(strAngle[1]), float.Parse(strAngle[2]));
            // PlankParent.GetChild(i).transform.eulerAngles = angle;
        }
        // int i = 0;
        // StartCoroutine(DelayFunc(0.5f, i));
    }

    IEnumerator DelayFunc(float time, int i)
    {
        yield return new WaitForSeconds(time);
        if (i < plankList.plank.Length)
        {
            string[] strPos = plankList.plank[i].pos.Split(char.Parse(","));
            Vector3 pos = new Vector3(float.Parse(strPos[0], CultureInfo.InvariantCulture.NumberFormat), float.Parse(strPos[1], CultureInfo.InvariantCulture.NumberFormat), float.Parse(strPos[2], CultureInfo.InvariantCulture.NumberFormat));
            text.text = pos.ToString();
            PlankParent.GetChild(i).transform.position = pos;
            string[] strAngle = plankList.plank[i].angle.Split(char.Parse(","));
            Vector3 angle = new Vector3(float.Parse(strAngle[0]), float.Parse(strAngle[1]), float.Parse(strAngle[2]));
            PlankParent.GetChild(i).transform.eulerAngles = angle;
            i++;
            StartCoroutine(DelayFunc(0.5f, i));
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
