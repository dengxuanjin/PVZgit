using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingHandle : MonoBehaviour
{
    void Start()
    {
        transform.DORotate(new Vector3(0, 0, -360), 2.0f, RotateMode.FastBeyond360).SetEase(Ease.Linear).SetLoops(-1, LoopType.Restart);
        // SetLoops(-1, LoopType.Restart);ѭ����ʽ����Ϊ����ѭ��
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
