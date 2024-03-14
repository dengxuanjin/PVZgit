using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeaShooter : Plant
{
    [SerializeField] private float interval;
    [SerializeField] private GameObject Bullet;
    [SerializeField] private Transform BulletPos;

    private float timer = 0;


    void Update()
    {
        if (!start) return;
        if (timer < interval)
        {
            timer += Time.deltaTime;
        }
        else
        {
            timer = 0;
            GameObject obj = GameObject.Instantiate(Bullet, BulletPos);
        }
    }

}
