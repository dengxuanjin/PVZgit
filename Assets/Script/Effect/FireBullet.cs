using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBullet : Bullet
{
    public override void DestroyBullet()
    {
        base.DestroyBullet();
        GameObject FirePrefabs = Resources.Load("Prefabs/Effect/Fire") as GameObject;
        GameObject FireObject = Instantiate(FirePrefabs, transform.position, Quaternion.identity);

    }
}
