using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewUserPanel : BasePanel
{
    public Button btnOk;
    public Button btnCancel;
    public InputField inputField;

    private string inputstring;

    private string curUserName;

    public override void Awake()
    {
        base.Awake();
        btnOk.onClick.AddListener(OnBtnOk);
        btnCancel.onClick.AddListener(OnBtnCancel);
        inputField.onValueChanged.AddListener(OnInputChange);
    }
    private void OnBtnOk()
    {
        if(inputstring.Trim() == "")
        {
            print(">>>>>>>>>>>>>input string is emyty!");
            return;
        }else if(LocalConfig.LoadUserData(inputstring) != null)
        {
            print(">>>>>>>>>>>>>input string has exist!");
            return;
        }
        UserData userdata = new UserData(inputstring, 1);
        LocalConfig.SaveUserData(userdata);
        EventCenter.Instance.EventTrigger<UserData>(EventType.EventNewUserCreate, userdata);
        ClosePanel();
    }
    private void OnBtnCancel()
    {
        Debug.Log("Cancel");
        ClosePanel();
    }
    public void OnInputChange(string value)
    {
        inputstring = value;
    }
}
