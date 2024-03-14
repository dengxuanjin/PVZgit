using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserPanel : BasePanel
{
    public Button btnOk;
    public Button btnCancel;
    public Button btnDelete;

    public ScrollRect scroll;
    public GameObject UserNamePrefab;
    public List<UserData> testData;

    private Dictionary<string, UserNameItem> menuNameItems;
    private  string curUserName;
    public string CurUserName
    {
        get
        {
            return curUserName;
        }
        set
        {
            curUserName = value;
            RefreshSelectState();

        }
    }
    public override void Awake()
    {
        base.Awake();
        btnOk.onClick.AddListener(OnBtnOk);
        btnCancel.onClick.AddListener(OnBtnCancel);
        btnDelete.onClick.AddListener(OnBtnDelete);
        testData = new List<UserData>();
        menuNameItems = new Dictionary<string, UserNameItem>();
        EventCenter.Instance.AddEventListener<UserData>(EventType.EventNewUserCreate, OnEventNewUserCreate);
        EventCenter.Instance.AddEventListener<string>(EventType.EventCurrentUserChange, OnEventCurrentUserChange);
        EventCenter.Instance.AddEventListener<UserData>(EventType.EventUserDelete, OnEventUserDelete);
    }
    private void Start()
    {
        RefreshMainPanel();
        CurUserName = BaseManager.instance.currentUserName;
    }
    private void OnEventNewUserCreate(UserData userData)
    {
        curUserName = userData.name;
        RefreshMainPanel();
    }

    private void OnEventUserDelete(UserData userdata)
    {
        RefreshMainPanel();
    }
    void OnEventCurrentUserChange(string curName)
    {
        curUserName = curName;
    }
    public void RefreshSelectState()
    {
        foreach (UserNameItem item in menuNameItems.Values)
        {
            item.RefreSelect();
        }
    }

    public void RefreshMainPanel()
    {
        menuNameItems.Clear();
        foreach (Transform child in scroll.content)
        {
            // 首先先删除所有的子节点
            Destroy(child.gameObject);
        }
        // 创建存储的用户
        foreach (UserData user in LocalConfig.LoadAllUseData())
        {
            Transform prefab = GameObject.Instantiate(UserNamePrefab).transform;
            prefab.SetParent(scroll.content, false);
            prefab.localPosition = Vector3.zero;
            prefab.localScale = Vector3.one;
            prefab.localRotation = Quaternion.identity;
            prefab.GetComponent<UserNameItem>().InitItem(user, this);
            menuNameItems.Add(user.name, prefab.GetComponent<UserNameItem>());
        }
        // 创建新用户
        Transform prefab1 = GameObject.Instantiate(UserNamePrefab).transform;
        prefab1.SetParent(scroll.content, false);
        prefab1.localPosition = Vector3.zero;
        prefab1.localScale = Vector3.one;
        prefab1.localRotation = Quaternion.identity;
        prefab1.GetComponent<UserNameItem>().InitNewUserItem();

    }

    private void OnBtnOk()
    {
        Debug.Log(LocalConfig.LoadUserData(CurUserName).name);
        if(CurUserName == "")
        {
            print(">>>>>>>>>Enpty");
            return;
        }
        BaseManager.instance.SetCurrentUserName(curUserName);
        ClosePanel();

    }
    private void OnBtnCancel()
    {
        Debug.Log("Cancel");
        ClosePanel();
    }
    private void OnBtnDelete()
    {
        if(curUserName == "")
        {
            return;
        }
        bool isSuccess = LocalConfig.ClearUserData(curUserName);
        if (isSuccess && curUserName == BaseManager.instance.currentUserName)
        {
            List<UserData> users = LocalConfig.LoadAllUseData();
            if(users.Count > 0)
            {
                BaseManager.instance.SetCurrentUserName(users[0].name);
            }
            else
            {
                BaseManager.instance.SetCurrentUserName("");
                BaseUIManager.Instance.OpenPanel(UIConst.NewUserPanel);
            }
        }
        RefreshMainPanel();
        curUserName = "";
    }

    public override void ClosePanel()
    {
        base.ClosePanel();
        EventCenter.Instance.RemoveEventListener<string>(EventType.EventCurrentUserChange, OnEventCurrentUserChange);
        EventCenter.Instance.RemoveEventListener<UserData>(EventType.EventNewUserCreate, OnEventNewUserCreate);
        EventCenter.Instance.RemoveEventListener<UserData>(EventType.EventUserDelete, OnEventUserDelete);

    }
}
