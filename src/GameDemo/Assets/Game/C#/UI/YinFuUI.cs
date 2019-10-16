using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class YinFuUI : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        YinFuLoad.Add("GMusic", @"MusicUi/G1");
        YinFuLoad.Add("SMusic", @"MusicUi/S1");
        YinFuLoad.Add("JMusic", @"MusicUi/J1");
        YinFuLoad.Add("ZMusic", @"MusicUi/Z1");
        YinFuLoad.Add("YMusic", @"MusicUi/Y1");

        YinFuIndex.Add("GMusic", 0);
        YinFuIndex.Add("SMusic",1);
        YinFuIndex.Add("JMusic",2);
        YinFuIndex.Add("ZMusic",3);
        YinFuIndex.Add("YMusic",4 );
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    Dictionary<string, string> YinFuLoad = new Dictionary<string, string>();
    Dictionary<string, int> YinFuIndex = new Dictionary<string, int>();

    public void YinFu(string YinFuName)
    {

        transform.GetChild(YinFuIndex[YinFuName]).GetComponent<Image>().sprite = Resources.Load<Sprite>(YinFuLoad[YinFuName]);
        transform.GetChild(YinFuIndex[YinFuName]).GetComponent<Animator>().SetTrigger("Gain");
        
    }
}
