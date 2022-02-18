using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateJSON : MonoBehaviour
{
    // [SerializeField] private List<PotionData> _PotionData = new List<PotionData>();
    List<PotionData> __PotionData = new List<PotionData>();
    public Transform PlankParent;
    public Transform FlagParent;

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
            // if (!firtFlag)
            // {
            //     potion += "\n \"flag\": [";
            //     firtFlag = true;
            // }
            FlagData flag = new FlagData();
            flag.pos = FlagParent.GetChild(i).transform.position.x.ToString() + "," + FlagParent.GetChild(i).transform.position.y.ToString() + "," + FlagParent.GetChild(i).transform.position.z.ToString();
            flag.angle = FlagParent.GetChild(i).transform.eulerAngles.x.ToString() + "," + FlagParent.GetChild(i).transform.eulerAngles.y.ToString() + "," + FlagParent.GetChild(i).transform.eulerAngles.z.ToString();
            if (i == 0)
            {
                potion += "\n \"flag\": [";
                potion += JsonUtility.ToJson(flag) + ",";
            }
            else if (i == FlagParent.childCount - 1)
            {
                potion += JsonUtility.ToJson(flag) + "]";
            }
            else
            {
                potion += JsonUtility.ToJson(flag) + ",";
            }
        }
        System.IO.File.WriteAllText(Application.persistentDataPath + "/PotionData.json", potion);
    }
}

[System.Serializable]
public class PotionData
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

// [System.Serializable]
// public class Effect
// {
//     public string name;
//     public string desc;
// }