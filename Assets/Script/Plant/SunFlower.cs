using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunFlower : Plant
{
    [SerializeField] private float readyTime;
    [SerializeField] private GameObject sunPrefab;


    private bool isSpwanAnimator;
    private float Timer;
    private int sunNum;

     protected override void Start()
    {
        base.Start();
        sunNum = 0;
    }


    void Update()
    {
        if (!start) return;
        if(Timer < readyTime)
        {
            Timer += Time.deltaTime;
        }
        else if(!isSpwanAnimator)
        {
            m_animator.SetBool("Ready", true);
            isSpwanAnimator = true;
        }
    }
    public void BornSunOver()
    {
        BornSun();
        m_animator.SetBool("Ready", false);
        Timer = 0;
        isSpwanAnimator = false;
    }
    private void BornSun()
    {
        GameObject sunNew = GameObject.Instantiate(sunPrefab);
        sunNum += 1;
        float RandomX = 0;
        if(sunNum % 2 == 1)
        {
            RandomX = Random.Range(transform.position.x - 30, transform.position.x - 20);

        }
        else
        {
            RandomX = Random.Range(transform.position.x + 30, transform.position.x + 20);

        }
        float RandomY = Random.Range(transform.position.y - 20, transform.position.y + 20);
        sunNew.transform.position = new Vector2(RandomX, RandomY);
    }

}
