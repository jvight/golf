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
    public Transform ObjParent;
    public TMP_Text text;
    public GameObject prefabPlank;
    public GameObject prefabFlag;
    public List<GameObject> prefabObjs = new List<GameObject>();
    public List<Transform> listFlag = new List<Transform>();
    [System.Serializable]
    public class PlankData
    {
        public string pos;
        public string angle;
        public Color colorBase;
        public List<Color> color = new List<Color>();
        public bool isRed;
    }

    [System.Serializable]
    public class FlagData
    {
        public string pos;
        public string angle;
    }
    [System.Serializable]
    public class ObjectData
    {
        public int id;
        public string pos;
        public string angle;
        public string scale;
        public bool isMove;
        public string posEnd;
        public bool isX;

        public string xPos;
        public string xAngle;
        public string xScale;
        public bool isRotate;
        public string angleEnd;
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
        public ObjectData[] obj;
    }
    public ObjectList objectList = new ObjectList();
    // Start is called before the first frame update
    void Start()
    {
        Read();
    }

    public void Read()
    {
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
            Vector3 pos = new Vector3(float.Parse(strPos[0], CultureInfo.InvariantCulture.NumberFormat), float.Parse(strPos[1], CultureInfo.InvariantCulture.NumberFormat), float.Parse(strPos[2], CultureInfo.InvariantCulture.NumberFormat));
            PlankParent.GetChild(i).transform.position = pos;
            string[] strAngle = levelData.plank[i].angle.Split(char.Parse(","));
            Vector3 angle = new Vector3(float.Parse(strAngle[0], CultureInfo.InvariantCulture.NumberFormat), float.Parse(strAngle[1], CultureInfo.InvariantCulture.NumberFormat), float.Parse(strAngle[2], CultureInfo.InvariantCulture.NumberFormat));
            PlankParent.GetChild(i).transform.eulerAngles = angle;
            PlankParent.GetChild(i).GetComponent<Plank>().SetColor(levelData.plank[i].colorBase, levelData.plank[i].color);
            PlankParent.GetChild(i).GetComponent<Plank>().isRed = levelData.plank[i].isRed;
            PlankParent.GetChild(i).GetComponent<Plank>().ResetPlank();
        }
        for (int i = 0; i < levelData.flag.Length; i++)
        {
            if (i >= FlagParent.childCount)
            {
                GameObject flag = Instantiate(prefabFlag);
                flag.transform.parent = FlagParent;
            }
            FlagParent.GetChild(i).gameObject.SetActive(true);
            string[] strPos = levelData.flag[i].pos.Split(char.Parse(","));
            Vector3 pos = new Vector3(float.Parse(strPos[0], CultureInfo.InvariantCulture.NumberFormat), float.Parse(strPos[1], CultureInfo.InvariantCulture.NumberFormat), float.Parse(strPos[2], CultureInfo.InvariantCulture.NumberFormat));
            FlagParent.GetChild(i).transform.position = pos;
            string[] strAngle = levelData.flag[i].angle.Split(char.Parse(","));
            Vector3 angle = new Vector3(float.Parse(strAngle[0], CultureInfo.InvariantCulture.NumberFormat), float.Parse(strAngle[1], CultureInfo.InvariantCulture.NumberFormat), float.Parse(strAngle[2], CultureInfo.InvariantCulture.NumberFormat));
            FlagParent.GetChild(i).transform.eulerAngles = angle;
            FlagParent.GetChild(i).GetComponent<Flag>().ResetFlag();
        }
        for (int i = 0; i < levelData.obj.Length; i++)
        {
            GameObject obj = Instantiate(prefabObjs[levelData.obj[i].id]);
            obj.transform.parent = ObjParent;
            string[] strPos = levelData.obj[i].pos.Split(char.Parse(","));
            Vector3 pos = new Vector3(float.Parse(strPos[0], CultureInfo.InvariantCulture.NumberFormat), float.Parse(strPos[1], CultureInfo.InvariantCulture.NumberFormat), float.Parse(strPos[2], CultureInfo.InvariantCulture.NumberFormat));
            obj.transform.position = pos;
            string[] strAngle = levelData.obj[i].angle.Split(char.Parse(","));
            Vector3 angle = new Vector3(float.Parse(strAngle[0], CultureInfo.InvariantCulture.NumberFormat), float.Parse(strAngle[1], CultureInfo.InvariantCulture.NumberFormat), float.Parse(strAngle[2], CultureInfo.InvariantCulture.NumberFormat));
            obj.transform.eulerAngles = angle;
            string[] strScale = levelData.obj[i].scale.Split(char.Parse(","));
            Vector3 scale = new Vector3(float.Parse(strScale[0], CultureInfo.InvariantCulture.NumberFormat), float.Parse(strScale[1], CultureInfo.InvariantCulture.NumberFormat), float.Parse(strScale[2], CultureInfo.InvariantCulture.NumberFormat));
            obj.transform.localScale = scale;
            obj.GetComponent<ObjMap>().isMove = levelData.obj[i].isMove;
            obj.GetComponent<ObjMap>().isRotate = levelData.obj[i].isRotate;
            obj.GetComponent<ObjMap>().isX = levelData.obj[i].isX;
            if (obj.GetComponent<ObjMap>().isX)
            {
                obj.GetComponent<ObjMap>().xMark.gameObject.SetActive(true);
                string[] strPosX = levelData.obj[i].xPos.Split(char.Parse(","));
                Vector3 xPos = new Vector3(float.Parse(strPosX[0], CultureInfo.InvariantCulture.NumberFormat), float.Parse(strPosX[1], CultureInfo.InvariantCulture.NumberFormat), float.Parse(strPosX[2], CultureInfo.InvariantCulture.NumberFormat));
                obj.GetComponent<ObjMap>().xMark.position = xPos;
                string[] strAngleX = levelData.obj[i].xAngle.Split(char.Parse(","));
                Vector3 xAngle = new Vector3(float.Parse(strAngleX[0], CultureInfo.InvariantCulture.NumberFormat), float.Parse(strAngleX[1], CultureInfo.InvariantCulture.NumberFormat), float.Parse(strAngleX[2], CultureInfo.InvariantCulture.NumberFormat));
                obj.GetComponent<ObjMap>().xMark.eulerAngles = xAngle;
                string[] strScaleX = levelData.obj[i].xScale.Split(char.Parse(","));
                Vector3 xScale = new Vector3(float.Parse(strScaleX[0], CultureInfo.InvariantCulture.NumberFormat), float.Parse(strScaleX[1], CultureInfo.InvariantCulture.NumberFormat), float.Parse(strScaleX[2], CultureInfo.InvariantCulture.NumberFormat));
                obj.GetComponent<ObjMap>().xMark.localScale = xScale;
            }
            if (levelData.obj[i].isMove)
            {
                string[] strPosEnd = levelData.obj[i].posEnd.Split(char.Parse(","));
                Vector3 posEnd = new Vector3(float.Parse(strPosEnd[0], CultureInfo.InvariantCulture.NumberFormat), float.Parse(strPosEnd[1], CultureInfo.InvariantCulture.NumberFormat), float.Parse(strPosEnd[2], CultureInfo.InvariantCulture.NumberFormat));
                obj.GetComponent<ObjMap>().posEnd = posEnd;
                obj.GetComponent<ObjMap>().Move();
            }
            if (levelData.obj[i].isRotate)
            {
                string[] strAngleEnd = levelData.obj[i].angleEnd.Split(char.Parse(","));
                Vector3 angleEnd = new Vector3(float.Parse(strAngleEnd[0], CultureInfo.InvariantCulture.NumberFormat), float.Parse(strAngleEnd[1], CultureInfo.InvariantCulture.NumberFormat), float.Parse(strAngleEnd[2], CultureInfo.InvariantCulture.NumberFormat));
                obj.GetComponent<ObjMap>().angleEnd = angleEnd;
                obj.GetComponent<ObjMap>().Rotate();
            }
        }
        GameController.Instance.CreateDone();
    }
    // Update is called once per frame
    void Update()
    {

    }
}
