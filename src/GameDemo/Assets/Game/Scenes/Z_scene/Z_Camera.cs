using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Z_Camera : MonoBehaviour
{
    public GameObject player;
    public Vector3 playPos;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
         playPos.x = player.transform.position.x;
         playPos.y = player.transform.position.y;
         this.transform.position = playPos;
    }
}
