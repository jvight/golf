using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateJSON : MonoBehaviour
{
    // [SerializeField] private List<PotionData> _PotionData = new List<PotionData>();
    List<PotionData> __PotionData = new List<PotionData>();
    public Transform PlankParent;
    public Transform FlagParent;
    public Transform ObjectParent;

    public void SaveIntoJson()
    {
        Debug.Log(Application.persistentDataPath);
        string potion = "";
        for (int i = 0; i < PlankParent.childCount; i++)
        {
            PotionData potionData = new PotionData();
            potionData.pos = PlankParent.GetChild(i).transform.position.x.ToString() + "," + PlankParent.GetChild(i).transform.position.y.ToString() + "," + PlankParent.GetChild(i).transform.position.z.ToString();
            potionData.angle = PlankParent.GetChild(i).transform.eulerAngles.x.ToString() + "," + PlankParent.GetChild(i).transform.eulerAngles.y.ToString() + "," + PlankParent.GetChild(i).transform.eulerAngles.z.ToString();
            potionData.colorBase = PlankParent.GetChild(i).GetComponent<Plank>().colorBase;
            potionData.color = PlankParent.GetChild(i).GetComponent<Plank>().listColorChange;
            potionData.isRed = PlankParent.GetChild(i).GetComponent<Plank>().isRed;
            if (i == 0)
            {
                potion += "\"plank\": [";
                potion += JsonUtility.ToJson(potionData) + ",";
            }
            else if (i == PlankParent.childCount - 1)
            {
                potion += JsonUtility.ToJson(potionData) + "],";
            }
            else
            {
                potion += JsonUtility.ToJson(potionData) + ",";
            }
        }
        for (int i = 0; i < FlagParent.childCount; i++)
        {
            FlagData flag = new FlagData();
            flag.pos = FlagParent.GetChild(i).transform.position.x.ToString() + "," + FlagParent.GetChild(i).transform.position.y.ToString() + "," + FlagParent.GetChild(i).transform.position.z.ToString();
            flag.angle = FlagParent.GetChild(i).transform.eulerAngles.x.ToString() + "," + FlagParent.GetChild(i).transform.eulerAngles.y.ToString() + "," + FlagParent.GetChild(i).transform.eulerAngles.z.ToString();
            if (i == 0)
            {
                potion += "\n \"flag\": [";
                potion += JsonUtility.ToJson(flag);
                if (FlagParent.childCount == 1)
                {
                    potion += "]";
                }
                else
                {
                    potion += ",";
                }
            }
            else if (i == FlagParent.childCount - 1)
            {
                potion += JsonUtility.ToJson(flag) + "],";
            }
            else
            {
                potion += JsonUtility.ToJson(flag) + ",";
            }
        }
        if (ObjectParent.childCount == 0)
        {
              potion += "\n \"obj\": []";
        }
        else
        {
            for (int i = 0; i < ObjectParent.childCount; i++)
            {
                ObjData obj = new ObjData();
                obj.pos = ObjectParent.GetChild(i).transform.position.x.ToString() + "," + ObjectParent.GetChild(i).transform.position.y.ToString() + "," + ObjectParent.GetChild(i).transform.position.z.ToString();
                obj.angle = ObjectParent.GetChild(i).transform.eulerAngles.x.ToString() + "," + ObjectParent.GetChild(i).transform.eulerAngles.y.ToString() + "," + ObjectParent.GetChild(i).transform.eulerAngles.z.ToString();
                obj.scale = ObjectParent.GetChild(i).transform.localScale.x.ToString() + "," + ObjectParent.GetChild(i).transform.localScale.y.ToString() + "," + ObjectParent.GetChild(i).transform.localScale.z.ToString();
                obj.id = ObjectParent.GetChild(i).GetComponent<ObjMap>().id;
                obj.isMove = ObjectParent.GetChild(i).GetComponent<ObjMap>().isMove;
                obj.isRotate = ObjectParent.GetChild(i).GetComponent<ObjMap>().isRotate;
                obj.posEnd = ObjectParent.GetChild(i).GetComponent<ObjMap>().posEnd.x.ToString() + "," + ObjectParent.GetChild(i).GetComponent<ObjMap>().posEnd.y.ToString() + "," + ObjectParent.GetChild(i).GetComponent<ObjMap>().posEnd.z.ToString();
                obj.angleEnd = ObjectParent.GetChild(i).GetComponent<ObjMap>().angleEnd.x.ToString() + "," + ObjectParent.GetChild(i).GetComponent<ObjMap>().angleEnd.y.ToString() + "," + ObjectParent.GetChild(i).GetComponent<ObjMap>().angleEnd.z.ToString();
                if(ObjectParent.GetChild(i).GetComponent<ObjMap>().isX){
                    obj.isX=true;
                    obj.xPos=ObjectParent.GetChild(i).GetComponent<ObjMap>().xMark.position.x.ToString() + "," + ObjectParent.GetChild(i).GetComponent<ObjMap>().xMark.position.y.ToString() + "," + ObjectParent.GetChild(i).GetComponent<ObjMap>().xMark.position.z.ToString();
                    obj.xAngle=ObjectParent.GetChild(i).GetComponent<ObjMap>().xMark.eulerAngles.x.ToString() + "," + ObjectParent.GetChild(i).GetComponent<ObjMap>().xMark.eulerAngles.y.ToString() + "," + ObjectParent.GetChild(i).GetComponent<ObjMap>().xMark.eulerAngles.z.ToString();
                    obj.xScale=ObjectParent.GetChild(i).GetComponent<ObjMap>().xMark.localScale.x.ToString() + "," + ObjectParent.GetChild(i).GetComponent<ObjMap>().xMark.localScale.y.ToString() + "," + ObjectParent.GetChild(i).GetComponent<ObjMap>().xMark.localScale.z.ToString();
                }
                if (i == 0)
                {
                    potion += "\n \"obj\": [";
                    potion += JsonUtility.ToJson(obj);
                    if (ObjectParent.childCount == 1)
                    {
                        potion += "]";
                    }
                    else
                    {
                        potion += ",";
                    }
                }
                else if (i == ObjectParent.childCount - 1)
                {
                    potion += JsonUtility.ToJson(obj) + "]";
                }
                else
                {
                    potion += JsonUtility.ToJson(obj) + ",";
                }
            }
        }
        System.IO.File.WriteAllText(Application.persistentDataPath + "/PotionData.json", potion);
        Debug.Log(potion);
    }
}

[System.Serializable]
public class PotionData
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
public class ObjData
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