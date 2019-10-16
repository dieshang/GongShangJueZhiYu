using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GainMusic : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Animator>().Play("GainMusic");
       // DontDestroyOnLoad(gameObject);
        //MoveToDi();
    }

   bool CanMove = false;

    public float Speed=1.5f;
  public  string PosName="G";

    Vector3 FollowPos;
   void MoveToDi()
    {
        CanMove = true;
    }
    // Update is called once per frame
    void Update()
    {
        FollowPos = GameObject.FindGameObjectWithTag("Player").transform.GetChild(3).transform.Find(PosName).position;
        if (CanMove) {
          
            transform.position = Vector3.Slerp(transform.position, FollowPos, Speed * Time.deltaTime);

            Rect rect = new Rect(FollowPos, new Vector2(20, 20));
            if (rect.Contains(transform.position)) { 
            //CanMove = false;
            //FollowPlayer = true;
                GetComponent<Animator>().Play("GainMusicNormal");
            }
        }
     //   if (FollowPlayer) { 
     //if(Vector3.Distance(transform.position, FollowPos) > 0.5f)
     //   {
     //       transform.position = Vector3.Slerp(transform.position, FollowPos, Speed * Time.deltaTime);
     //       }
     //   }

    }
}
