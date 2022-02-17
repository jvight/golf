using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateJSON : MonoBehaviour
{
    // [SerializeField] private List<PotionData> _PotionData = new List<PotionData>();
    List<PotionData> __PotionData = new List<PotionData>();
    public Transform parentPos;

    public void SaveIntoJson()
    {
        Debug.Log(Application.persistentDataPath);
        string potion = "";
        for (int i = 0; i < parentPos.childCount; i++)
        {
            PotionData potionData = new PotionData();
            potionData.pos = parentPos.GetChild(i).transform.position.x.ToString() + "," + parentPos.GetChild(i).transform.position.y.ToString() + "," + parentPos.GetChild(i).transform.position.z.ToString();
            potionData.angle = parentPos.GetChild(i).transform.eulerAngles.x.ToString() + "," + parentPos.GetChild(i).transform.eulerAngles.y.ToString() + "," + parentPos.GetChild(i).transform.eulerAngles.z.ToString();

            if (i == 0)
            {
                potion += "[";
                potion += JsonUtility.ToJson(potionData) + ",";
            }
            else if (i == parentPos.childCount - 1)
            {
                potion += JsonUtility.ToJson(potionData) + "]";
            }
            else
            {
                potion += JsonUtility.ToJson(potionData) + ",";
            }
        }
        System.IO.File.WriteAllText(Application.persistentDataPath + "/PotionData.json", potion);
    }
}

[System.Serializable]
public class PotionData
{
    // public string potion_name;
    // public int value;
    // public List<Effect> effect = new List<Effect>();
    public string pos;
    public string angle;
}

// [System.Serializable]
// public class Effect
// {
//     public string name;
//     public string desc;
// }