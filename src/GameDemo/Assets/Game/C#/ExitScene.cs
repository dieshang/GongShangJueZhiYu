using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitScene : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag=="Player")
        GameObject.Find("GameCanvas").transform.GetChild(3).gameObject.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        GameObject.Find("GameCanvas").transform.GetChild(3).gameObject.SetActive(false);
    }
}
