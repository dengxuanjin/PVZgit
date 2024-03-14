using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserNameItem : MonoBehaviour
{
    private GameObject select;
    private Text text;

    private Button btn;
    public UserData userData;

    private UserPanel parent;
    public string ItemType = "name";

    private void Awake()
    {
        select = transform.Find("Select").gameObject;
        select.SetActive(false);
        text = transform.Find("Name").GetComponent<Text>();
        btn = GetComponent<Button>();
        btn.onClick.AddListener(OnBtnNameItem);
    }

    public void InitItem(UserData userData, UserPanel userPanel)
    {
        this.userData = userData;
        this.text.text = userData.name;
        this.parent = userPanel;
    }
    public void InitNewUserItem()
    {
        ItemType = "new";
        text.text = "创建新用户"; 
    }
    public void RefreSelect()
    {
        select.SetActive(userData.name == parent.CurUserName);
    }
    void OnBtnNameItem()
    {
        if(ItemType == "name")
        {
            Debug.Log(parent.CurUserName);
            Debug.Log(userData.name);
            parent.CurUserName = userData.name;
        }
        else
        {

            BaseUIManager.Instance.OpenPanel(UIConst.NewUserPanel);
        }
    }

}
