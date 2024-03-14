using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sun : MonoBehaviour
{
    [SerializeField] private float readyTime;
    private float Timer = 0;

    private Vector3 targetPos;
    void Start()
    {
        Timer = 0;
    }

    public void SetTargetPos(Vector3 pos)
    {
        this.targetPos = pos;
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
