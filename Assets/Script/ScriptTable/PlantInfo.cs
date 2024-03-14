using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "PlantInfo", menuName = "Plant/PlantInfo")]
public class PlantInfo : ScriptableObject
{
    public List<PlantInfoItem> PlantDataList = new List<PlantInfoItem>();
}

[Serializable]
public class PlantInfoItem
{
    public int plantId;
    public string plantName;
    public string description;

/*    public int useNum;
    public int cdTime;*/
    public GameObject cardPrefab;

    public override string ToString()
    {
        return "[plantId]:" + plantId.ToString();
    }
}
