using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxRevert : MonoBehaviour
{
    private Vector3 pos;
    // Start is called before the first frame update
    void Start()
    {
        pos = this.gameObject.transform.position;
        pos.y = 27;
    }

    void OnCollisionEnter2D(Collision2D col )
    {
        if (col.gameObject.name=="Revert")
        {
            Debug.Log("碰撞到的物体的名字是："+col.gameObject.name);
            this.gameObject.transform.position = pos;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
