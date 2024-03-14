using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "LevelInfo", menuName = "Level/LevelInfo")]
public class LevelInfo : ScriptableObject
{
    public List<LevelInfoItem> LevelDataList = new List<LevelInfoItem>();
}

[Serializable]

public class LevelInfoItem
{
    public int LevelId;
    public string levelName;
    public float[] progressPercent;

}
