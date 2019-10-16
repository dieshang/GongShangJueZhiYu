using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class QTE : MonoBehaviour
{ public bool IsQteOpen
    { set; get;
    }
    int KeyCodeCount;

    // Start is called before the first frame update
    void Start()
    {
        IsQteOpen = false;
        Area = GetComponent<RectTransform>();
      

       
    }

    enum ScuessedType{
        Perfect,
        Great,
        Cool
    }
    public void OpenQte(float OpenTime, int keyCodeCount)
    {
        KeyCodeCount = keyCodeCount;
        if (!IsQteOpen) { 
           IsQteOpen = true;
        // InvokeRepeating("CreateItem", 0.5f, intervaltime);

        float SumTime = 0;
            //如果总时长小于规定的时长（0.5为第一次开始的间隔时间）
            while (SumTime <= OpenTime - 0.5f) {
                
                    //随机一个间隔时间
                    int i = Random.Range(1,10);
                    float t1;
                    //7：3的机率
                    if (i < 7) { 
                   t1 = Random.Range(1.1f, 1.5f);
                        SumTime +=( t1+ FailTime);
                       
                }
                    else
                    {
                       t1 = Random.Range(1.5f, 2.0f);
                        SumTime += (t1 + FailTime);
                    }

                   
                intervaltime.Add(t1);
        }
        StartTime = Time.time;
        EndTimeLong = OpenTime;
        CancelInvoke("CloseQte");
        Invoke("CloseQte", SumTime);
            StartCoroutine("StartQte");

        }


    }


    IEnumerator StartQte()
    {
   
        for (int i = 0; i < intervaltime.Count; i++) { 
            CreateItem();
           
        yield return new WaitForSeconds(intervaltime[i]);
            //如果都生成完了 游戏却还没结束
            if (i == intervaltime.Count - 1 && IsQteOpen)
            {
                CloseQte();
                //继续生成
                OpenQte(EndTimeLong - (Time.time - StartTime), KeyCodeCount);
            }
        }

    }
   
    // Update is called once per frame
    void Update()
    {
     
        if (IsQteOpen) {
            UpdateQTE();
            if (IsQteOpen)
            {
                MonitorPlayerInput();
            }
        }


   
    }
    /// <summary>
    /// qte开始的时间
    /// </summary>
    float StartTime;
    /// <summary>
    /// qte结束的时长
    /// </summary>
    float EndTimeLong;
    /// <summary>
    /// 时间上限
    /// </summary>
    public float FailTime = 1.0f;
    /// <summary>
    /// 当前时间
    /// </summary>
    float time;
    /// <summary>
    /// 产生间隔时间
    /// </summary>
 // public  float intervaltime = 3f;
    public List<float> intervaltime = new List<float>();
    //KeyCode CurLey;
    /// <summary>
    /// 产生范围
    /// </summary>
    RectTransform Area;
    List<Item> CurItem = new List<Item>();
    public delegate void FailDelegate();
    public FailDelegate OnFailEvent;

    public class Item {
        public GameObject spriteObj;
        public float totalTime { set; get; }
        public int keyCode { set; get; }
        public Item(GameObject Obj, int code, float Time=0)
        {
            spriteObj = Obj;
            totalTime = Time;
            keyCode = code;
           
        }


    }

    private void Sucesed(ScuessedType st)
    {
        switch (st) {
            case ScuessedType.Perfect:
                CurItem[0].spriteObj.transform.GetChild(2).GetComponent<Text>().text = "Perfect";
                CurItem[0].spriteObj.GetComponent<Animator>().Play("Perfect");
                break;
            case ScuessedType.Great:
                CurItem[0].spriteObj.transform.GetChild(2).GetComponent<Text>().text = "Great";
                CurItem[0].spriteObj.GetComponent<Animator>().Play("Great");
                break;
            case ScuessedType.Cool:
                CurItem[0].spriteObj.transform.GetChild(2).GetComponent<Text>().text = "Cool";
                CurItem[0].spriteObj.GetComponent<Animator>().Play("Cool");
                break;

        }
        Destroy(CurItem[0].spriteObj, 0.2f);
        CurItem[0] = null;
        CurItem.Remove(CurItem[0]);
        Debug.Log("成功");
        GetComponent<AudioSource>().Play();
      
    }

    float GetCurItemZeroScaleX()
    {
        return CurItem[0].spriteObj.transform.GetChild(0).GetComponent<RectTransform>().localScale.x;
    }


    private void PDSucesed()
    {
        if (GetCurItemZeroScaleX() < 0.61515 && GetCurItemZeroScaleX() > 0.471669)
        {
            Sucesed(ScuessedType.Perfect);
        }
        else if (GetCurItemZeroScaleX() >= 0.80)
        {

            Sucesed(ScuessedType.Cool);

        }
        else
        {
            Sucesed(ScuessedType.Great);
        }
    }

    void MonitorPlayerInput()
    {

       
        if (CurItem.Count > 0) { 
        switch (CurItem[0].keyCode)
        {
            case 1:
                if (Input.GetKeyDown(KeyCode.Alpha1))
                {

                        PDSucesed();

                    }
                    break;
            case 2:
                if (Input.GetKeyDown(KeyCode.Alpha2))
                {
                        PDSucesed();
                    }
                break;
            case 3:
                if (Input.GetKeyDown(KeyCode.Alpha3))
                {
                        PDSucesed();
                    }
                break;

            case 4:

                if (Input.GetKeyDown(KeyCode.Alpha4))
                {
                        PDSucesed();
                    }
                break;
            case 5:

                if (Input.GetKeyDown(KeyCode.Alpha5))
                {
                        PDSucesed();
                    }
                break;
        }

        }
    }


    
    void UpdateQTE() {

        for(int i = 0; i < CurItem.Count; i++) {
            CurItem[i].totalTime += Time.deltaTime;
            if (CurItem[i].spriteObj.transform.GetChild(0).GetComponent<RectTransform>().localScale.x <= 0.2)
            {
                CurItem[i].totalTime = FailTime;
            }
            else
            {
                CurItem[i].spriteObj.transform.GetChild(0).GetComponent<RectTransform>().localScale = new Vector3(1 - (1 / FailTime * CurItem[i].totalTime), 1 - (1 / FailTime * CurItem[i].totalTime));
            }
            if (CurItem[i].totalTime >= FailTime) {
                //过了时间
                CurItem[i].spriteObj.transform.GetChild(2).GetComponent<Text>().text = "Miss";
                CurItem[i].spriteObj.GetComponent<Animator>().Play("Fail");
                Destroy(CurItem[i].spriteObj,0.5f);
                CurItem[i] = null;
                CurItem.Remove(CurItem[i]);
                Fail();

            }

        }


    }

  

    void CreateItem()
    {
        //如果CurItem的数目大于10 返回。
        if (CurItem.Count > 9) return;

        int Keycode = Random.Range(1, KeyCodeCount);
        GameObject aItemObject = Instantiate(Resources.Load<GameObject>("MusicItem"));
        aItemObject.transform.SetParent(transform);

        //如果CurItem的数目大于一定数量而不加限制会进入死循环
        while (true)
        {
            aItemObject.GetComponent<RectTransform>().anchoredPosition = RandomPos();
            bool CanSet = true;
            foreach (Item obj in CurItem)
            {
                //如果有重叠
                if (IsRectTransformOverlap(obj.spriteObj.GetComponent<RectTransform>(), aItemObject.GetComponent<RectTransform>()))
                {
                    CanSet = false; break;
                }

            }
            if (CanSet == true) { break; }
        }

        aItemObject.transform.GetChild(1).GetChild(0).GetChild(0).GetComponent<Text>().text = Keycode.ToString();
        Item aitem = new Item(aItemObject, Keycode);
        CurItem.Add(aitem);


     


    }

    Vector3 RandomPos()
    {
      return new Vector3(Random.Range(0, Area.rect.width), Random.Range(0, Area.rect.height));

}

    bool IsRectTransformOverlap(RectTransform rect1,RectTransform rect2)
    {
        if (Mathf.Abs(rect1.anchoredPosition.x - rect2.anchoredPosition.x) < rect1.rect.width / 2 + rect2.rect.height / 2 &&
            Mathf.Abs(rect1.anchoredPosition.y - rect2.anchoredPosition.y) < rect1.rect.height / 2 + rect2.rect.height / 2)
        {
            return true;
        }
        return false;
    }

    private void Fail()
    {
       
        Debug.Log("失败");
        CloseQte();
        if (OnFailEvent!=null)
        OnFailEvent();
;


    }

    public void CloseQte()
    {
        CancelInvoke("CloseQte");
        foreach (Item aitem in CurItem)
        {
            Destroy(aitem.spriteObj);
        }
        CurItem.Clear();
        IsQteOpen = false;
        StopCoroutine("StartQte");
        intervaltime.Clear();
        Debug.Log("关闭QTE");

    }
}
