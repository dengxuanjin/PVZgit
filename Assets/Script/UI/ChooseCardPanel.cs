using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseCardPanel : MonoBehaviour
{
    public GameObject Cards;
    [SerializeField] private GameObject beforeCardPrefab;
    public List<GameObject> ChooseCard;
    private void Start()
    {
        ChooseCard = new List<GameObject>();
        for(int i=0; i < 8; i++)
        {
            GameObject beforeCard = GameObject.Instantiate(beforeCardPrefab);
            beforeCard.transform.SetParent(Cards.transform, false);
            beforeCard.name = "Card" + i.ToString();
            beforeCard.transform.Find("bg").gameObject.SetActive(false);
        }
    }

    public void UpdateCardPos()
    {
        for(int i = 0; i< ChooseCard.Count; i ++)
        {
            GameObject useCard = ChooseCard[i];
            Transform targetObject = Cards.transform.Find("Card" + i.ToString());
            useCard.GetComponent<Card>().isMoving = true ;
            useCard.transform.DOMove(targetObject.position, 0.3f).OnComplete(()=>
            {
                useCard.transform.SetParent(targetObject, false);
                useCard.transform.localPosition = Vector3.zero;
                useCard.GetComponent<Card>().isMoving = false ;
            });
        }
    }
}
