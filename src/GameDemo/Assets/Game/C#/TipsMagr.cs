using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//新手教学提示
public class TipsMagr : MonoBehaviour
{

   public int step=1;
    bool Normal=true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (GameObject.Find("player").GetComponent<Interaction>().Hasnote.Count >= 1 && step == 1)
        {

            Normal = false;

        }
        if (GameObject.Find("player").GetComponent<Interaction>().Hasnote.Count >= 1&& step==2)
        {
           
            if (!Normal) step++;
            step++;

        }
        else if (Input.GetKeyDown(KeyCode.K) && (step == 1|| step == 3 || step == 4))
        {
           
            step++;
        }
        if (GameObject.Find("GDoor").GetComponent<AudioSource>().isPlaying) { step = 5; }
        UpdateTips();

    }


   void UpdateTips()
    {
        if (!Normal) transform.GetChild(1).gameObject.SetActive(false);
        if((step - 1) >= 0&& (step-1)<transform.childCount) { 
        transform.GetChild(step-1).gameObject.SetActive(true);
        }
        if ((step - 2) >= 0 && (step - 2) < transform.childCount)
        {
            if (step == 5 && !GameObject.Find("GDoor").GetComponent<AudioSource>().isPlaying) return;
            transform.GetChild(step - 2).gameObject.SetActive(false);
        }
    }
}
