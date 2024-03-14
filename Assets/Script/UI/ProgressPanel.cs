using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressPanel : MonoBehaviour
{
    private GameObject Progress;
    private GameObject Head;
    private GameObject Flag;
    private GameObject FlagPrefabs;
    private GameObject LevelText;
    private GameObject Bg;
    private void Start()
    {
        Progress = transform.Find("Progress").gameObject;
        Head = transform.Find("Head").gameObject;
        LevelText = transform.Find("LevelText").gameObject;
        Bg = transform.Find("Bg").gameObject;
        Flag = transform.Find("Flag").gameObject;
        FlagPrefabs = Resources.Load("Prefabs/Effect/Flag") as GameObject;


    }

    private void Update()
    {
        
    }
    public void SetParcent(float per)
    {
        Progress.GetComponent<Image>().fillAmount = per;
        // 头的初始位置
        float OriginPosX = Bg.GetComponent<RectTransform>().localPosition.x + Bg.GetComponent<RectTransform>().sizeDelta.x / 2;
        float width = Bg.GetComponent<RectTransform>().sizeDelta.x;
        float offset = 0;
        //设置移动 最右边的位置-进度条的一半 +自定义的偏移
        Head.GetComponent<RectTransform>().localPosition = new Vector2(OriginPosX-per*width+offset, Head.GetComponent<RectTransform>().localPosition.y); 
        Head.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);

    }

    public void SetFlagParcent(float per)
    {
        Flag.SetActive(false);
        // 棋子的初始位置
        float OriginPosX = Bg.GetComponent<RectTransform>().localPosition.x + Bg.GetComponent<RectTransform>().sizeDelta.x / 2;
        float width = Bg.GetComponent<RectTransform>().sizeDelta.x;
        
        float offset = 10;
        GameObject newFlag = Instantiate(FlagPrefabs);
        newFlag.transform.SetParent(gameObject.transform,false);
        newFlag.GetComponent<RectTransform>().localPosition = Flag.GetComponent<RectTransform>().localPosition;
        //设置移动 最右边的位置-进度条的一半 +自定义的偏移
        newFlag.GetComponent<RectTransform>().localPosition = new Vector2(OriginPosX - per * width + offset, newFlag.GetComponent<RectTransform>().localPosition.y);
        newFlag.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
        Head.GetComponent<RectTransform>().SetAsLastSibling();
    }
}
