using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallNut : Plant
{

     protected override void Start()
    {
        base.Start();
    }
    void Update()
    {

    }

    public override float ChangeHealth(float num)
    {

        float boor = base.ChangeHealth(num);
        m_animator.SetFloat("BloodPercent", (float)boor / health);
        return boor;
    }

}
