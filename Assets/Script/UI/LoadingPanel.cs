using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingPanel : MonoBehaviour
{
    public Slider slider;
    public Button BtnStart;

    private float timer = 2f;
    private float curTime;

    public bool really;

    AsyncOperation operation;
    private void Start()
    {
        BtnStart.gameObject.SetActive(false);
        BtnStart.onClick.AddListener(Onclickbtnstart);
        curTime = 0;
        slider.value = curTime;
        if (really)
        {
            operation =  SceneManager.LoadSceneAsync("Menu");
            operation.allowSceneActivation = false;// 是否立即跳转
        }
    }

    private void Onclickbtnstart()
    {
        if (!really)
        {
            SceneManager.LoadScene("Menu");
            Debug.Log("dakai");
        }
        else
        {
            operation.allowSceneActivation = true;// 是否立即跳转

        }

        DOTween.Clear();
    }


    private  void OnSliderValueChange(float value)
    {
        slider.value = value;
        if(value >= 1f)
        {
            BtnStart.gameObject.SetActive(true);
        }
    }

    private void Update()
    {
        if (!really)
        {
            curTime += Time.deltaTime / timer;
            if (curTime >= 1)
            {
                curTime = 1;
            }
            OnSliderValueChange(curTime);
        }
        else
        {
            curTime = Mathf.Clamp01(operation.progress / 0.9f);
            OnSliderValueChange(curTime);
        }

    }
}
