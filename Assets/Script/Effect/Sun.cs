using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sun : MonoBehaviour
{
    [SerializeField] private float readyTime;
    private float Timer = 0;

    private Vector3 targetPos;
    private SpriteRenderer sr;
    void Start()
    {
        Timer = 0;
        sr = GetComponent<SpriteRenderer>();
        CraeteSun();
    }

    public void SetTargetPos(Vector3 pos)
    {
        this.targetPos = pos;
    }

    private void CraeteSun()
    {
        transform.DOScale(0, 0.5f).From() ;
        sr.DOFade(0, 0.5f).From();
    }

    void Update()
    {
        if (targetPos != Vector3.zero && Vector3.Distance(targetPos, transform.position) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, 0.1f);
            return;
        }
        Timer += Time.deltaTime;
        if(Timer >= readyTime)
        {
            GameObject.Destroy(gameObject);
        }
    }

    private void OnMouseDown()
    {
        GameManager.Instance.ChangeSunNum(25);
        GameObject.Destroy(gameObject);
    }
}
