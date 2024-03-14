using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{
    private static UIManager instance;

    public TextMeshProUGUI sunText;
    public ProgressPanel progressPanel;
    public AllCardPanel allCardPanel;
    public ChooseCardPanel chooseCardPanel;
    public static UIManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new UIManager();
            }
            return instance;
        }
        set
        {
            instance = value;
        }
    }
    private void Start()
    {
        Instance = this;

    }


    public void InitUI()
    {
        sunText.text = GameManager.Instance.sunNum.ToString();
        progressPanel.gameObject.SetActive(false);
        allCardPanel.InitCard();
    }
    public void UpdateUI()
    {
        sunText.text = GameManager.Instance.sunNum.ToString();
    }
    public void InitGrogressPanel()
    {
        LevelInfoItem levelInfo = GameManager.Instance.levelInfo.LevelDataList[GameManager.Instance.curLevelId-1];
        
        for(int i=0; i<levelInfo.progressPercent.Length; i++)
        {
            if(levelInfo.progressPercent[i] == 1)
            {
                continue;
            }
            progressPanel.SetFlagParcent(levelInfo.progressPercent[i]);
        }
        progressPanel.SetParcent(0);
        progressPanel.gameObject.SetActive(true);
    }
    public void UpdateProgressPanel()
    {
        int progressNum = 0;
        for(int i = 0; i< GameManager.Instance.levelData.LevelDataList.Count; i++)
        {
            LevelItem levelItem = GameManager.Instance.levelData.LevelDataList[i];
            // 筛选当前关卡的僵尸
            if (levelItem.levelId == GameManager.Instance.curLevelId && levelItem.progressId == GameManager.Instance.curProgressId)
            {
                progressNum++;
            }
        }
        int remainNum = GameManager.Instance.curProgressZombie.Count;
        //当前波次进行到百分比
        float percent = (float)(progressNum - remainNum) / progressNum;
        // 当前波次比例，前一波次比例
        LevelInfoItem levelInfoItem = GameManager.Instance.levelInfo.LevelDataList[GameManager.Instance.curLevelId-1];
        float progressPercent = levelInfoItem.progressPercent[GameManager.Instance.curProgressId-1];
        float lastProgressPercent = 0;
        if(GameManager.Instance.curProgressId > 1)
        {
            lastProgressPercent = levelInfoItem.progressPercent[GameManager.Instance.curProgressId - 2];
        }
        // 最终比例 = 当前波次百分比 + 前一波次百分比
        float finalPercent = percent * (progressPercent - lastProgressPercent) + lastProgressPercent;
        Debug.Log(finalPercent);
        progressPanel.SetParcent(finalPercent);
    }
}
