using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingHandle : MonoBehaviour
{
    void Start()
    {
        transform.DORotate(new Vector3(0, 0, -360), 2.0f, RotateMode.FastBeyond360).SetEase(Ease.Linear).SetLoops(-1, LoopType.Restart);
        // SetLoops(-1, LoopType.Restart);循环方式这里为永久循环
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
