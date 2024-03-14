using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torchwood : Plant
{
    private GameObject FireBullet;
    protected override void Start()
    {
        base.Start();
        FireBullet = Resources.Load("Prefabs/Effect/FireBullet") as GameObject;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Bullet")
        {
            Bullet bt = collision.transform.GetComponent<Bullet>();
            if (bt.torchwoodCreate) return;
            bt.DestroyBullet();
            CreateBullet(bt.transform.position);

        }
    }
    private void CreateBullet(Vector3 borPos)
    {
        GameObject fireBullet = Instantiate(FireBullet, borPos, Quaternion.identity);
        fireBullet.transform.parent = transform.parent;
        fireBullet.transform.position = borPos;
        fireBullet.GetComponent<Bullet>().torchwoodCreate = true;
    }
}
