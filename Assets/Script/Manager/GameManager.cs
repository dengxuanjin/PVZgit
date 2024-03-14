using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public int sunNum;
    public GameObject bornParent;
    public GameObject zombiePrefab;
    public float createZombieTimer;
    private int zOrderindex;

    public LevelData levelData;
    public LevelInfo levelInfo;
    public PlantInfo plantInfo;

    public bool gameStart;
    public int curLevelId = 1;  // �ؿ�
    public int curProgressId = 1;   //����
    public List<GameObject> curProgressZombie;
    public static GameManager Instance
    {
        get
        {
            if(instance == null)
            {
                instance = new GameManager();
            }
            return instance;
        }
        set
        {
            instance = value;
        }
    }
    private void Start()
    {
        Instance = this;
        curProgressZombie = new List<GameObject>();
        ReadData();
    }
    private void Update()
    {
    }

    void ReadData()
    {
        StartCoroutine(LoadTable());
        //LoadTableNew();
    }
    public void LoadTableNew()
    {
        levelData = Resources.Load("TablesObject/Level") as LevelData;
        levelInfo = Resources.Load("TablesObject/LevelInfo") as LevelInfo;
        plantInfo = Resources.Load("TablesObject/PlantInfo") as PlantInfo;
        GameStart();
    }

    // �첽��ȡ
    IEnumerator LoadTable()
    {
        ResourceRequest request = Resources.LoadAsync("TablesObject/Level");
        ResourceRequest request2 = Resources.LoadAsync("TablesObject/LevelInfo");
        ResourceRequest request3 = Resources.LoadAsync("TablesObject/PlantInfo");
        yield return request;
        yield return request2;
        yield return request3;
        levelData = request.asset as LevelData;
        levelInfo = request2.asset as LevelInfo;
        plantInfo = request3.asset as PlantInfo;
        GameStart();
    }
    public void GameReallyStart()
    {
        gameStart = true;
        CreateZobie();
        SoundManager.Instance.PlayBGM(Globals.BGM1);
        InvokeRepeating("CreateSunDown", 5, 10);
    }
    private void GameStart()
    {
        UIManager.Instance.InitUI();
    }
    public void ChangeSunNum(int num)
    {
        sunNum += num;
        if(sunNum <= 0)
        {
            sunNum = 0;
        }
        UIManager.Instance.UpdateUI();
    }
    public void CreateZobie()
    {
        //StartCoroutine(DalayCreateZombie());
        TableCreateZombie();
        UIManager.Instance.InitGrogressPanel();
    }
    //���б���ɾ����ʬ
    public void ZombieDied(GameObject gameObject)
    {
        if (curProgressZombie.Contains(gameObject))
        {
            UIManager.Instance.UpdateProgressPanel();
            curProgressZombie.Remove(gameObject);
        }
        if(curProgressZombie.Count == 0)
        {
            curProgressId++;
            TableCreateZombie();
        }
    }
    private void TableCreateZombie()
    {
        bool canCreate = false;
        for(int i = 0; i<levelData.LevelDataList.Count; i++)
        {
            LevelItem levelItem = levelData.LevelDataList[i];
            // ɸѡ��ǰ�ؿ��Ľ�ʬ
            if(levelItem.levelId == curLevelId && levelItem.progressId == curProgressId)
            {
                StartCoroutine(ITableCreateZombie(levelItem));
                canCreate = true;
            }
        }
        if (!canCreate)
        {
            // ֹͣ����Э��
            StopAllCoroutines();
            curProgressZombie = new List<GameObject>();
            gameStart = false;
        }
    }
    // ����levelItem����
    IEnumerator ITableCreateZombie(LevelItem   levelItem) 
    {
        yield return new WaitForSeconds(levelItem.createTime);
        GameObject zombiePrefab = Resources.Load("Prefabs/Zombie/Zombie" + levelItem.zombieType.ToString()) as GameObject;
        
        GameObject zombie = Instantiate(zombiePrefab);
        Transform zombieLine = bornParent.transform.Find("born" + levelItem.bornPos.ToString());
        zombie.transform.SetParent(zombieLine);
        zombie.transform.localPosition = Vector3.zero;
        zombie.GetComponent<SpriteRenderer>().sortingOrder = zOrderindex;
        zOrderindex++;
        curProgressZombie.Add(zombie);
    }
    // �ȴ�ʱ��������ɽ�ʬ
    IEnumerator DalayCreateZombie()
    {
        yield return new WaitForSeconds(createZombieTimer);
        GameObject zombie = Instantiate(zombiePrefab);
        int index = Random.Range(0, 5);
        Transform zombieLine = bornParent.transform.Find("born" + index.ToString());
        zombie.transform.SetParent(zombieLine);
        zombie.transform.localPosition = Vector3.zero;
        zombie.GetComponent<SpriteRenderer>().sortingOrder = zOrderindex;
        zOrderindex++;

        StartCoroutine(DalayCreateZombie());
    }
    // ����̫��
    public void CreateSunDown()
    {
        Vector3 leftBottom = Camera.main.ViewportToWorldPoint(Vector2.zero);
        Vector3 rightTop = Camera.main.ViewportToWorldPoint(Vector2.one);

        GameObject sunPrefab = Resources.Load("Prefabs/Effect/Sun") as GameObject;
        float x = Random.Range(leftBottom.x + 30, rightTop.x - 30);
        Vector3 bornPos = new Vector3(x, rightTop.y,0);
        GameObject sun = Instantiate(sunPrefab, bornPos, Quaternion.identity);
        //����Ŀ��λ��
        float y = Random.Range(leftBottom.y + 100, leftBottom.y + 30);
        sun.GetComponent<Sun>().SetTargetPos(new Vector3(bornPos.x, y, 0));
    }
    // ��ȡ��ǰֲ������һ��
    public int GetPlantLine(GameObject plant)
    {
        GameObject lineObject = plant.transform.parent.parent.gameObject;
        string lineStr = lineObject.name;
        int line = int.Parse(lineStr.Split("line")[1]);
        return line;
    }
    // ��ȡָ�������еĽ�ʬ
    public List<GameObject> GetLineZombies(int line)
    {
        string lineName = "born" + line.ToString();
        Transform bornObject = bornParent.transform.Find(lineName);
        List<GameObject> zombies = new List<GameObject>();
        for(int i=0; i< bornObject.childCount; i++)
        {
            zombies.Add(bornObject.GetChild(i).gameObject);
        }
        return zombies;
    }


}
