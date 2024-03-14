using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AllCardPanel : MonoBehaviour
{
    public GameObject Bg;
    [SerializeField] Button StartBullet;
    [SerializeField] private GameObject CardPrefabs;
    private void Awake()
    {
        StartBullet.onClick.AddListener(() =>
        {
            GameManager.Instance.GameReallyStart();
        });
    }

    private void Start()
    {
        for(int i = 0; i< 40; i++)
        {
            GameObject card = GameObject.Instantiate(CardPrefabs);
            card.transform.SetParent(Bg.transform, false);
            card.name = "Card" + i.ToString();
        }

    }

    public void InitCard()
    {
        foreach(PlantInfoItem item in GameManager.Instance.plantInfo.PlantDataList)
        {
            GameObject c1 = Bg.transform.Find("Card" + item.plantId.ToString()).gameObject;
            GameObject c2 = GameObject.Instantiate(item.cardPrefab);
            c2.transform.SetParent(c1.transform, false) ;
            c2.transform.localPosition = Vector3.zero;
            c2.GetComponent<Card>().infoItem = item;
        }
    }
}
