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
    public Transform FlagParent;
    public TMP_Text text;
    public GameObject prefabPlank;
    public List<Transform> listFlag = new List<Transform>();
    [System.Serializable]
    public class PlankData
    {
        public string pos;
        public string angle;
        public Color colorBase;
        public List<Color> color = new List<Color>();
    }

    [System.Serializable]
    public class FlagData
    {
        public string pos;
        public string angle;
    }

    [System.Serializable]
    public class ObjectList
    {
        public Level[] level;
    }
    [System.Serializable]
    public class Level
    {
        public PlankData[] plank;
        public FlagData[] flag;
    }
    public ObjectList objectList = new ObjectList();
    // Start is called before the first frame update
    void Start()
    {
        Read();
    }
    
    void Read() {
        objectList = JsonUtility.FromJson<ObjectList>(textJSON.text);
        Level levelData = objectList.level[StaticData.level];
        for (int i = 0; i < levelData.plank.Length; i++)
        {
            if (i >= PlankParent.childCount)
            {
                GameObject plank = Instantiate(prefabPlank);
                plank.transform.parent = PlankParent;
            }
            string[] strPos = levelData.plank[i].pos.Split(char.Parse(","));
            Vector3 pos = new Vector3(float.Parse(strPos[0], CultureInfo.InvariantCulture), float.Parse(strPos[1], CultureInfo.InvariantCulture), float.Parse(strPos[2], CultureInfo.InvariantCulture));
            PlankParent.GetChild(i).transform.position = pos;
            string[] strAngle = levelData.plank[i].angle.Split(char.Parse(","));
            Vector3 angle = new Vector3(float.Parse(strAngle[0], CultureInfo.InvariantCulture), float.Parse(strAngle[1], CultureInfo.InvariantCulture), float.Parse(strAngle[2], CultureInfo.InvariantCulture));
            PlankParent.GetChild(i).transform.eulerAngles = angle;
            // PlankParent.GetChild(i).GetComponent<Plank>().colorBase = new Color(levelData.plank[i].colorBase.r, levelData.plank[i].colorBase.g, levelData.plank[i].colorBase.b, levelData.plank[i].colorBase.a);
            PlankParent.GetChild(i).GetComponent<Plank>().SetColor(levelData.plank[i].colorBase, levelData.plank[i].color);
        }
        for (int i = 0; i < levelData.flag.Length; i++)
        {
            FlagParent.GetChild(i).gameObject.SetActive(true);
            string[] strPos = levelData.flag[i].pos.Split(char.Parse(","));
            Vector3 pos = new Vector3(float.Parse(strPos[0], CultureInfo.InvariantCulture), float.Parse(strPos[1], CultureInfo.InvariantCulture), float.Parse(strPos[2], CultureInfo.InvariantCulture));
            FlagParent.GetChild(i).transform.position = pos;
            string[] strAngle = levelData.flag[i].angle.Split(char.Parse(","));
            Vector3 angle = new Vector3(float.Parse(strAngle[0], CultureInfo.InvariantCulture), float.Parse(strAngle[1], CultureInfo.InvariantCulture), float.Parse(strAngle[2], CultureInfo.InvariantCulture));
            FlagParent.GetChild(i).transform.eulerAngles = angle;
        }
    }
    // Update is called once per frame
    void Update()
    {

    }
}
