using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuPanel : BasePanel
{
    public Button btnChangeUser;
    public Text UserName;
    public Text SmallLevel;
    public override void Awake()
    {
        base.Awake();
        btnChangeUser.onClick.AddListener(OnChangeUser);
        EventCenter.Instance.AddEventListener<string>(EventType.EventCurrentUserChange, OnUserChangeText);

        EventCenter.Instance.AddEventListener<UserData>(EventType.EventUserDelete, OnUserDelete);
    
    }

    private void Start()
    {
        if (BaseManager.instance.currentUserName == "")
        {
            BaseUIManager.Instance.OpenPanel(UIConst.NewUserPanel);
            return;
        }
        UserName.text = BaseManager.instance.currentUserName;
        UserData userData = LocalConfig.LoadUserData(BaseManager.instance.currentUserName);
        if (userData != null)
        {
            SmallLevel.text = userData.level.ToString();
        }
    }
    private void OnUserChangeText(string name)
    {
        
        UserName.text = name;
        if (LocalConfig.LoadUserData(name) == null)
        {
            SmallLevel.text = "1";
            return;
        }
        SmallLevel.text = LocalConfig.LoadUserData(name).level.ToString();
    }
    private void OnUserDelete(UserData userData)
    {

        /*UserName.text = userData.name;

        SmallLevel.text = userData.level.ToString();*/
    }

    private void OnChangeUser()
    {
        BaseUIManager.Instance.OpenPanel(UIConst.UserPanel);
    }

    public override void ClosePanel()
    {
        base.ClosePanel();
        EventCenter.Instance.RemoveEventListener<string>(EventType.EventCurrentUserChange, OnUserChangeText);
        EventCenter.Instance.RemoveEventListener<UserData>(EventType.EventUserDelete, OnUserDelete);
    }
}
