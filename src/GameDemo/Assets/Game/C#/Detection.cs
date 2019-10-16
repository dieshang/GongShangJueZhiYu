using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detection : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {

    
        if (collision.gameObject.tag == "Player")
        {
           GetComponentInParent<Monster>().ToPursue();
            Debug.Log("找到玩家");
        }
       
    }
   /* private void OnTriggerStay2D(Collider2D collision)
    {
        
    }*/
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
           GetComponentInParent<Monster>().ToPatrol();
            Debug.Log("失去玩家");
        }
    }
}
