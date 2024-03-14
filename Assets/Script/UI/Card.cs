using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Card : MonoBehaviour,IPointerClickHandler
{
    //IPointerClickHandler点击接口
    [SerializeField]private float waitTime;
    [SerializeField]private int UseSun;
    [SerializeField] private GameObject objectPrefab;

    private GameObject curGameObject;
    private GameObject dank;
    private GameObject progress;

    private float Timer;


    public bool hasUse;
    public bool hasLock;
    public bool isMoving;
    public bool isStart;
    public PlantInfoItem infoItem;
    private void Start()
    {
        dank = transform.Find("dank").gameObject;
        progress = transform.Find("progress").gameObject;
        dank.SetActive(false);
        progress.SetActive(false);
    }

    private void Update()
    {

        if (!GameManager.Instance.gameStart) return;
        if (!isStart)
        {
            Debug.Log("haflskdjlfk");
            isStart = true;
            dank.SetActive(true);
            progress.SetActive(true);
        }
        Timer += Time.deltaTime;
        UpdateProgress();
        UpdateDarkBg();
    }

    private void UpdateProgress()
    {
        float per = Mathf.Clamp(Timer / waitTime, 0, 1);
        progress.GetComponent<Image>().fillAmount = 1 - per;
    }
    private void UpdateDarkBg()
    {
        if (progress.GetComponent<Image>().fillAmount == 0 && GameManager.Instance.sunNum >= UseSun)
        {
            dank.SetActive(false);
        }
        else
        {
            dank.SetActive(true);
        }
    }
    public void OnBeginDrag(BaseEventData data)
    {
        if (!isStart) return;
        if (hasLock) return;
        if (dank.activeSelf)
        {
            return;
        }
        PointerEventData pointerEventData = data as PointerEventData;
        curGameObject = GameObject.Instantiate(objectPrefab);
        curGameObject.transform.position = TranlateScreenToWorld(pointerEventData.position);
        SoundManager.Instance.PlaySound(Globals.S_Seedlift);
    }
    public void OnDrag(BaseEventData data)
    {
        if(curGameObject == null)
        {
            return;
        }

        PointerEventData pointerEventData = data as PointerEventData;
        curGameObject.transform.position = TranlateScreenToWorld(pointerEventData.position);

    }
    public void OnEndDrag(BaseEventData data)
    {
        if (curGameObject == null)
        {
            return;
        }
        PointerEventData pointerEventData = data as PointerEventData;
        Collider2D[] colliders = Physics2D.OverlapPointAll(TranlateScreenToWorld(pointerEventData.position));
        foreach (Collider2D collider in colliders)
        {
            if(collider.tag == "Land"&& collider.gameObject.transform.childCount == 0)
            {
                curGameObject.transform.SetParent(collider.gameObject.transform);
                curGameObject.transform.localPosition = Vector3.zero;
                curGameObject.GetComponent<Plant>().SetPlantStart();
                curGameObject = null;
                GameManager.Instance.ChangeSunNum(-UseSun);
                SoundManager.Instance.PlaySound(Globals.S_Plant);
                Timer = 0;
                break;

            }
        }

        if(curGameObject != null)
        {
            GameObject.Destroy(curGameObject);
            curGameObject = null;
        }

    }
    public static Vector3 TranlateScreenToWorld(Vector3 position)
    {
        Vector3 cameraTranslatePos = Camera.main.ScreenToWorldPoint(position);
        return new Vector3(cameraTranslatePos.x, cameraTranslatePos.y, 0);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (isMoving) return;
        if (hasLock) return;
        if (hasUse)
        {
            RemoveCard(gameObject);
        }
        else
        {
            AddCard();
        }
    }

    public void RemoveCard(GameObject removeCard)
    {
        ChooseCardPanel chooseCardPanel = UIManager.Instance.chooseCardPanel;
        if (chooseCardPanel.ChooseCard.Contains(removeCard))
        {
            Card rcard = removeCard.GetComponent<Card>();
            rcard.isMoving = true;
            chooseCardPanel.ChooseCard.Remove(removeCard);
            chooseCardPanel.UpdateCardPos();

            Transform cardParent = UIManager.Instance.allCardPanel.Bg.transform.Find("Card" + rcard.infoItem.plantId);
            Vector3 curPosition = removeCard.transform.position;
            removeCard.transform.SetParent(UIManager.Instance.transform, false);
            removeCard.transform.position = curPosition;
            removeCard.transform.DOMove(cardParent.position, 0.3f).OnComplete(() =>
            {
                cardParent.GetComponentInChildren<Card>().dank.SetActive(false) ;
                cardParent.GetComponentInChildren<Card>().hasLock = false;
                hasLock = false;
                GameObject.Destroy(removeCard);
            });
        }
    }

    public void AddCard()
    {
        ChooseCardPanel chooseCardPanel = UIManager.Instance.chooseCardPanel;
        int curIndex = chooseCardPanel.ChooseCard.Count;
        if(curIndex >= 8)
        {
            Debug.Log("超出最大值");
            return;
        }
        GameObject useCard = GameObject.Instantiate(infoItem.cardPrefab);
        useCard.transform.SetParent(UIManager.Instance.transform);
        useCard.transform.position = transform.position;
        useCard.name = "Card";
        useCard.GetComponent<Card>().infoItem = infoItem ;
        Transform targetObject = chooseCardPanel.Cards.transform.Find("Card"+curIndex);
        hasLock = true;
        dank.SetActive(true);
        // 移动到目标位置
        useCard.GetComponent<Card>().isMoving = true;
        useCard.GetComponent<Card>().hasUse = true;
        chooseCardPanel.ChooseCard.Add(useCard);
        // DoMove移动到目标位置
        useCard.transform.DOMove(targetObject.position, 0.3f).OnComplete(
            () => {
                useCard.transform.SetParent(targetObject, false);
                useCard.transform.localPosition = Vector3.zero;
                useCard.GetComponent<Card>().isMoving = false;
                useCard.GetComponent<Card>().hasUse = true;
            });
    }
}
